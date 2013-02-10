// AgaveScript.js
(function($){
Type.registerNamespace('OfficeApp1Script');OfficeApp1Script.Etsy=function(){}
OfficeApp1Script.AgaveScript=function(){}
OfficeApp1Script.AgaveScript.logon=function(){var $0={};$0.scope='email, user_likes, publish_stream';FB.login(function($p1_0){
},$0);}
OfficeApp1Script.AgaveScript.setFieldBinding=function(){var $0=$('#BindingField').val();Office.context.document.bindings.addFromNamedItemAsync($0,Office.BindingType.Text,OfficeApp1Script.AgaveScript.$1($0+OfficeApp1Script.AgaveScript.fieldBindingSuffix));}
OfficeApp1Script.AgaveScript.getFieldBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.fieldBindingSuffix;Office.select('bindings#'+$0).getDataAsync(function($p1_0){
if($p1_0.status==='succeeded'){$('#selectedDataTxt').val($p1_0.value);}});}
OfficeApp1Script.AgaveScript.setFieldData=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.fieldBindingSuffix;var $1=$('#selectedDataTxt').val();Office.select('bindings#'+$0).setDataAsync($1,OfficeApp1Script.AgaveScript.$0('text'));}
OfficeApp1Script.AgaveScript.setTableBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.tableBindingSuffix;Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Matrix,OfficeApp1Script.AgaveScript.$1($0));}
OfficeApp1Script.AgaveScript.getTableBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.tableBindingSuffix;Office.select('bindings#'+$0).getDataAsync(OfficeApp1Script.AgaveScript.$0('table'),function($p1_0){
alert('Break point');});}
OfficeApp1Script.AgaveScript.$0=function($p0){var $0={};$0.coercionType=$p0;return $0;}
OfficeApp1Script.AgaveScript.$1=function($p0){var $0={};$0.id=$p0;return $0;}
OfficeApp1Script.AgaveScript.populateRowCombo=function(){var $0=[];OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.rowBinding).getDataAsync(function($p1_0){
var $1_0=$('#rows');$1_0.html('');var $1_1=$p1_0.value[0][0];$.each($1_1,function($p2_0,$p2_1){
var $2_0='<option>'+$p2_1.toString()+'</option>';$1_0.append($2_0);});});}
OfficeApp1Script.AgaveScript.select=function(bindingID){return Office.select('bindings#'+bindingID);}
OfficeApp1Script.AgaveScript.setBinding=function(bindingID,type){Office.context.document.bindings.addFromNamedItemAsync(bindingID,type,OfficeApp1Script.AgaveScript.$1(bindingID));}
OfficeApp1Script.AgaveScript.getRowValues=function(){OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.rowBinding).getDataAsync(function($p1_0){
var $1_0=$('#results');$1_0.html('');var $1_1=$p1_0.value[1];$.each($1_1,function($p2_0,$p2_1){
var $2_0=$p1_0.value[0][0];var $2_1=$2_0[$p2_0]+' : '+(($p2_1!=null)?$p2_1.toString():'JSNULL')+'<br/>';$1_0.append($2_1);});});}
OfficeApp1Script.Etsy.registerClass('OfficeApp1Script.Etsy');OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');OfficeApp1Script.AgaveScript.fieldBindingSuffix='FieldBinding';OfficeApp1Script.AgaveScript.rowBindingSuffix='RowBinding';OfficeApp1Script.AgaveScript.tableBindingSuffix='TableBinding';OfficeApp1Script.AgaveScript.rowBinding='Row';(function(){Office.initialize=function($p1_0){
OfficeApp1Script.AgaveScript.setBinding(OfficeApp1Script.AgaveScript.rowBinding,Office.BindingType.Matrix);OfficeApp1Script.AgaveScript.populateRowCombo();OfficeApp1Script.AgaveScript.getRowValues();Office.context.document.addHandlerAsync(Office.EventType.DocumentSelectionChanged,function($p2_0){
$('#eventResults').append('Event fired: '+$p2_0.document.mode.toString()+' Type: '+$p2_0.type.toString());OfficeApp1Script.AgaveScript.getRowValues();});};})();
})(jQuery);// This script was generated using Script# v0.7.4.0
