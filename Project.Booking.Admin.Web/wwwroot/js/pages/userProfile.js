var userProfile = {
    init: function () {

        userProfile.validateData();
    },
    validateData: function () {
        $('#frm-user-profile').bootstrapValidator({
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
                userProfile.saveUserProfile(form);
            }
        });
        $("#btn-reset").click(function () {
            $('#frm-user-profile').data('bootstrapValidator').resetForm();
        });
    },
    saveUserProfile: function (form) {        
        app.loading(true);
        
        $.ajax({
            url: baseUrl + "User/SaveUserProfile",
            type: 'post',
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    setTimeout(function () {
                        window.location.reload();
                    }, 1000);
                });
                app.loading(false);
            },
            error: function (xhr, status, error) {
                window.location.reload();
            },
            data: new FormData(document.getElementById("frm-user-profile"))
        });
        return false;
    }
};