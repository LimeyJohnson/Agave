// FacebookScript.js
(function($){
Type.registerNamespace('FacebookScript');FacebookScript.FacebookScript=function(){}
FacebookScript.FacebookScript.login=function(){alert('You are here');;}
FacebookScript.FacebookScript.registerClass('FacebookScript.FacebookScript');Office.initialize=function($p1_0){
var $1_0={};$1_0.channelUrl='http://facebookagave.azurewebsites.net/pages/channel.ashx';$1_0.appId='263395420459543';$1_0.status=true;$1_0.cookie=false;FB.init($1_0);$('#FBLogin').click(function($p2_0){
var $2_0={};$2_0.scope='email, user_likes, publish_stream';FB.login(function($p3_0){
alert('We are logged in');},$2_0);});};})(jQuery);// This script was generated using Script# v0.7.6.0
