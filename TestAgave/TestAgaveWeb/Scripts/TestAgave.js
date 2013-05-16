//Global Variables
var tableBindingID = "myTableBinding";

// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to buttons
Office.initialize = function (reason) {
    $(document).ready(function () {
        $("#createTableBinding").click(createTableBinding);
        $("#getFilteredData").click(getFilteredTableData);
        $("#setDocumentData").click(setDocumentData);
        $("#getAllData").click(getAllTableData);
    });
};
function setDocumentData() {
    var myTable = new Office.TableData();
    myTable.headers = [[ "First Name", "Last Name", "Gender", "Age"]];
    myTable.rows = [["Andrew", "Johnson", "male", "26"], ["Matthew","Johnson","male","24"], ["Travis", "Cragg","male","26"], ["Ashley","Johnson","female","26"]];
    // Write table.
    Office.context.document.setSelectedDataAsync(myTable, { coercionType: Office.CoercionType.Table }, verifyCallback);
}
function createTableBinding() {
    setResult("Cleared");
    Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, { id: tableBindingID }, verifyCallback);
}
function getTableData() {
    setResult("Cleared");
    Office.select("bindings#"+tableBindingID).getDataAsync({ coercionType: Office.CoercionType.Table }, verifyCallback);
}
function getFilteredTableData() {
    setResult("Cleared");
    Office.select("bindings#" + tableBindingID).getDataAsync({ coercionType: Office.CoercionType.Table, filterType: Office.FilterType.OnlyVisible }, verifyCallback);
}
function getAllTableData() {
    setResult("Cleared");
    Office.select("bindings#" + tableBindingID).getDataAsync({ coercionType: Office.CoercionType.Table }, verifyCallback);
}
function verifyCallback(asyncResult) {
    if (asyncResult.status == Office.AsyncResultStatus.Failed) {
        if (asyncResult.error && asyncResult.error.name && asyncResult.error.message) {
            setResult("ERROR Name:" + asyncResult.error.name + " Message:" + asyncResult.error.message);
        }
    }
    if (asyncResult.status == Office.AsyncResultStatus.Succeeded) {
        if (asyncResult.value.headers) {
            //The callback has returned 
            
            var headers = "[" + asyncResult.value.headers[0].join() + "]";
            var rows = "";
            $.each(asyncResult.value.rows, function (x, v) {
                rows = rows + "[" + v.join() + "]\n";
            });
            setResult("Headers:\n" + headers + "\n\nRows:\n" + rows);
        }
        else {
            setResult("Call Succeeded");
        }
    }
}

function setResult(result) {
    $('#results').val(result);
}
