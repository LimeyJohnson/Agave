//! AgaveScript.debug.js
//

(function($) {

Type.registerNamespace('OfficeApp1Script');

////////////////////////////////////////////////////////////////////////////////
// OfficeApp1Script.Etsy

OfficeApp1Script.Etsy = function OfficeApp1Script_Etsy() {
}


////////////////////////////////////////////////////////////////////////////////
// OfficeApp1Script.AgaveScript

OfficeApp1Script.AgaveScript = function OfficeApp1Script_AgaveScript() {
    /// <field name="fieldBindingSuffix" type="String" static="true">
    /// </field>
    /// <field name="rowBindingSuffix" type="String" static="true">
    /// </field>
    /// <field name="tableBindingSuffix" type="String" static="true">
    /// </field>
    /// <field name="rowBinding" type="String" static="true">
    /// </field>
}
OfficeApp1Script.AgaveScript.logon = function OfficeApp1Script_AgaveScript$logon() {
    var options = {};
    options.scope = 'email, user_likes, publish_stream';
    FB.login(function(response) {
    }, options);
}
OfficeApp1Script.AgaveScript.setFieldBinding = function OfficeApp1Script_AgaveScript$setFieldBinding() {
    var bindingID = $('#BindingField').val();
    Office.context.document.bindings.addFromNamedItemAsync(bindingID, Office.BindingType.Text, OfficeApp1Script.AgaveScript._createOptions(bindingID + OfficeApp1Script.AgaveScript.fieldBindingSuffix));
}
OfficeApp1Script.AgaveScript.getFieldBinding = function OfficeApp1Script_AgaveScript$getFieldBinding() {
    var bindingID = $('#BindingField').val() + OfficeApp1Script.AgaveScript.fieldBindingSuffix;
    Office.select('bindings#' + bindingID).getDataAsync(function(result) {
        if (result.status === 'succeeded') {
            $('#selectedDataTxt').val(result.value);
        }
    });
}
OfficeApp1Script.AgaveScript.setFieldData = function OfficeApp1Script_AgaveScript$setFieldData() {
    var bindingID = $('#BindingField').val() + OfficeApp1Script.AgaveScript.fieldBindingSuffix;
    var data = $('#selectedDataTxt').val();
    Office.select('bindings#' + bindingID).setDataAsync(data, OfficeApp1Script.AgaveScript._createCoercionType('text'));
}
OfficeApp1Script.AgaveScript.setTableBinding = function OfficeApp1Script_AgaveScript$setTableBinding() {
    var bindingID = $('#BindingField').val() + OfficeApp1Script.AgaveScript.tableBindingSuffix;
    Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Matrix, OfficeApp1Script.AgaveScript._createOptions(bindingID));
}
OfficeApp1Script.AgaveScript.getTableBinding = function OfficeApp1Script_AgaveScript$getTableBinding() {
    var bindingID = $('#BindingField').val() + OfficeApp1Script.AgaveScript.tableBindingSuffix;
    Office.select('bindings#' + bindingID).getDataAsync(OfficeApp1Script.AgaveScript._createCoercionType('table'), function(result) {
        alert('Break point');
    });
}
OfficeApp1Script.AgaveScript._createCoercionType = function OfficeApp1Script_AgaveScript$_createCoercionType(type) {
    /// <param name="type" type="String">
    /// </param>
    /// <returns type="Object"></returns>
    var options = {};
    options.coercionType = type;
    return options;
}
OfficeApp1Script.AgaveScript._createOptions = function OfficeApp1Script_AgaveScript$_createOptions(ID) {
    /// <param name="ID" type="String">
    /// </param>
    /// <returns type="Object"></returns>
    var options = {};
    options.id = ID;
    return options;
}
OfficeApp1Script.AgaveScript.populateRowCombo = function OfficeApp1Script_AgaveScript$populateRowCombo() {
    var items = [];
    OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.rowBinding).getDataAsync(function(result) {
        var combo = $('#rows');
        combo.html('');
        var fields = result.value[0][0];
        $.each(fields, function(i, o) {
            var html = '<option>' + o.toString() + '</option>';
            combo.append(html);
        });
    });
}
OfficeApp1Script.AgaveScript.select = function OfficeApp1Script_AgaveScript$select(bindingID) {
    /// <param name="bindingID" type="String">
    /// </param>
    /// <returns type="AgaveApi.SelectObject"></returns>
    return Office.select('bindings#' + bindingID);
}
OfficeApp1Script.AgaveScript.setBinding = function OfficeApp1Script_AgaveScript$setBinding(bindingID, type) {
    /// <param name="bindingID" type="String">
    /// </param>
    /// <param name="type" type="Office.BindingType">
    /// </param>
    Office.context.document.bindings.addFromNamedItemAsync(bindingID, type, OfficeApp1Script.AgaveScript._createOptions(bindingID));
}
OfficeApp1Script.AgaveScript.getRowValues = function OfficeApp1Script_AgaveScript$getRowValues() {
    OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.rowBinding).getDataAsync(function(result) {
        var combo = $('#results');
        combo.html('');
        var fields = result.value[1];
        $.each(fields, function(i, o) {
            var fieldNames = result.value[0][0];
            var appendText = fieldNames[i] + ' : ' + ((o != null) ? o.toString() : 'JSNULL') + '<br/>';
            combo.append(appendText);
        });
    });
}


OfficeApp1Script.Etsy.registerClass('OfficeApp1Script.Etsy');
OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');
OfficeApp1Script.AgaveScript.fieldBindingSuffix = 'FieldBinding';
OfficeApp1Script.AgaveScript.rowBindingSuffix = 'RowBinding';
OfficeApp1Script.AgaveScript.tableBindingSuffix = 'TableBinding';
OfficeApp1Script.AgaveScript.rowBinding = 'Row';
(function () {
    Office.initialize = function(reason) {
        OfficeApp1Script.AgaveScript.setBinding(OfficeApp1Script.AgaveScript.rowBinding, Office.BindingType.Matrix);
        OfficeApp1Script.AgaveScript.populateRowCombo();
        OfficeApp1Script.AgaveScript.getRowValues();
        Office.context.document.addHandlerAsync(Office.EventType.DocumentSelectionChanged, function(args) {
            $('#eventResults').append('Event fired: ' + args.document.mode.toString() + ' Type: ' + args.type.toString());
            OfficeApp1Script.AgaveScript.getRowValues();
        });
    };
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
