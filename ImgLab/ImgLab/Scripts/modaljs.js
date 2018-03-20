function ModalEditSubmit() {
    waitingDialog.show("Загрузка");
    $.ajax({
        type: 'POST',
        url: '/Settings/Edit',
        data: $('#editform').serialize(),
        error: function (xhr) {
            waitingDialog.hide();
            alert('Ошибка! ' + xhr.status + ' ' + xhr.statusText);
        },
        success: function (a) {
            waitingDialog.hide();
            $('#modalbody').html(a);
        }
    });
}

function ModalCreateSubmit() {
    waitingDialog.show("Загрузка");
    $.ajax({
        type: 'POST',
        url: '/Settings/Create',
        data: $('#createform').serialize(),
        error: function (xhr) {
            waitingDialog.hide();
            alert('Ошибка! ' + xhr.status + ' ' + xhr.statusText);
        },
        success: function (a) {
            waitingDialog.hide();
            $('#modalbody').html(a);
        }
    });
}


function EditModal(id) {
    waitingDialog.show("Загрузка");

    $.ajax({
        type: "GET", url: "/Settings/Edit/" + id,
        timeout: 10000, async: true,
        error: function (xhr) {
            waitingDialog.hide();
            alert('Ошибка! ' + xhr.status + ' ' + xhr.statusText);
        },
        success: function (a) {
            waitingDialog.hide();
            $("#modalid").modal("toggle");
            $("#modaltitle").text("Редактировать");
            document.getElementById("modalbody").innerHTML = a;

        }
    });

}

function DetailsModal(id) {
    waitingDialog.show("Загрузка");

    $.ajax({
        type: "GET", url: "/Settings/Details/" + id,
        timeout: 10000, async: true,
        error: function (xhr) {
            waitingDialog.hide();
            alert('Ошибка! ' + xhr.status + ' ' + xhr.statusText);
        },
        success: function (a) {
            waitingDialog.hide();
            $("#modalid").modal("toggle");
            $("#modaltitle").text("Подробно");
            document.getElementById("modalbody").innerHTML = a;

        }
    });

}

function RemoveModal(id) {
    waitingDialog.show("Загрузка");

    $.ajax({
        type: "GET", url: "/Settings/Delete/" + id,
        timeout: 10000, async: true,
        error: function (xhr) {
            waitingDialog.hide();
            alert('Ошибка! ' + xhr.status + ' ' + xhr.statusText);
        },
        success: function (a) {
            waitingDialog.hide();
            $("#modalid").modal("toggle");
            $("#modaltitle").text("Удалить");
            document.getElementById("modalbody").innerHTML = a;
        }
    });
}

function CreateModal() {
    waitingDialog.show("Загрузка");

    $.ajax({
        type: "GET", url: "/Settings/Create/",
        timeout: 10000, async: true,
        error: function (xhr) {
            waitingDialog.hide();
            alert('Ошибка! ' + xhr.status + ' ' + xhr.statusText);
        },
        success: function (a) {
            waitingDialog.hide();
            $("#modalid").modal("toggle");
            $("#modaltitle").text("Создать");
            document.getElementById("modalbody").innerHTML = a;
        }
    });
}

$(function () {
    $("#modalid").on("hidden.bs.modal", function () {
        $("#modalbody").text("");
    });

    $('[data-toggle="tooltip"]').tooltip();
})
