// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to buttons
Office.initialize = function (reason) {
    $(document).ready(function () {
        $('#getDataBtn').click(function () { getData('#selectedDataTxt'); });

    });
};

// Writes data from textbox to the current selection in the document
function getData(elementId) {
    Office.context.document.bindings.addFromNamedItemAsync("q_Office_OfficesPartOfMove", Office.BindingType.Table, { id: 'firstname' }, function (result) {$(elementId).val(result.value); })
}

