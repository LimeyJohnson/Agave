// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to buttons
Office.initialize = function (reason) {
    $(document).ready(function () {
        $('#getDataBtn').click(function () { getData('#selectedDataTxt'); });

        // If setSelectedDataAsync method is supported by the host application
        // setDatabtn is hooked up to call the method else setDatabtn is removed
        if (Office.context.document.setSelectedDataAsync) {
            $('#setDataBtn').click(function () { setData('#selectedDataTxt'); });
        }
        else {
            $('#setDataBtn').remove();
        }
    });
};

// Writes data from textbox to the current selection in the document
function setData(elementId) {
    Office.context.document.setSelectedDataAsync($(elementId).val());
}

// Reads the data from current selection of the document and displays it in a textbox
function getData(elementId) {
    Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
    function (result) {
        if (result.status === 'succeeded') {
            $(elementId).val(result.value);
        }
    });
}