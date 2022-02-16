// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Modal jQuery
$(function () {
    var placeholder = $('#Placeholder');
    $('button[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholder.html(data);
            placeholder.find('.modal').modal('show');
        })
    })

    placeholder.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');

        var formUrl = form.attr('action');
        var send = form.serialize();
        $.post(formUrl, send).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholder.find('.modal-body').replaceWith(newBody);

            var validValue = newBody.find('[name="IsValid"]').val();
            var isValid = validValue === "True";
            if (isValid) {
                placeholder.find('.modal').modal('hide');
                window.location.reload();
            };
        })
    });
})

function getFromDate() {
    return document.getElementById("fromDate").value;
}

function getToDate() {
    return document.getElementById("toDate").value;
}

function getReport() {

}