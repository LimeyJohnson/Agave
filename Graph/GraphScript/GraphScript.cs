// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using GoogleCharts;
using AgaveApi;

namespace GraphScript
{

    public static class GraphScript
    {
        static GraphScript()
        {
            Office.Initialize = delegate(InitializationEnum init) 
            {
                JQuery.Select("createbutton").click(new JQueryEvent(SetData));
            };
            Google.Load("visualization", "1.0", new Dictionary<string, string[]>("packages", new string[] { "corechart" }));
            Google.SetOnLoadCallback(DrawChart);
        }
        public static void DrawChart()
        {
            DataTable data = new DataTable();
            data.AddColumn("string", "Toppings");
            data.AddColumn("number", "Slices");
            data.AddRows(new object[][] { new object[] { "Mushrooms", 1 }, new object[] { "Onions", 1 }, new object[] { "Olives", 1 }, new object[] { "Zucchini", 1 }, new object[] { "Pepperoni", 2 } });
            ChartOptions options = new ChartOptions();
            options.Title = "How Much Pizza I Ate Last Night";
            options.Width = 400;
            options.Height = 300;
            PieChart chart = new PieChart(Document.GetElementById("chart_div"));
            chart.Draw(data,options);
            
        }
        public static void SetData(JQueryEventArgs eventArgs)
        {
        }
    }
}
