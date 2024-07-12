$.ajaxSetup({
    cache: false
});


var app = {
    init: function () {
        $('input.decimal').autoNumeric({ aSep: ',', aNeg: '-', mDec: 2, mNum: 10 });
        $('input.integer').autoNumeric({ aSep: ',', aNeg: '-', vMin: -9999999999, mNum: 10, aPad: false });
    },
    loading: function (show, id) {

        var customElement = $("<div>", {
            "css": {
                "border": "4px dashed gold",
                "font-size": "40px",
                "text-align": "center",
                "padding": "10px"
            },
            "class": "your-custom-class",
            "text": "Custom!"
        });
        var diaplay = "hide";

        if (show) {
            diaplay = "show";
        }

        if (id) {
            $(id).LoadingOverlay(diaplay, {
                background: "rgba(165, 190, 100, 0.5)",
                //background: "#eff2f3",
                image: '<svg xmlns="http://www.w3.org/2000/svg" class="icon" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round"><path stroke="none" d="M0 0h24v24H0z" fill="none"/><path d="M9 4.55a8 8 0 0 1 6 14.9m0 -4.45v5h5" /><line x1="5.63" y1="7.16" x2="5.63" y2="7.17" /><line x1="4.06" y1="11" x2="4.06" y2="11.01" /><line x1="4.63" y1="15.1" x2="4.63" y2="15.11" /><line x1="7.16" y1="18.37" x2="7.16" y2="18.38" /><line x1="11" y1="19.94" x2="11" y2="19.95" /></svg>',
                imageClass: "loading-custom",
                progress: true,
            });
        }
        else {
            $.LoadingOverlay(diaplay, {
                background: "rgb(155 153 252 / 50%)",
                image: "",
                fontawesome: "fa fa-spinner fa-spin",
                fontawesomeResizeFactor: 1,
                fontawesomeColor: "rgb(98 88 88)",
                // Sizing
                size: 50,
                minSize: 20,
                maxSize: 50
            });
        }
    },
    notify: function (type, message) {
        if (type === 'error') {
            $.growl.error({ message: message, duration: 10000, location: 'tc', size: 'large', fixed:false});
        }
        else if (type === 'success') {
            $.growl.notice({ title: type, message: message, duration: 10000, location: 'tc', size: 'large'});
        }
        else if (type === 'warning') {
            $.growl.warning({ message: message, duration: 10000, location: 'tc', size: 'large' });
        }
    },
    reloadTable: function (table) {
        if (table != undefined) {
            //table.api().clear().draw();
            table.DataTable().clear().draw();
            return;
        }
    },
    ajaxComplete: function (res, callbackSuccess, successAlert = true) {
        if (res.success) {
            if (successAlert) {
                app.notify('success', res.message);
            }
            if (typeof callbackSuccess === 'function') {
                callbackSuccess();
            }
        }
        else {
            //console.log(res);
            if (res.sessionTimeOut) {
                app.notify('warning', res.message);
                setTimeout(function () {
                    window.location.reload();
                }, 3000);
            }
            else {
                app.notify('error', res.message);
            }            
        }
    } 
};