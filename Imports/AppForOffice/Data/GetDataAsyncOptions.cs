// GetDataAsyncOptions.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{

    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class GetDataAsyncOptions
    {
        public CoercionType CoercionType;
        public ValueFormat ValueFormat;
        public FilterType FilterType;
        public int StartRow;
        public int StartColumn;
        public int RowCount;
        public int ColumnCount;
        public object AsyncContext;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.FilterType")]
    public enum FilterType
    {
        [ScriptName(PreserveCase = true)]
        All,
        [ScriptName(PreserveCase = true)]
        OnlyVisible,
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.ValueFormat")]
    public enum ValueFormat
    {
        [ScriptName(PreserveCase = true)]
        Formatted,
        [ScriptName(PreserveCase = true)]
        UnFormatted,
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.CoercionType")]
    public enum CoercionType
    {
        [ScriptName(PreserveCase = true)]
        Text,
        [ScriptName(PreserveCase = true)]
        Matrix,
        [ScriptName(PreserveCase = true)]
        Table
    }
}
