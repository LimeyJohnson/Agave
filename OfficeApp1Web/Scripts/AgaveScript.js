// AgaveScript.js
(function($){
Type.registerNamespace('OfficeApp1Script');OfficeApp1Script.AgaveScript=function(){}
OfficeApp1Script.AgaveScript.setBinding=function(){var $0=$('#BindingField').val();var $1={};$1.id=$0+'binding';Office.context.document.bindings.addFromNamedItemAsync($0,Office.BindingType.Text,$1);}
OfficeApp1Script.AgaveScript.getBinding=function(){var $0=$('#BindingField').val();Office.select('bindings#'+$0+'binding').getDataAsync(function($p1_0){
if($p1_0.status==='succeeded'){$('#selectedDataTxt').val($p1_0.value);}});}
OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');(function(){Office.initialize=function($p1_0){
};})();
})(jQuery);// This script was generated using Script# v0.7.4.0
