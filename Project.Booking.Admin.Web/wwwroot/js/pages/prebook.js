const prebook = {
    init: () => {
        $("#ProjectID").val(selectProjectID);
        $("#form-search").submit(() => {
            //app.reloadTable(tbl_list);
            //return false;
        });             

        prebook.initAction();
    },
    initAction: () => {        
        $("button[data-action='edit-prebook']").unbind('click').click(() => {            
            $("#modal-prebook").modal();
            return false;
        });
    }
};