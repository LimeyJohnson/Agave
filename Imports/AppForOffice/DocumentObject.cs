// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{

    [ScriptImport, ScriptIgnoreNamespace]
    public class DocumentObject
    {
        public void AddHandlerAsync(EventType eventType, Action<BindingEventArgs> handler) { }
        public void SetSelectedDataAsync(string[][] data, GetDataAsyncOptions options, Action<ASyncResult> callback) { }
        public void SetSelectedDataAsync(string[][] data, Action<ASyncResult> callback) { }
        public void SetSelectedDataAsync(TableData td, GetDataAsyncOptions options, Action<ASyncResult> callback) { }
        public BindingsObject Bindings;
        public DocumentMode Mode;
        public SettingsObject Settings;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.DocumentMode")]
    public enum DocumentMode
    {
        [ScriptName(PreserveCase = true)]
        ReadOnly,
        [ScriptName(PreserveCase = true)]
        ReadWrite
    }
}
