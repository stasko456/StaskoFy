// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {

    document.querySelectorAll('[data-choices]').forEach(function (element) {

        const config = {
            removeItemButton: element.dataset.removeItemButton === "true",
            searchEnabled: element.dataset.searchEnabled !== "false",
            placeholderValue: element.dataset.placeholderValue,
            noResultsText: element.dataset.noResultsText,
            itemSelectText: element.dataset.itemSelectText,
            shouldSort: element.dataset.shouldSort !== "false"
        };

        new Choices(element, config);
    });
});