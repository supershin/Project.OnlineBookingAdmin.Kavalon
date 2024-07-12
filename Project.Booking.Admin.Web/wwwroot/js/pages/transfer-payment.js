let tbl_list;
const transfer = {
    init: () => {
        $("#ProjectID").val(selectProjectID);
        $("#form-search").submit(() => {
            app.reloadTable(tbl_list);
            return false;
        });
        transfer.getPaymentList();
    },
    getPaymentList: () => {

        tbl_list = $('#tbl-table').dataTable({
            "dom": '<<t>lip>',
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "processing": true,
            "serverSide": true,
            "ajax": {
                url: baseUrl + 'TransferPayment/GetPaymentList',
                type: "POST",
                data: function (json) {
                    //app.loading(true, "#tbl-table");
                    //Make your callback here.                                        
                    var datastring = $("#form-search").serialize();
                    //json.ProjectID = project_id;
                    json = datastring + '&' + $.param(json);

                    //return json;
                    return json;
                },
                "dataSrc": function (res) {
                    //app.ajaxVerifySession(res);                    
                    return res.data;
                },
                complete: function (res) {
                    //$("button[data-action='delete-unit']").unbind('click').click((e) => {
                    //    let id = $(e.currentTarget).attr('data-id');
                    //    unit.modalDelete(id);
                    //    return false;
                    //});
                    $("button[data-action='edit-payment']").unbind('click').click((e) => {
                        let id = $(e.currentTarget).attr('data-id');
                        transfer.getPayment(id);
                        return false;
                    });
                }
            },
            "ordering": true,
            //"order": [[8, "desc"]],
            "columns": [
                { 'data': 'CustomerName' },
                { 'data': 'Email' },
                { 'data': 'TransferDate', 'className': 'text-center' },
                { 'data': 'Amount', 'className': 'text-center' },
                { 'data': 'ApproveAmount', 'className': 'text-center' },
                {
                    'data': 'StatusID',
                    //'orderable': false,
                    //'width': '70px',
                    "className": "text-center",
                    'mRender': function (ID, type, data, obj) {
                        let bg = '';
                        if (data.StatusID == verify) bg = "badge-warning";
                        else if (data.StatusID == approve) bg = "badge-success";
                        else if (data.StatusID == fail) bg = "badge-danger";
                        var html = '<div><label style="width:65px;" class="badge ' + bg + '" >' + data.StatusName + '</label></div>';
                        return html;
                    }
                },
                { 'data': 'Quota', 'className': 'text-center' },
                {
                    'data': 'VerifyBy',
                    //'orderable': false,
                    //'width': '100px',
                    "className": "text-center",
                    'mRender': function (ID, type, data, obj) {
                        var html = '<div>' + data.VerifyDate + '</div>';
                        html += '<div>' + data.VerifyBy + '</div>';
                        return html;
                    }
                },
                {
                    'data': 'ID',
                    'orderable': false,
                    //'width': '40px',
                    "className": "text-center",
                    'mRender': function (ID, type, data, obj) {
                        var html = '<div><button data-action="edit-payment" style="margin-right:5px;" class="btn btn-xs btn-warning" data-id=' + data.ID + ' >Edit</button>';
                        html += '<a href=' + data.UrlFile + ' class="btn btn-xs btn-success" target="_blank" >File</button></div>';
                        return html;
                    }
                }
            ]
        });
    },
    getPayment: (id) => {
        app.loading(true);

        $.ajax({
            url: baseUrl + 'TransferPayment/GetPayment',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    transfer.setPayment(res);
                }, false);
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: { ID: id }
        });
        return false;
    },
    setPayment: (res) => {
        $("#partial-payment-modal").empty().html(res.html);
        
        $("#form-payment").unbind('submit').submit(() => {            
            transfer.savePayment();
            return false;
        });

        $('input.decimal').autoNumeric({ aSep: ',', aNeg: '-', mDec: 2, mNum: 10 });
        $('input.integer').autoNumeric({ aSep: ',', aNeg: '-', vMin: -9999999999, mNum: 10, aPad: false });

        $('#modal-payment').modal({
            show: 'true',
            backdrop: 'static'
        });
    },    
    savePayment: function () {

        var datastring = $("#form-payment").serialize();
        var formData = datastring + '&' + $.param({
        });                
        app.loading(true);        
        $.ajax({
            url: baseUrl + 'TransferPayment/SavePayment',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {                
                app.ajaxComplete(res, function () {
                    app.reloadTable(tbl_list)
                    $(".auto-close").click();
                });               
                app.loading(false);                
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: formData
        });
        return false;
    }
};