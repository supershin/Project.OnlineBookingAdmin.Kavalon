var user = {
    init: function () {
        
        user.validateData();
    },
    validateData: function () {
        $('#frm-user').bootstrapValidator({
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
                            message: 'โปรดระบุ first name'
                        }
                    }                    
                },
                LastName: {
                    validators: {
                        notEmpty: {
                            message: 'โปรดระบุ last name'
                        }
                    }
                },
                Username: {
                    validators: {
                        notEmpty: {
                            message: 'โปรดระบุ username / email'
                        }
                    }
                },
                Mobile: {
                    validators: {
                        notEmpty: {
                            message: 'โปรดระบุ mobile'
                        }
                    }
                },                
                DepartmentID: {
                    validators: {
                        notEmpty: {
                            message: 'โปรดระบุ department'
                        }
                    }
                },
                RoleID: {
                    validators: {
                        notEmpty: {
                            message: 'โปรดระบุ role'
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
                            message: 'password & confirm password ไม่ตรงกัน'
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
                            message: 'password & confirm password ไม่ตรงกัน'
                        }
                    }
                }
            },
            submitHandler: function (validator, form, submitButton) {                
                user.saveUser();
            }
        });
        $("#btn-reset").click(function () {
            $('#frm-user').data('bootstrapValidator').resetForm();            
        });
    },
    saveUser: function () {

        var datastring = $("#frm-user").serialize();        

        var formData = datastring + '&' + $.param({
        });

        app.loading(true);

        $.ajax({
            url: $("#frm-user").attr("action"),
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    setTimeout(function () {
                        window.location = baseUrl + 'User';
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