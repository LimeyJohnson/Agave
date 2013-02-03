// OfficeApp1Script.js
(function($){
Type.registerNamespace('OfficeApp1Script');AgaveScript=function(){}
AgaveScript.setBinding=function(){var $0=$('#BindingField').val();var $1=new AgaveApi.NameItemAsyncOptions();$1.set_id($0+'binding');Office.context.document.bindings.addFromNamedItemAsync($0,AgaveApi.Office.BindingType.text,$1);}
AgaveScript.getBinding=function(){var $0=$('#BindingField').val();Office.select('bindings#'+$0+'binding').getDataAsync(function($p1_0){
if($p1_0.status==='succeeded'){$('#selectedDataTxt').val($p1_0.value);}});}
AgaveScript.registerClass('AgaveScript');(function(){Office.intialize=function($p1_0){
};})();
})(jQuery);// This script was generated using Script# v0.7.4.0
