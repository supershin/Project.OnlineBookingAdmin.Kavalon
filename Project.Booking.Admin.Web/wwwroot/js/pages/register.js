const register = {
    init: () => {
        register.validateData();

        $('#table-register').DataTable({
            responsive: true
        });

        $("button[data-action='edit-quota']").click((e) => {
            let id = $(e.currentTarget).attr("data-id");
            register.getProjectQuota(id);
        });

    },
    getProjectQuota: (id) => {
        app.loading(true);

        $.ajax({
            url: baseUrl + 'Register/GetProjectQuota',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    register.setProjectQuota(res);
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
    validateData: function () {
        $('#frm-register').bootstrapValidator({
            // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
            //feedbackIcons: {
            //    valid: 'glyphicon glyphicon-ok',
            //    invalid: 'glyphicon glyphicon-remove',
            //    validating: 'glyphicon glyphicon-refresh'
            //},
            fields: {
                FirstName: {
                    validators: {
                        notEmpty: {
                            message: 'Please entry name'
                        }
                    }
                },
                Email: {
                    validators: {
                        notEmpty: {
                            message: 'Please emtry email'
                        }
                    }
                },
                Mobile: {
                    validators: {
                        notEmpty: {
                            message: 'Please entry mobile'
                        }
                    }
                },
                RegisterTypeID: {
                    validators: {
                        notEmpty: {
                            message: 'Please select register type'
                        }
                    }
                },
                Password: {
                    validators: {
                        //notEmpty: {
                        //    message: 'โปรดระบุ password'
                        //},
                        identical: {
                            field: 'ConfirmPassword',
                            message: 'password & confirm password invalid'
                        }
                    }
                },
                ConfirmPassword: {
                    validators: {
                        //notEmpty: {
                        //    message: 'โปรดระบุ confirm'
                        //},
                        identical: {
                            field: 'Password',
                            message: 'password & confirm password invalid'
                        }
                    }
                }
            },
            submitHandler: function (validator, form, submitButton) {
                register.saveRegister();
            }
        });
        $("#btn-reset").click(function () {
            $('#frm-register').data('bootstrapValidator').resetForm();
        });
    },
    saveRegister: function () {

        var datastring = $("#frm-register").serialize();

        var formData = datastring + '&' + $.param({
        });

        app.loading(true);

        $.ajax({
            url: $("#frm-register").attr("action"),
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    setTimeout(function () {
                        window.location = baseUrl + 'Register/Manage?id=' + res.id;
                    }, 1000);
                });
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: formData
        });
        return false;
    },
    setProjectQuota: (res) => {
        $("#partial-quota-modal").empty().html(res.html);
        $('input.integer').autoNumeric({ aSep: ',', aNeg: '-', vMin: 0, mNum: 10, aPad: false });
        $("#form-quota").unbind('submit').submit(() => {
            register.saveProjectQuota();
            return false;
        });

        $('#quota-modal').modal({
            show: 'true',
            backdrop: 'static'
        });
    },
    saveProjectQuota: function () {

        var datastring = $("#form-quota").serialize();

        var formData = datastring + '&' + $.param({
            RegisterID: register_id
        });

        app.loading(true);

        $.ajax({
            url: baseUrl + 'register/SaveProjectQuota',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    $('#quota-modal').hide();
                    setTimeout(function () {                      
                        window.location.reload();
                    }, 1000);
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