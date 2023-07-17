// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".button-select-date").hover(function () {
        $("#selectDate").css("display", "block");
    }, function () {
        $("#selectDate").css("display", "none");
    });
});
