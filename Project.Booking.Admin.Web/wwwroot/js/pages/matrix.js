const matrix = {
    init: () => {
        $("div[data-action='unit-item']").click((e) => {
            let unitID = $(e.currentTarget).attr("unit-id");
            matrix.getUnit(unitID);
        });
    },
    getUnit: (id) => {
        app.loading(true);
        $.ajax({
            url: baseUrl + 'Matrix/GetUnit',
            type: 'post',
            data: { ID: id },
            success: function (res) {
                app.ajaxComplete(res, function () {
                    $("#div-unit-modal").empty().html(res.html);
                    $('button[data-action="btn-save-unit-bookint"]').unbind('click').click((e) => {
                        let unitID = $(e.currentTarget).attr("unit-id");
                        if (confirm('Are you sure ?'))
                            matrix.saveUnitBooking(unitID);
                    });
                    $('#modal-unit').modal();
                }, false);
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            }
        });
        return false;
    },
    saveUnitBooking: (id) => {
        app.loading(true);
        $.ajax({
            url: baseUrl + 'Matrix/SaveUnitBooking',
            type: 'post',
            data: { ID: id },
            success: function (res) {
                app.ajaxComplete(res, function () {
                    //$('.close').click();
                    $(".modal-content > .modal-footer").remove();
                    $("#div-unit-status").text('Reservation');
                    appSignalR.sendUnitStatusHubServer(res.unit)
                });
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            }
        });
        return false;
    }
};