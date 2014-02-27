// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.TableData")]
    public sealed class TableData
    {
        public object[] Headers;
        [ScriptName("headers")]
        public Array[] HeadersDouble;
        public object[][] Rows;
    }
}
