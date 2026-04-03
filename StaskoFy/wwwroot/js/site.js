// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', function () {
    // 1. Tell jQuery Validator NOT to ignore the hidden select element
    if (typeof $ !== 'undefined' && $.validator) {
        $.validator.setDefaults({ ignore: [] });
    }

    document.querySelectorAll('[data-choices]').forEach(function (element) {
        const config = {
            removeItemButton: element.dataset.removeItemButton === "true",
            searchEnabled: element.dataset.searchEnabled !== "false",
            placeholderValue: element.dataset.placeholderValue,
            noResultsText: element.dataset.noResultsText,
            itemSelectText: element.dataset.itemSelectText,
            shouldSort: element.dataset.shouldSort !== "false"
        };

        const choicesInstance = new Choices(element, config);

        element.addEventListener('change', function () {
            $(this).valid();
        });
    });
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


// Music Player functionality
// 1. Element Selectors
const audio = document.getElementById("myAudio");
const playPauseButton = document.getElementById("btn-play-pause");
const progressBar = document.getElementById("progress-bar");
const audioBar = document.getElementById("volume-bar");
const currentTimeLabel = document.querySelectorAll('.time')[0];
const durationLabel = document.querySelectorAll('.time')[1];

// 2. State Management
let currentPlayingId = null;

// 3. Icon Constants (Matching your gold theme)
const PLAY_ICON = '<svg viewBox="0 0 640 640" height="40" width="40" style="background-color:transparent"><path fill="#ffc107" d="M64 320C64 178.6 178.6 64 320 64C461.4 64 576 178.6 576 320C576 461.4 461.4 576 320 576C178.6 576 64 461.4 64 320zM252.3 211.1C244.7 215.3 240 223.4 240 232L240 408C240 416.7 244.7 424.7 252.3 428.9C259.9 433.1 269.1 433 276.6 428.4L420.6 340.4C427.7 336 432.1 328.3 432.1 319.9C432.1 311.5 427.7 303.8 420.6 299.4L276.6 211.4C269.2 206.9 259.9 206.7 252.3 210.9z" /></svg>';
const PAUSE_ICON = '<svg viewBox="0 0 448 512" height="40" width="40" style="background-color:transparent"><path fill="#ffc107" d="M48 64C21.5 64 0 85.5 0 112L0 400c0 26.5 21.5 48 48 48l64 0c26.5 0 48-21.5 48-48l0-288c0-26.5-21.5-48-48-48L48 64zm192 0c-26.5 0-48 21.5-48 48l0 288c0 26.5 21.5 48 48 48l64 0c26.5 0 48-21.5 48-48l0-288c0-26.5-21.5-48-48-48l-64 0z"/></svg>';

// --- CORE LOGIC ---

// 4. Global Play/Pause Synchronization
// This ensures that whether you click the footer, the card, or the spacebar, 
// ALL relevant icons on the page update at once.
audio.addEventListener('play', () => {
    playPauseButton.innerHTML = PAUSE_ICON;
    if (currentPlayingId) {
        const cardBtn = document.querySelector(`[data-song-id="${currentPlayingId}"]`);
        if (cardBtn) cardBtn.innerHTML = PAUSE_ICON;
    }
});

audio.addEventListener('pause', () => {
    playPauseButton.innerHTML = PLAY_ICON;
    if (currentPlayingId) {
        const cardBtn = document.querySelector(`[data-song-id="${currentPlayingId}"]`);
        if (cardBtn) cardBtn.innerHTML = PLAY_ICON;
    }
});

// 5. Main Footer Button Click
playPauseButton.addEventListener("click", () => {
    if (audio.src) {
        audio.paused ? audio.play() : audio.pause();
    }
});

// 6. Load Song Function
function loadSong(songId) {
    // If clicking the song that's already playing, just toggle it
    if (currentPlayingId === songId) {
        audio.paused ? audio.play() : audio.pause();
        return;
    }

    // Reset icons of all other cards to 'Play' before switching
    document.querySelectorAll('.play-btn-custom').forEach(btn => {
        btn.innerHTML = PLAY_ICON;
    });

    fetch(`/Songs/GetSong?id=${songId}`)
        .then(response => {
            if (!response.ok) throw new Error("Song not found");
            return response.json();
        })
        .then(data => {
            if (data) {
                // Update tracker
                currentPlayingId = songId;

                // Update Footer UI (Metadata)
                document.querySelector('.song-title').innerText = data.title;
                const artistList = (data.artists && Array.isArray(data.artists))
                    ? data.artists.map(a => a.name).join(', ')
                    : "Unknown Artist";
                document.querySelector('.artist-name').innerText = artistList;

                const albumImg = document.querySelector('.song-art');
                if (albumImg) albumImg.src = data.artCover;

                // Reset Progress Bar and Labels
                progressBar.value = 0;
                currentTimeLabel.innerText = "0:00";
                durationLabel.innerText = data.duration;

                // Set Audio Source and Play
                audio.src = data.audioUrl;
                audio.load();
                audio.play().catch(e => console.error("Playback blocked:", e));
            }
        })
        .catch(error => console.error('Error loading song:', error));
}

// --- CONTROLS & BARS ---

// 7. Progress Bar: Updates as song plays
audio.addEventListener('timeupdate', () => {
    if (audio.duration && !isNaN(audio.duration)) {
        const progress = (audio.currentTime / audio.duration) * 100;
        progressBar.value = progress;

        // Update current time label
        let mins = Math.floor(audio.currentTime / 60);
        let secs = Math.floor(audio.currentTime % 60);
        currentTimeLabel.innerText = `${mins}:${secs < 10 ? '0' : ''}${secs}`;
    }
});

// 8. Progress Bar: Seek logic (user drags slider)
progressBar.addEventListener("input", (e) => {
    if (audio.duration) {
        const time = (e.target.value / 100) * audio.duration;
        audio.currentTime = time;
    }
});

// 9. Volume Control
audioBar.addEventListener("input", (e) => {
    audio.volume = e.target.value / 100;
});