// AgaveScript.js
(function($){
Type.registerNamespace('OfficeApp1Script');OfficeApp1Script.Etsy=function(){}
OfficeApp1Script.AgaveScript=function(){}
OfficeApp1Script.AgaveScript.logon=function(){var $0={};$0.scope='email, user_likes, publish_stream';FB.login(function($p1_0){
},$0);}
OfficeApp1Script.AgaveScript.setFieldBinding=function(){var $0=$('#BindingField').val();Office.context.document.bindings.addFromNamedItemAsync($0,Office.BindingType.Text,OfficeApp1Script.AgaveScript.$1($0+OfficeApp1Script.AgaveScript.fieldBindingSuffix));}
OfficeApp1Script.AgaveScript.getFieldBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.fieldBindingSuffix;Office.select('bindings#'+$0).getDataAsync(function($p1_0){
if($p1_0.status===Office.AsyncResultStatus.Succeeded){$('#selectedDataTxt').val($p1_0.value);}});}
OfficeApp1Script.AgaveScript.setFieldData=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.fieldBindingSuffix;var $1=$('#selectedDataTxt').val();Office.select('bindings#'+$0).setDataAsync($1,OfficeApp1Script.AgaveScript.$0(Office.CoercionType.Text));}
OfficeApp1Script.AgaveScript.setTableBinding=function(){var $0=$('#BindingField').val()+OfficeApp1Script.AgaveScript.tableBinding;Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Matrix,OfficeApp1Script.AgaveScript.$1($0));}
OfficeApp1Script.AgaveScript.$0=function($p0){var $0={};$0.coercionType=$p0;return $0;}
OfficeApp1Script.AgaveScript.$1=function($p0){var $0={};$0.id=$p0;return $0;}
OfficeApp1Script.AgaveScript.populateRowCombo=function(){var $0=[];OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.rowBinding).getDataAsync(OfficeApp1Script.AgaveScript.$0(Office.CoercionType.Matrix),function($p1_0){
var $1_0=$('#rows');$1_0.html('');var $1_1=$p1_0.value[0][0];$.each($1_1,function($p2_0,$p2_1){
var $2_0='<option>'+$p2_1.toString()+'</option>';$1_0.append($2_0);});});}
OfficeApp1Script.AgaveScript.select=function(bindingID){return Office.select('bindings#'+bindingID);}
OfficeApp1Script.AgaveScript.setBinding=function(bindingID,type){if(type===Office.BindingType.Matrix){Office.context.document.bindings.addFromSelectionAsync(type,OfficeApp1Script.AgaveScript.$1(bindingID),OfficeApp1Script.AgaveScript.checkAsyncCallbackForErrors);}else{Office.context.document.bindings.addFromNamedItemAsync(bindingID,type,OfficeApp1Script.AgaveScript.$1(bindingID),OfficeApp1Script.AgaveScript.checkAsyncCallbackForErrors);}}
OfficeApp1Script.AgaveScript.getRowValues=function(){OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.rowBinding).getDataAsync(function($p1_0){
if($p1_0.status===Office.AsyncResultStatus.Succeeded){var $1_0=$('#row');$1_0.html('');var $1_1=$p1_0.value[1];$.each($1_1,function($p2_0,$p2_1){
var $2_0=$p1_0.value[0][0];var $2_1=$2_0[$p2_0]+' : '+(($p2_1!=null)?$p2_1.toString():'JSNULL')+'<br/>';$1_0.append($2_1);});}else{OfficeApp1Script.AgaveScript.setError('GetDataAsync in GetRowValues() failed');}});}
OfficeApp1Script.AgaveScript.getTableBinding=function(){OfficeApp1Script.AgaveScript.select(OfficeApp1Script.AgaveScript.tableBinding).getDataAsync(function($p1_0){
if($p1_0.status===Office.AsyncResultStatus.Succeeded){var $1_0=$('#table');$1_0.html('');var $1_1=$p1_0.value[1];$.each($1_1,function($p2_0,$p2_1){
var $2_0=$p1_0.value[0][0];var $2_1=$2_0[$p2_0]+' : '+(($p2_1!=null)?$p2_1.toString():'JSNULL')+'<br/>';$1_0.append($2_1);});}else{OfficeApp1Script.AgaveScript.setError('GetDataAsync in GetRowValues() failed');}});}
OfficeApp1Script.AgaveScript.checkAsyncCallbackForErrors=function(result){if(result.status!==Office.AsyncResultStatus.Succeeded){OfficeApp1Script.AgaveScript.setError('ASync Result Failed');}}
OfficeApp1Script.AgaveScript.setError=function(errorText){$('#error').append(errorText);}
OfficeApp1Script.Etsy.registerClass('OfficeApp1Script.Etsy');OfficeApp1Script.AgaveScript.registerClass('OfficeApp1Script.AgaveScript');OfficeApp1Script.AgaveScript.fieldBindingSuffix='FieldBinding';OfficeApp1Script.AgaveScript.rowBindingSuffix='RowBinding';OfficeApp1Script.AgaveScript.tableBinding='TableBinding';OfficeApp1Script.AgaveScript.rowBinding='DataTypes';(function(){Office.initialize=function($p1_0){
OfficeApp1Script.AgaveScript.setBinding(OfficeApp1Script.AgaveScript.rowBinding,Office.BindingType.Matrix);OfficeApp1Script.AgaveScript.setBinding(OfficeApp1Script.AgaveScript.tableBinding,Office.BindingType.Table);OfficeApp1Script.AgaveScript.populateRowCombo();OfficeApp1Script.AgaveScript.getRowValues();Office.context.document.addHandlerAsync(Office.EventType.DocumentSelectionChanged,function($p2_0){
$('#eventResults').append('Event fired: '+$p2_0.document.mode.toString()+' Type: '+$p2_0.type.toString()+'<br/>');OfficeApp1Script.AgaveScript.getRowValues();});};})();
})(jQuery);// This script was generated using Script# v0.7.4.0
