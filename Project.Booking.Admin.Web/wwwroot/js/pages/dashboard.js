var select_project, $select_project;
var projectVal;
var dashboard = {
    init: () => {
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

        dashboard.initialSelectize();

        $("#form-dashboard-search").submit(() => {
            dashboard.refreshData();
            return false;
        });
    },
    initialSelectize: () => {
        $('#BUIDs').selectize({
            maxItems: null,
            valueField: 'ID',
            labelField: 'Name',
            searchField: ['Name'],
            create: false,
            onChange: (values) => {
                dashboard.buChange(values);
            }
        });

        $select_project = $('#ProjectIDs').selectize({
            persist: true,
            valueField: 'projectID',
            labelField: 'projectName',
            searchField: ['projectName'],
            options: [],
            //preload: true,
            createOnBlur: true,
            create: false,
            maxItems: null
        });

        select_project = $select_project[0].selectize;
    },
    buChange: (buIDs) => {
        dashboard.projectLoading(buIDs);
    },
    projectLoading: (buIDs) => {
        select_project.disable();
        select_project.clear();
        select_project.clearOptions();
        select_project.load(function (callback) {
            $.ajax({
                url: baseUrl + 'Dashboard/GetProjectByBU',
                type: 'POST',
                dataType: 'json',
                data: {
                    BUIDs: buIDs
                },
                success: function (results) {
                    callback(results.data);
                    select_project.enable();
                    //select_project.setValue(indicationVal, false);
                    //select_indication.refreshOptions();
                },
                error: function () {
                    callback();
                }
            });
        });
    },
    refreshData: () => {
        app.loading(true);

        var datastring = $("#form-dashboard-search").serialize();
        var formData = datastring + '&' + $.param({
            BUIDs: $.trim($('#BUIDs').val()),
            ProjectIDs: $.trim($('#ProjectIDs').val())
        });

        $.ajax({
            url: baseUrl + 'Dashboard/GetData',
            type: 'post',
            dataType: 'json',
            //processData: false,
            //contentType: false,
            success: function (res) {
                app.ajaxComplete(res, function () {
                    $("#div-partial-section1").empty().html(res.section1);
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