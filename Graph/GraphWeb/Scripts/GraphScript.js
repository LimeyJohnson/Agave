// GraphScript.js
(function($){
Type.registerNamespace('GraphScript');GraphScript.GraphScript=function(){}
GraphScript.GraphScript.drawChart=function(){var $0=new google.visualization.DataTable();$0.addRows([['Mushrooms',1],['Onions',1],['Olives',1],['Zucchini',1],['Pepperoni',2]]);var $1={};$1.title='How Much Pizza I Ate Last Night';$1.width=400;$1.height=300;var $2=new google.visualization.PieChart(document.getElementById('chart_div'));$2.draw($0,$1);}
GraphScript.GraphScript.registerClass('GraphScript.GraphScript');(function(){Office.initialize=function($p1_0){
};google.load('visualization','1.0',{packages:['corechart']});google.setOnLoadCallback(GraphScript.GraphScript.drawChart);})();
})(jQuery);// This script was generated using Script# v0.7.4.0
