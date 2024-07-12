var booking = {
    init: () => {
        $("#ProjectID").val(selectProjectID);

        $("#btn-search").click(() => {
            booking.getBookingByProjectAll();
        });
        booking.getBookingByProjectAll();
    },
    initManage: () => {
        $('input.integer').autoNumeric({ aSep: ',', aNeg: '-', vMin: 0, mNum: 10, aPad: false });
        $("#frm-booking").unbind('submit').submit(function () {            
            booking.SaveCancelBooking(this);
            return false;
        })
    },
    getBookingByProjectAll: () => {

        app.loading(true);

        var data = {
            ProjectID: $("#ProjectID").val()
        };

        $.ajax({
            url: baseUrl + 'Booking/GetBookingByProjectAll',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    $("#div-booking-list").html(res.html);
                    $('#table-booking').DataTable({
                        "order": [[3, "desc"]],
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
    SaveCancelBooking: (form) => {
        if (confirm("คุณแน่ใจที่จะยกเลิกรายการจองนี้ ?")) {
            app.loading(true);
            $.ajax({
                url: baseUrl + 'Booking/SaveCancelBooking',
                type: 'post',
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.success) {
                        app.notify('success', res.message);
                        appSignalR.sendUnitStatusHubServer(res.unit)
                    }
                    else {
                        if (res.message !== "") app.notify('error', res.message);
                    }
                    $("#div-frm-booking").html(res.html);
                    booking.initManage();
                    app.loading(false);
                },
                error: function (xhr, status, error) {
                    window.location.reload();
                }
            });
        }
      
        return false;
    }
};