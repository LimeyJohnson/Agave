// GetDataAsyncOptions.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class BindingEventArgs
    {
        public BindingObject Binding;
        public EventType Type;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.EventType")]
    public enum EventType
    {
        [ScriptName(PreserveCase = true)]
        DocumentSelectionChanged,
        [ScriptName(PreserveCase = true)]
        BindingSelectionChanged,

        [ScriptName(PreserveCase = true)]
        BindingDataChanged
    }
}
