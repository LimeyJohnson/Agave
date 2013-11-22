// GetDataAsyncOptions.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class BindingSelectionChangedEventArgs:EventArgs
    {
        public int ColumnCount;
        public int RowCount;
        public int StartRow;
        public int StartColumn;
    }
}
