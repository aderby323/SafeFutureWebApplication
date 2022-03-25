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

function getReport() {
    var from = new Date(document.getElementById('fromDate').value).toISOString();
    var to = new Date(document.getElementById('toDate').value).toISOString();
    const hostName = window.location.host;
    const reportUrl = `https://${hostName}/Admin/GetReport?fromDate=${from}&toDate=${to}`;
    let today = new Date().toISOString().slice(0, 10);

    fetch(reportUrl)
        .then(resp => {
            if (!resp.status === 200) {
                throw new Error("An error occured when generating report");
            }
            return resp.blob();
        })
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            a.download = `${today}_Report.csv`;
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
        })
        .catch((error) => console.log(error));
}

var loginElements = document.getElementsByName("login-input");
var forgotPasswordElement = document.getElementById('login-forgotpassword');

function toggleVisibility() {
    Array.from(loginElements).forEach((el) => {
        el.style.display = el.style.display === 'block' ? 'none' : 'block';
    });

    forgotPasswordElement.innerText = forgotPasswordElement.innerText === 'Forgot Password?' ? 'Cancel' : 'Forgot Password?';
}