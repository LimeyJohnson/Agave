// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    public sealed class ASyncResult
    {
        public Error Error;
        public AsyncResultStatus Status;
        [ScriptName("value")]
        public string TextValue;
        [ScriptName("value")]
        public object[][] MatrixValue;
        [ScriptName("value")]
        public TableData TableValue;
        public Object Value;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.AsyncResultStatus")]
    public enum AsyncResultStatus
    {
        [ScriptName(PreserveCase = true)]
        Succeeded,
        [ScriptName(PreserveCase = true)]
        Failed,
    }
}
