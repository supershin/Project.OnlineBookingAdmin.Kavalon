const prebook = {
    init: () => {
        $("#ProjectID").val(selectProjectID);
        $("#form-search").submit(() => {
            prebook.getPrebook();
            return false;
        });
        $('#select-status').multiselect({
            enableFiltering: false,
            filterPlaceholder: 'filter status',
            nonSelectedText: 'select status',
            buttonWidth: '100%',
        });

        prebook.initAction();
    },
    initAction: () => {
        $("button[data-action='edit-prebook']").unbind('click').click(() => {
            $("#modal-prebook").modal();
            return false;
        });
    },
    getPrebook: () => {
        let data = {
            StatusIDs: prebook.setStatus()
        };
        alert(JSON.stringify(data));
        return false;
    },
    setStatus: () => {
        var selected = [];
        $('#select-status option:selected').each(function () {
            selected.push($(this).val());
        });
        return selected;
    }
};