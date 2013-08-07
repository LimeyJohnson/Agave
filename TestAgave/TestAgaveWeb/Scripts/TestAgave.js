// This function is run when the app is ready to start interacting with the host application
// It ensures the DOM is ready before adding click handlers to buttons
Office.initialize = function (reason) {
    $(document).ready(function () {
        GraphTypes = { Bar: 1, Pie: 2 }
        GraphType = GraphTypes.Bar;
        $("#graphBtn").click(BindAndGraph);
        d3.selectAll("input").on("change", GraphTypeChange);
    });
};
var GraphType;
// Writes data from textbox to the current selection in the document
function BindAndGraph(eventArgs) {
    Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, { id: 'newid' }, function (callback) {
        if (callback.status == "succeeded") {
            GetDataAndGraph();
            Office.select("bindings#newid").addHandlerAsync(Office.EventType.BindingDataChanged, GetDataAndGraph);
        }
    });
}
function GraphTypeChange() {
    GraphType = parseInt(this.value);
    GetDataAndGraph();
}
function GetDataAndGraph() {
    Office.select("bindings#newid").getDataAsync({ coercionType: Office.CoercionType.Table, filterType: Office.FilterType.OnlyVisible }, function (getDataCB) {
        if (getDataCB.status = "succeeded") {
            GraphIt(GraphType, getDataCB.value, "xAxis", "yAxis");
        }
    });
    function GraphIt(graphType, tableData, xAxisText, yAxisText) {
        d3.select("svg").remove();
        var width = 700, height = 700, radius = Math.min(width, height) / 2;
        var margin = { top: 20, right: 20, bottom: 30, left: 40 };
        switch (graphType) {
            case GraphTypes.Pie:
                var color = d3.scale.category20();

                var arc = d3.svg.arc()
                    .outerRadius(radius - 10)
                    .innerRadius(0);

                var pie = d3.layout.pie()
                    .sort(d3.ascending)
                    .value(function (d) { return d[1]; });

                var svg = d3.select("body").append("svg")
                    .attr("width", width)
                    .attr("height", height)
                  .append("g")
                    .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

                var g = svg.selectAll(".arc")
                    .data(pie(tableData.rows))
                  .enter().append("g")
                    .attr("class", "arc");

                var path = g.append("path")
                    .attr("d", arc)
                    .style("fill", function (d) { return color(d.data[0]); });

                g.append("text")
                    .attr("transform", function (d) { return "translate(" + arc.centroid(d) + ")"; })
                    .attr("dy", ".35em")
                    .style("text-anchor", "middle")
                    .text(function (d) { return d.data[0]; });
                break;
            case GraphTypes.Bar:
                var x = d3.scale.ordinal()
                    .rangeRoundBands([0, width - margin.left - margin.right], .1);

                var y = d3.scale.linear()
                    .range([height, 0]);

                var xAxis = d3.svg.axis()
                    .scale(x)
                    .orient("bottom");

                var yAxis = d3.svg.axis()
                    .scale(y)
                    .orient("left")


                var svg = d3.select("body").append("svg")
                    .attr("width", width)
                    .attr("height", height)
                  .append("g").attr("transform", "translate(" + margin.left + "," + margin.top + ")");

                x.domain(tableData.rows.map(function (d) { return d[0]; }));
                y.domain([0, d3.max(tableData.rows, function (d) { return d[1]; })]);

                svg.append("g")
                    .attr("class", "x axis")
                    .attr("transform", "translate(0," + height + ")")
                    .call(xAxis);

                svg.append("g")
                    .attr("class", "y axis")
                    .call(yAxis)
                  .append("text")
                    .attr("transform", "rotate(-90)")
                    .attr("y", 6)
                    .attr("dy", ".71em")
                    .style("text-anchor", "end")
                    .text(xAxisText);

                svg.selectAll(".bar")
                    .data(tableData.rows)
                  .enter().append("rect")
                    .attr("class", "bar")
                    .attr("x", function (d) { return x(d[0]); })
                    .attr("width", x.rangeBand())
                    .attr("y", function (d) { return y(d[1]); })
                    .attr("height", function (d) { return height - y(d[1]); });
                break;


        }
    }
}
// Reads the data from current selection of the document and displays it in a textbox
