//! GraphScript.debug.js
//

(function($) {

Type.registerNamespace('GraphScript');

////////////////////////////////////////////////////////////////////////////////
// GraphScript.GraphScript

GraphScript.GraphScript = function GraphScript_GraphScript() {
}
GraphScript.GraphScript.drawChart = function GraphScript_GraphScript$drawChart() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Toppings');
    data.addColumn('number', 'Slices');
    data.addRows([ [ 'Mushrooms', 1 ], [ 'Onions', 1 ], [ 'Olives', 1 ], [ 'Zucchini', 1 ], [ 'Pepperoni', 2 ] ]);
    var options = {};
    options.title = 'How Much Pizza I Ate Last Night';
    options.width = 400;
    options.height = 300;
    var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}


GraphScript.GraphScript.registerClass('GraphScript.GraphScript');
(function () {
    Office.initialize = function(init) {
    };
    google.load('visualization', '1.0', { packages: [ 'corechart' ] });
    google.setOnLoadCallback(GraphScript.GraphScript.drawChart);
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
