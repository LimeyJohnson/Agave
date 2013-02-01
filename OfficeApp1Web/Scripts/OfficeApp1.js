// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to buttons
Office.initialize = function (reason) {

};

// Writes data from textbox to the current selection in the document
function setBinding() {
    var bindingID = $('#BindingField').val();
    Office.context.document.bindings.addFromNamedItemAsync(bindingID, Office.BindingType.Text, { id: bindingID+'binding' });
}

function getBinding() {
    var bindingID = $('#BindingField').val();
    Office.select('bindings#'+bindingID+'binding').getDataAsync(
        function (result) {
            if (result.status === 'succeeded') {
                $('#selectedDataTxt').val(result.value);
            }
        });
}

function getExcelData() {
    Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
        function (result) {
            if (result.status != "failed") {
                $('#selectedDataTxt').val(result.value);
            }
            
        });

}