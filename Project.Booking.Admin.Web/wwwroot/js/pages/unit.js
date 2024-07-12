var unit = {
    init: () => {
        $("#ProjectID").val(selectProjectID);
        $("#btn-search").click(() => {
            unit.getUnitByProjectAll();
        });
        $("#btn-matrix").click(() => {
            let url = baseUrl + 'Matrix/Pre?projectID=' + $("#ProjectID").val();
            window.location = url;
            return false;
        });
        unit.getUnitByProjectAll();
    },
    initManage: function () {
        $('input.integer').autoNumeric({ aSep: ',', aNeg: '-', vMin: 0, mNum: 10, aPad: false });
        $('input[area="readonly"]').attr('readonly', 'readonly');
        $("#frm-unit").unbind('submit').submit(function () {
            unit.saveUnit(this);
            return false;
        })
    },
    getUnitByProjectAll: () => {

        app.loading(true);

        var data = {
            ProjectID: $("#ProjectID").val()
        };

        $.ajax({
            url: baseUrl + 'Unit/GetUnitByProjectAll',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    $("#div-unit-list").html(res.html);
                    $('#table-unit').DataTable({
                        "order": [[10, "desc"]],
                        responsive: true
                    });
                }, false);
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: data
        });
        return false;
    },
    saveUnit: function (form) {
        app.loading(true);
        $.ajax({
            url: baseUrl + 'Unit/SaveUnit',
            type: 'post',
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                app.ajaxComplete(res, function () {                    
                    appSignalR.sendUnitStatusHubServer(res.unit)
                    $("#div-frm-unit").html(res.html);
                    unit.initManage();
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