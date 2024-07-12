const report = {
    init: () => {

        $("#ProjectID").val(selectProjectID);

        var from = $("#StartDate")
            .datepicker($.extend({}, $.datepicker.regional.en,
                {
                    //defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    numberOfMonths: 1,
                    dateFormat: "yy-mm-dd",
                    defaultDate: +0
                }
            ))
            .on("change", function () {
                to.datepicker("option", "minDate", $.datepicker.getDate(this));
            }),
            to = $("#EndDate").datepicker($.extend({}, $.datepicker.regional.en,
                {
                    //defaultDate: "+1w",
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: "yy-mm-dd",
                    numberOfMonths: 1
                }
            )).on("change", function () {
                from.datepicker("option", "maxDate", $.datepicker.getDate(this));
            });

        $("#form-unit-search").submit(() => {
            report.getUnitData();
            return false;
        });

        $("#form-booking-payment-search").submit(() => {
            report.getBookingPaymentData();
            return false;
        });

        $("#form-transfer-payment-search").submit(() => {
            report.getTransferPaymentData();
            return false;
        });

        $("#form-register-search").submit(() => {
            report.getRegisterData();
            return false;
        });

        report.initialSelectize();
    },
    initialSelectize: () => {

        $('#ProjectID').selectize({
            maxItems: 1,
            valueField: 'value',
            labelField: 'text',
            searchField: ['text'],
            create: false
        });

        $('#UnitStatusIDs').selectize({
            maxItems: null,
            valueField: 'value',
            labelField: 'text',
            searchField: ['text'],
            create: false
        });
    },
    getUnitData: () => {
        app.loading(true);

        var datastring = $("#form-unit-search").serialize();
        var formData = datastring + '&' + $.param({                        
            UnitStatusIDs: $.trim($('#UnitStatusIDs').val())
        });

        $.ajax({
            url: baseUrl + 'Report/GetUnitData',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    const url = baseUrl + 'Report/DownloadFileTempExcel?fileGuid=' + res.fileGuid
                        + '&filename=' + res.fileName;
                    window.location = url;
                }, false);
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: formData
        });
        return false;
    },
    getBookingPaymentData: () => {
        app.loading(true);

        var datastring = $("#form-booking-payment-search").serialize();
        var formData = datastring + '&' + $.param({
            //UnitStatusIDs: $.trim($('#UnitStatusIDs').val())
        });

        $.ajax({
            url: baseUrl + 'Report/GetBookingPaymentData',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    const url = baseUrl + 'Report/DownloadFileTempExcel?fileGuid=' + res.fileGuid
                        + '&filename=' + res.fileName;
                    window.location = url;
                }, false);
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: formData
        });
        return false;
    },
    getRegisterData: () => {
        app.loading(true);

        var datastring = $("#form-register-search").serialize();
        var formData = datastring + '&' + $.param({
            //UnitStatusIDs: $.trim($('#UnitStatusIDs').val())
        });

        $.ajax({
            url: baseUrl + 'Report/GetRegisterData',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    const url = baseUrl + 'Report/DownloadFileTempExcel?fileGuid=' + res.fileGuid
                        + '&filename=' + res.fileName;
                    window.location = url;
                }, false);
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: formData
        });
        return false;
    },
    getTransferPaymentData: () => {
        app.loading(true);

        var datastring = $("#form-transfer-payment-search").serialize();
        var formData = datastring + '&' + $.param({
            //UnitStatusIDs: $.trim($('#UnitStatusIDs').val())
        });

        $.ajax({
            url: baseUrl + 'Report/GetTransferPaymentData',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    const url = baseUrl + 'Report/DownloadFileTempExcel?fileGuid=' + res.fileGuid
                        + '&filename=' + res.fileName;
                    window.location = url;
                }, false);
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