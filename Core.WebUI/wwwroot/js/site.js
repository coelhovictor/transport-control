// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('.select2').select2();
    $('.select2-none').select2({
        allowClear: true,
        placeholder: "None",
    });
    $('.select2-none').val(null); // Select the option with a value of 'US'
    $('.select2-none').trigger('change');
    $('.datepickertime').datetimepicker({
        format: 'MM/DD/YYYY HH:mm',
        icons: { time: 'far fa-clock' }
    });
    $('.datepickertime-wtime').datetimepicker({
        format: 'MM/DD/YYYY',
        icons: { time: 'far fa-clock' }
    });
    $('.daterangepicker').daterangepicker();
    $('.colorpicker-input').colorpicker()
    $('.colorpicker-input').on('colorpickerChange', function (event) {
        $('.colorpicker-input .fa-square').css('color', event.color.toString());
    })
})

function dateFormated() {
    return dateFormatedWithDate(new Date());
}

function firstdateFormated() {
    let date = new Date(), y = date.getFullYear(), m = date.getMonth(),
        hh = date.getHours(), mm = date.getMinutes();
    return dateFormatedWithDate(new Date(y, m, 1, hh, mm));
}

function lastdateFormated() {
    let date = new Date(), y = date.getFullYear(), m = date.getMonth(),
        hh = date.getHours(), mm = date.getMinutes();
    return dateFormatedWithDate(new Date(y, m + 1, 0, hh, mm));
}

function dateFormatedWithDate(date) {
    let d = new Date(date);
    return ("0" + (d.getMonth() + 1)).slice(-2) + "/" + ("0" + d.getDate()).slice(-2) + "/" +
        d.getFullYear() + " " + ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2);
}

function dateFormatedWithDateWithoutTime(date) {
    if (!date) return '';
    let d = new Date(date);
    return ("0" + (d.getMonth() + 1)).slice(-2) + "/" + ("0" + d.getDate()).slice(-2) + "/" +
        d.getFullYear();
}

function formatDate(date) {
    return date.replaceAll('/', '-');
}

function successMessage(message) {
    toastr.success(message);
}

function warningMessage(message) {
    toastr.warning(message);
}

function errorMessage(message) {
    toastr.error(message);
}


function errorFormInvalid(selector, message) {
    $(selector).addClass('is-invalid');
    $(selector + '-msg').html(message);
    $(selector + '-msg').show();
}

function hideFormInvalid(selector) {
    $(selector).removeClass('is-invalid');
    $(selector + '-msg').hide();
}


function reloadPage(timeout) {
    setTimeout(function () {
        location.reload();
    }, timeout)
}


function blockUICard(selector, show) {
    let overlay = $(selector).find('.overlay');
    if (overlay) {
        if (show) {
            $(overlay).show();
        } else {
            $(overlay).hide();
        }
    }
}