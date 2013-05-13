Office.initialize = function () {
    var tableData = new Office.TableData();
    tableData.headers = ['FirstName', 'LastName'];
    tableData.rows = [['James', 'Johnson'], ['Andrew', 'Johnson']];
    var sdo = {};
    sdo.coercionType = Office.CoercionType.Table;
    Office.context.document.setSelectedDataAsync(tableData, sdo, function (result) {
        var bOptions = {};
        bOptions.id = 'TestTable';
        Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, bOptions, function (newResult) {
            var gOptions = {};
            gOptions.coercionType = Office.CoercionType.Matrix;
            Office.select('Bindings#TestTable').getDataAsync(gOptions, function (newResults) {
                document.createElement('myEvent');
            });
        });
    });
 }