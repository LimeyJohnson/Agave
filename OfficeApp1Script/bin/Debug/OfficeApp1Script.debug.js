//! OfficeApp1Script.debug.js
//

(function($) {

Type.registerNamespace('OfficeApp1Script');

////////////////////////////////////////////////////////////////////////////////
// AgaveScript

AgaveScript = function AgaveScript() {
}
AgaveScript.Office = function AgaveScript$Office(initEnum) {
    /// <param name="initEnum" type="AgaveApi.InializationEnum">
    /// </param>
    alert('something');
}
AgaveScript.setBinding = function AgaveScript$setBinding() {
    var bindingID = $('#BindingField').val();
    var options = new AgaveApi.NameItemAsyncOptions();
    options.set_id(bindingID + 'binding');
    Office.context.document.bindings.addFromNamedItemAsync(bindingID, AgaveApi.Office.BindingType.text, options);
}


AgaveScript.registerClass('AgaveScript');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
