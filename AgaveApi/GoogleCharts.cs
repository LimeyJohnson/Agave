using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
namespace GoogleCharts
{
    [Imported, IgnoreNamespace, ScriptName("google")]
    public static class Google
    {
        public static extern void Load(string action, string version, Dictionary options);
        public static extern void SetOnLoadCallback(Action action);
    }
    [Imported, IgnoreNamespace, ScriptName("google.visualization.DataTable")]
    public class DataTable
    {
        public extern void AddColumn(string dataType, string columnName);
        public extern void AddRows(object[][] data);
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class ChartOptions
    {
        public string Title;
        public int Width;
        public int Height;
    }
    [Imported, IgnoreNamespace, ScriptName("google.visualization.PieChart")]
    public class PieChart
    {
        public extern PieChart(object id);
        public extern void Draw(DataTable data, ChartOptions options);
    }
}

