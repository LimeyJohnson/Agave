//! AgaveScript.debug.js
//

(function($) {

Type.registerNamespace('OfficeApp1Script');

////////////////////////////////////////////////////////////////////////////////
// OfficeApp1Script.AgaveScript

OfficeApp1Script.AgaveScript = function OfficeApp1Script_AgaveScript() {
}
OfficeApp1Script.AgaveScript.setBinding = function OfficeApp1Script_AgaveScript$setBinding() {
    var bindingID = $('#BindingField').val();
    var options = {};
    options.id = bindingID + 'binding';
    Office.context.document.bindings.addFromNamedItemAsync(bindingID, Office.BindingType.Text, options);
}
OfficeApp1Script.AgaveScript.getBinding = function OfficeApp1Script_AgaveScript$getBinding() {
    var bindingID = $('#BindingField').val();
    Office.select('bindings#' + bindingID + 'binding').getDataAsync(function(result) {
        if (result.status === 'succeeded') {
            $('#selectedDataTxt').val(result.value);
        }
    });
}


OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');
(function () {
    Office.initialize = function(reason) {
    };
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
