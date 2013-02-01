// OfficeApp1Script.js
(function($){
Type.registerNamespace('OfficeApp1Script');AgaveScript=function(){}
AgaveScript.Office=function(initEnum){alert('something');}
AgaveScript.setBinding=function(){var $0=$('#BindingField').val();var $1=new AgaveApi.NameItemAsyncOptions();$1.set_id($0+'binding');Office.context.document.bindings.addFromNamedItemAsync($0,AgaveApi.Office.BindingType.text,$1);}
AgaveScript.registerClass('AgaveScript');})(jQuery);// This script was generated using Script# v0.7.4.0
