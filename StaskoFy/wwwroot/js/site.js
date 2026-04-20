// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification

document.addEventListener('DOMContentLoaded', function () {
    if (typeof $ !== 'undefined' && $.validator) {
        $.validator.setDefaults({ ignore: [] });
    }

    document.querySelectorAll('[data-choices]').forEach(function (element) {
        const maxItems = element.dataset.maxItemCount ? parseInt(element.dataset.maxItemCount) : -1;

        const customMaxMessage = element.dataset.maxText || `You can only select ${maxItems} items.`;

        const config = {
            removeItemButton: element.dataset.removeItemButton === "true",
            searchEnabled: element.dataset.searchEnabled !== "false",
            placeholderValue: element.dataset.placeholderValue,
            noResultsText: element.dataset.noResultsText || "No results found",
            itemSelectText: element.dataset.itemSelectText || "",
            shouldSort: element.dataset.shouldSort !== "false",

            allowHTML: true,
            maxItemCount: maxItems,

            maxItemText: () => {
                return `<span class="text-danger fw-semibold">${customMaxMessage}</span>`;
            }
        };

        const choicesInstance = new Choices(element, config);

        element.addEventListener('change', function () {
            if (typeof $ !== 'undefined' && typeof $(this).valid === 'function') {
                $(this).valid();
            }
        });
    });
});

function initializeMusicSelects() {
    const elements = document.querySelectorAll('.js-choice');

    elements.forEach(el => {
        if (!el.parentElement.classList.contains('choices')) {
            new Choices(el, {
                removeItemButton: true,
                allowHTML: true,
                placeholder: true,
                placeholderValue: 'Select options...'
            });
        }
    });
}

document.addEventListener('DOMContentLoaded', initializeMusicSelects);

document.body.addEventListener('htmx:afterOnLoad', function (evt) {
    initializeMusicSelects();
});

function confirmDelete(event, formId) {
    event.preventDefault();

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success px-4 mx-2",
            cancelButton: "btn btn-outline-secondary px-4",
            popup: "border-1 border-secondary shadow-lg"
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: "<span style='color: #fff;'>Are you sure?</span>",
        icon: "warning",
        iconColor: "#ffc107",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true,
        background: "#1a1a1a",
        color: "#ffffff",
        backdrop: `rgba(0,0,0,0.8)`
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById(formId).submit();
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire({
                title: "Cancelled",
                icon: "error",
                background: "#1a1a1a",
                color: "#ffffff",
                showConfirmButton: false,
                timer: 1500
            });
        }
    });
}

const PLAY_ICON = '<svg viewBox="0 0 640 640" height="40" width="40" style="background-color:transparent"><path fill="#ffc107" d="M64 320C64 178.6 178.6 64 320 64C461.4 64 576 178.6 576 320C576 461.4 461.4 576 320 576C178.6 576 64 461.4 64 320zM252.3 211.1C244.7 215.3 240 223.4 240 232L240 408C240 416.7 244.7 424.7 252.3 428.9C259.9 433.1 269.1 433 276.6 428.4L420.6 340.4C427.7 336 432.1 328.3 432.1 319.9C432.1 311.5 427.7 303.8 420.6 299.4L276.6 211.4C269.2 206.9 259.9 206.7 252.3 210.9z" /></svg>';
const PAUSE_ICON = '<svg viewBox="0 0 448 512" height="40" width="40" style="background-color:transparent"><path fill="#ffc107" d="M48 64C21.5 64 0 85.5 0 112L0 400c0 26.5 21.5 48 48 48l64 0c26.5 0 48-21.5 48-48l0-288c0-26.5-21.5-48-48-48L48 64zm192 0c-26.5 0-48 21.5-48 48l0 288c0 26.5 21.5 48 48 48l64 0c26.5 0 48-21.5 48-48l0-288c0-26.5-21.5-48-48-48l-64 0z"/></svg>';

let songQueue = [];
let currentQueueIndex = -1;

const audio = document.getElementById("myAudio");
const playPauseButton = document.getElementById("btn-play-pause");
const progressBar = document.getElementById("progress-bar");
const audioBar = document.getElementById("volume-bar");
const currentTimeLabel = document.querySelectorAll('.time')[0];
const durationLabel = document.querySelectorAll('.time')[1];
const btnPrevious = document.getElementById('btn-previous');
const btnNext = document.getElementById('btn-next');

function playFromQueue(index) {
    if (index < 0 || index >= songQueue.length) return;

    currentQueueIndex = index;
    const data = songQueue[index];

    document.querySelector('.song-title').innerText = data.title;
    document.querySelector('.artists-names').innerText = Array.isArray(data.artists) ? data.artists.join(', ') : data.artists;

    const albumImg = document.querySelector('.song-art');
    if (albumImg) albumImg.src = data.artCover;

    durationLabel.innerText = data.duration;

    audio.src = data.audioUrl;
    audio.load();
    audio.play().catch(e => console.warn("Playback blocked:", e));

    if (currentQueueIndex >= songQueue.length - 2) {
        fetchMoreSongsForQueue();
    }
}

function loadSong(songId) {
    fetch(`/Song/GetSongForQueue?id=${songId}`)
        .then(response => response.json())
        .then(data => {
            if (data) {
                songQueue.push(data);
                playFromQueue(songQueue.length - 1);
            }
        })
        .catch(error => console.error('Error loading song:', error));
}

function fetchMoreSongsForQueue() {
    const offset = songQueue.length;
    fetch(`/Song/GetSongsForQueue?offset=${offset}&count=10`)
        .then(res => res.json())
        .then(newSongs => {
            if (newSongs && newSongs.length > 0) {

                songQueue = [...songQueue, ...newSongs];

                if (currentQueueIndex === -1) {
                    playFromQueue(0);
                }
            }
        });
}


audio.addEventListener('ended', () => {
    playFromQueue(currentQueueIndex + 1);
});

audio.addEventListener('play', () => {
    playPauseButton.innerHTML = PAUSE_ICON;
});

audio.addEventListener('pause', () => {
    playPauseButton.innerHTML = PLAY_ICON;
});

playPauseButton.addEventListener("click", () => {
    if (audio.src) {
        audio.paused ? audio.play() : audio.pause();
    }
});

btnNext.addEventListener("click", () => {
    playFromQueue(currentQueueIndex + 1);
})

btnPrevious.addEventListener("click", () => {
    playFromQueue(currentQueueIndex - 1);
})

audio.addEventListener('timeupdate', () => {
    if (audio.duration && !isNaN(audio.duration)) {
        const progress = (audio.currentTime / audio.duration) * 100;
        progressBar.value = progress;
        let mins = Math.floor(audio.currentTime / 60);
        let secs = Math.floor(audio.currentTime % 60);
        currentTimeLabel.innerText = `${mins}:${secs < 10 ? '0' : ''}${secs}`;
    }
});

progressBar.addEventListener("input", (e) => {
    if (audio.duration) {
        audio.currentTime = (e.target.value / 100) * audio.duration;
    }
});

audioBar.addEventListener("input", (e) => {
    audio.volume = e.target.value / 100;
});

function loadSongsFromPlaylistToQueue(playlistId) {
    fetch(`/Playlist/GetPlaylistSongsForQueue?id=${playlistId}`)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                songQueue = data;
                currentQueueIndex = 0;
                playFromQueue(0);
            }
        })
}

function loadSongsFromAlbumToQueue(albumId) {
    fetch(`/Album/GetAlbumSongsForQueue?id=${albumId}`)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                songQueue = data;
                currentQueueIndex = 0;
                playFromQueue(0);
            }
        })
}

function loadSongsFromLikedSongsToQueue(albumId) {
    fetch(`/LikedSongs/GetLikedSongsForQueue`)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                songQueue = data;
                currentQueueIndex = 0;
                playFromQueue(0);
            }
        })
}

function checkAndInitialize() {
    const forbiddenPaths = [
        '/User/Login', '/User/Register', '/Genre/Create', '/Genre/Edit',
        '/Song/Create', '/Song/Edit', '/Album/Create', '/Album/Edit',
        '/Playlist/Create', '/Playlist/Edit', '/Genre',
        '/Admin/ManageAlbumsStatus', '/Admin/ManageArtistsStatus'
    ];

    const currentPath = window.location.pathname;
    const isForbidden = forbiddenPaths.some(path => currentPath === path || currentPath.startsWith(path + '/'));

    if (!isForbidden) {
        if (songQueue.length === 0 && currentQueueIndex === -1) {
            fetchMoreSongsForQueue();
            playFromQueue(0);
        }
        else if (currentQueueIndex !== -1 && audio.paused) {
            playFromQueue(currentQueueIndex);
        }
    } else {
        if (!audio.paused) {
            audio.pause();
            console.log("Entering forbidden page: Music paused.");
        }
    }
}

checkAndInitialize();

document.addEventListener('DOMContentLoaded', function () {
    const observerOptions = {
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            }
        });
    }, observerOptions);

    document.querySelectorAll('.fade-in-up').forEach(el => {
        observer.observe(el);
    });
});

document.body.addEventListener('htmx:afterOnLoad', function (evt) {
    initializeMusicSelects();

    document.querySelectorAll('.fade-in-up').forEach(el => {
        el.style.opacity = "0";
        setTimeout(() => {
            el.style.transition = "opacity 0.8s ease, transform 0.8s ease";
            el.style.opacity = "1";
            el.style.transform = "translateY(0)";
        }, 50);
    });
});

function updateNavigation() {
    var navLinks = document.querySelectorAll('.nav-link');
    var currentPath = window.location.pathname.toLowerCase().replace(/\/$/, "");

    navLinks.forEach(function (link) {
        var targetUrl = link.getAttribute('href') || link.getAttribute('hx-get') || "";
        var targetPath = targetUrl.toLowerCase().replace(/\/$/, "");

        link.classList.remove('active');
        if (targetPath && targetPath === currentPath) {
            link.classList.add('active');
        }
    });
}

document.addEventListener('DOMContentLoaded', updateNavigation);

document.body.addEventListener('htmx:afterSettle', function () {
    updateNavigation();

    var mainContent = document.getElementById('main-content');
    if (mainContent) {
        mainContent.scrollTop = 0;

        mainContent.classList.remove('fade-in-up');
        void mainContent.offsetWidth;
        mainContent.classList.add('fade-in-up');
    }
});