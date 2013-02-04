// AgaveScript.js
(function($){
Type.registerNamespace('OfficeApp1Script');OfficeApp1Script.AgaveScript=function(){}
OfficeApp1Script.AgaveScript.setFieldBinding=function(){var $0=$('#BindingField').val();Office.context.document.bindings.addFromNamedItemAsync($0,Office.BindingType.Text,OfficeApp1Script.AgaveScript.$1($0+OfficeApp1Script.AgaveScript.fieldBindingSuffix));}
OfficeApp1Script.AgaveScript.getFieldBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.fieldBindingSuffix;Office.select('bindings#'+$0).getDataAsync(function($p1_0){
if($p1_0.status==='succeeded'){$('#selectedDataTxt').val($p1_0.value);}});}
OfficeApp1Script.AgaveScript.setFieldData=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.fieldBindingSuffix;var $1=$('#selectedDataTxt').val();Office.select('bindings#'+$0).setDataAsync($1,OfficeApp1Script.AgaveScript.$0('text'));}
OfficeApp1Script.AgaveScript.setTableBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.tableBindingSuffix;Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Matrix,OfficeApp1Script.AgaveScript.$1($0));}
OfficeApp1Script.AgaveScript.getTableBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.tableBindingSuffix;Office.select('bindings#'+$0).getDataAsync(OfficeApp1Script.AgaveScript.$0('table'),function($p1_0){
alert('Break point');});}
OfficeApp1Script.AgaveScript.$0=function($p0){var $0={};$0.coercionType=$p0;return $0;}
OfficeApp1Script.AgaveScript.$1=function($p0){var $0={};$0.id=$p0;return $0;}
OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');OfficeApp1Script.AgaveScript.fieldBindingSuffix='FieldBinding';OfficeApp1Script.AgaveScript.rowBindingSuffix='RowBinding';OfficeApp1Script.AgaveScript.tableBindingSuffix='TableBinding';(function(){Office.initialize=function($p1_0){
};})();
})(jQuery);// This script was generated using Script# v0.7.4.0
