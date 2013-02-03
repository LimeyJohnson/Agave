//! OfficeApp1Script.debug.js
//

(function($) {

Type.registerNamespace('OfficeApp1Script');

////////////////////////////////////////////////////////////////////////////////
// AgaveScript

AgaveScript = function AgaveScript() {
}
AgaveScript.setBinding = function AgaveScript$setBinding() {
    var bindingID = $('#BindingField').val();
    var options = new AgaveApi.NameItemAsyncOptions();
    options.set_id(bindingID + 'binding');
    Office.context.document.bindings.addFromNamedItemAsync(bindingID, AgaveApi.Office.BindingType.text, options);
}
AgaveScript.getBinding = function AgaveScript$getBinding() {
    var bindingID = $('#BindingField').val();
    Office.select('bindings#' + bindingID + 'binding').getDataAsync(function(result) {
        if (result.status === 'succeeded') {
            $('#selectedDataTxt').val(result.value);
        }
    });
}


AgaveScript.registerClass('AgaveScript');
(function () {
    Office.intialize = function(reason) {
    };
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
