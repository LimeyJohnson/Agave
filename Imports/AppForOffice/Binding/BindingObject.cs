// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    [ScriptImport, ScriptIgnoreNamespace]
    public sealed class BindingObject
    {
        public void GetDataAsync(Action<ASyncResult> callback) { }
        public void GetDataAsync(GetDataAsyncOptions options, Action<ASyncResult> callback) { }
        public void SetDataAsync(string data, GetDataAsyncOptions options) { }
        public void SetDataAsync(object[][] data, GetDataAsyncOptions options, Action<ASyncResult> callback) { }
        public void SetDataAsync(object[][] data, Action<ASyncResult> callback) { }
        public void SetDataAsync(TableData data, GetDataAsyncOptions options, Action<ASyncResult> callback) { }
        public void SetDataAsync(TableData data, GetDataAsyncOptions options) { }
        public void AddHandlerAsync(EventType eventType, Action<BindingEventArgs> handler) { }
        public void AddHandlerAsync(EventType eventType, Action<BindingSelectionChangedEventArgs> handler) { }
        public void DeleteAllDataValuesAsync(Action<ASyncResult> callback) { }
        public void AddColumnsAsync(TableData data, Action<ASyncResult> callback) { }
        public string Id;
        public EventType Type;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.BindingType")]
    public enum BindingType
    {
        [ScriptName(PreserveCase = true)]
        Text,
        [ScriptName(PreserveCase = true)]
        Matrix,
        [ScriptName(PreserveCase = true)]
        Table
    }
}
