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

//JavaScript for music streaming
const audio = document.getElementById("myAudio")
const playPauseButton = document.getElementById("btn-play-pause");
//const playPrevious = document.getElementById("btn-previous");
//const playNext = document.getElementById("btn-next");
const volumeSlider = document.getElementById("durationSlider");

playPauseButton.addEventListener("click", () => {
    if (audio.paused) {
        audio.play();
        playPauseButton.innerHTML = '<svg viewBox="0 0 640 640" height="30" width="30" style="background - color:transparent">< path fill = "#ffc107" d = "M64 320C64 178.6 178.6 64 320 64C461.4 64 576 178.6 576 320C576 461.4 461.4 576 320 576C178.6 576 64 461.4 64 320zM252.3 211.1C244.7 215.3 240 223.4 240 232L240 408C240 416.7 244.7 424.7 252.3 428.9C259.9 433.1 269.1 433 276.6 428.4L420.6 340.4C427.7 336 432.1 328.3 432.1 319.9C432.1 311.5 427.7 303.8 420.6 299.4L276.6 211.4C269.2 206.9 259.9 206.7 252.3 210.9z" /></svg >';
    } else {
        audio.pause();
        playPauseButton.innerHTML = '<svg viewBox="0 0 640 640" height="30" width="30" style="background - color:transparent"><path fill="#ffc107" d="M48 32C21.5 32 0 53.5 0 80L0 432c0 26.5 21.5 48 48 48l64 0c26.5 0 48-21.5 48-48l0-352c0-26.5-21.5-48-48-48L48 32zm224 0c-26.5 0-48 21.5-48 48l0 352c0 26.5 21.5 48 48 48l64 0c26.5 0 48-21.5 48-48l0-352c0-26.5-21.5-48-48-48l-64 0z"/></svg>'
    }
});

durationSlider.addEventListener("input", (e) => {
    audio.currentTime = e.target.value;
});