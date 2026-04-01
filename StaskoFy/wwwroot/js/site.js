// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//document.addEventListener('DOMContentLoaded', function () {

//    document.querySelectorAll('[data-choices]').forEach(function (element) {

//        const config = {
//            removeItemButton: element.dataset.removeItemButton === "true",
//            searchEnabled: element.dataset.searchEnabled !== "false",
//            placeholderValue: element.dataset.placeholderValue,
//            noResultsText: element.dataset.noResultsText,
//            itemSelectText: element.dataset.itemSelectText,
//            shouldSort: element.dataset.shouldSort !== "false"
//        };

//        new Choices(element, config);
//    });
//});

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
            // 2. IMPORTANT: Actually submit the form now
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