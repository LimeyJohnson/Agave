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


OfficeApp1Script.Etsy.registerClass('OfficeApp1Script.Etsy');
OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');
OfficeApp1Script.AgaveScript.fieldBindingSuffix = 'FieldBinding';
OfficeApp1Script.AgaveScript.rowBindingSuffix = 'RowBinding';
OfficeApp1Script.AgaveScript.tableBindingSuffix = 'TableBinding';
(function () {
    Office.initialize = function(reason) {
        var options = {};
        options.appId = '263395420459543';
        options.status = true;
        options.cookie = false;
        options.xfbml = false;
        FB.init(options);
        FB.getLoginStatus(function(loginResponse) {
            if (loginResponse.status === 'connected') {
                (document.getElementById('image')).src = 'http://graph.facebook.com/' + loginResponse.authResponse.userID + '/picture';
            }
        });
    };
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
