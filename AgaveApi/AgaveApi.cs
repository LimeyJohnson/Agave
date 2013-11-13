// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace AgaveApi
{
    #region Delegates
    public delegate void ASyncResultCallBack(ASyncResult result);
    public delegate void InitReason(InitializationEnum inializationEnum);
    public delegate void EventHandler();
    public delegate void BindingDataChanged(BindingDataChangedEventArgs args);
    public delegate void DocumentSelectionChanged(DocumentSelectionChangedEventArgs args);
    public delegate void BindingSelectionChanged(BindingSelectionChangedEventArgs args);
    #endregion
    #region Classes
    [Imported, IgnoreNamespace]
    public static class Office
    {
        public static ContextObject Context;
        public static extern BindingObject Select(string binding);
        [IntrinsicProperty]
        public static extern InitReason Initialize { get; set; }
    }
    [Imported, IgnoreNamespace]
    public class ContextObject
    {
        public DocumentObject Document;
    }

    [Imported, IgnoreNamespace]
    public class BindingsObject
    {
        public extern void AddFromNamedItemAsync(string bindingID, BindingType bindingType, BindingOptions options);
        public extern void AddFromNamedItemAsync(string bindingID, BindingType bindingType, BindingOptions options, ASyncResultCallBack callback);
        public extern void AddFromSelectionAsync(BindingType bindingType, BindingOptions options);
        public extern void AddFromSelectionAsync(BindingType bindingType, BindingOptions options, ASyncResultCallBack callback);
        public extern void AddFromPromptAsync(BindingType bindingType, PromptBindingOptions options);
        public extern void AddFromPromptAsync(BindingType bindingType, PromptBindingOptions options, ASyncResultCallBack callback);
        public extern void GetByIdAsync(string id, ASyncResultCallBack callback);
    }
    [Imported, IgnoreNamespace]
    public class DocumentObject
    {
        public extern void AddHandlerAsync(EventType eventType, DocumentSelectionChanged handler);
        public extern void SetSelectedDataAsync(string[][] data, GetDataAsyncOptions options, ASyncResultCallBack callback);
        public extern void SetSelectedDataAsync(string[][] data, ASyncResultCallBack callback);
        public extern void SetSelectedDataAsync(TableData td, GetDataAsyncOptions options, ASyncResultCallBack callback);
        public BindingsObject Bindings;
        public DocumentMode Mode;
        public SettingsObject Settings;
    }
    [Imported, IgnoreNamespace]
    public class SettingsObject
    {
        public extern void Set(string name, object value);
        public extern void SaveAsync(ASyncResultCallBack callback);
        //  public extern void RefreshAsync (ASyncResultCallBack callback);
        public extern object Get(string name);
    }
    public sealed class BindingObject
    {
        public extern void GetDataAsync(ASyncResultCallBack callback);
        public extern void GetDataAsync(GetDataAsyncOptions options, ASyncResultCallBack callback);
        public extern void SetDataAsync(string data, GetDataAsyncOptions options);
        public extern void SetDataAsync(object[][] data, GetDataAsyncOptions options, ASyncResultCallBack callback);
        public extern void SetDataAsync(object[][] data, ASyncResultCallBack callback);
        public extern void SetDataAsync(TableData data, GetDataAsyncOptions options, ASyncResultCallBack callback);
        public extern void SetDataAsync(TableData data, GetDataAsyncOptions options);
        public extern void AddHandlerAsync(EventType eventType, BindingDataChanged handler);
        public extern void AddHandlerAsync(EventType eventType, BindingSelectionChanged handler);
        public extern void AddHandlerAsync(EventType eventType, BindingSelectionChanged handler, ASyncResultCallBack callback);
        public extern void DeleteAllDataValuesAsync(ASyncResultCallBack callback);
        public extern void AddColumnsAsync(TableData data, ASyncResultCallBack callback);
        public string Id;
        public EventType Type;
    }
    #region Options and Callback Args
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
    [Imported, IgnoreNamespace]
    public sealed class Error
    {
        public string Name;
        public string Message;
        public int Code;
    }
    [Imported, IgnoreNamespace, ScriptName("Office.TableData")]
    public sealed class TableData
    {
        public object[] Headers;
        [ScriptName("headers")]
        public Array[] HeadersDouble;
        public object[][] Rows;
        [ScriptName("rows")]
        public object[] SingleRow;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class BindingOptions
    {
        public string ID;
        public object AsyncContext;
        public Array columnNames;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class PromptBindingOptions : BindingOptions
    {
        public string PromptText;
        public TableData sampleData;
    }

    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class GetDataAsyncOptions
    {
        public CoercionType CoercionType;
        public ValueFormat ValueFormat;
        public FilterType FilterType;
        public ScopeType ScopeType;
        public int StartRow;
        public int StartColumn;
        public int RowCount;
        public int ColumnCount;
        public object AsyncContext;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class BindingDataChangedEventArgs
    {
        public BindingObject Binding;
        public EventType Type;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class DocumentSelectionChangedEventArgs
    {
        public DocumentObject Document;
        public EventType Type;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class BindingSelectionChangedEventArgs
    {
        public BindingsObject Binding;
        public int ColumnCount;
        public int RowCount;
        public int StartRow;
        public int StartColumn;
        public EventType Type;
    }
    #endregion
    #endregion
    #region Enums
    [Imported, IgnoreNamespace, ScriptName("Office.AsyncResultStatus")]
    public enum AsyncResultStatus
    {
        [PreserveCase]
        Succeeded,
        [PreserveCase]
        Failed,
    }
    [Imported, IgnoreNamespace, ScriptName("Office.FilterType")]
    public enum FilterType
    {
        [PreserveCase]
        All,
        [PreserveCase]
        OnlyVisible,
    }
    [Imported, IgnoreNamespace, ScriptName("Office.ValueFormat")]
    public enum ValueFormat
    {
        [PreserveCase]
        Formatted,
        [PreserveCase]
        UnFormatted,
    }
    [Imported, IgnoreNamespace, ScriptName("Office.ScopeType")]
    public enum ScopeType
    {
        [PreserveCase]
        SelectedRows,
        [PreserveCase]
        All
    }
    [Imported, IgnoreNamespace, ScriptName("Office.CoercionType")]
    public enum CoercionType
    {
        [PreserveCase]
        Text,
        [PreserveCase]
        Matrix,
        [PreserveCase]
        Table
    }
    [Imported, IgnoreNamespace, ScriptName("Office.BindingType")]
    public enum BindingType
    {
        [PreserveCase]
        Text,
        [PreserveCase]
        Matrix,
        [PreserveCase]
        Table
    }
    [Imported, IgnoreNamespace, ScriptName("Office.EventType")]
    public enum EventType
    {
        [PreserveCase]
        DocumentSelectionChanged,
        [PreserveCase]
        BindingSelectionChanged,

        [PreserveCase]
        BindingDataChanged
    }
    [Imported, IgnoreNamespace, ScriptName("Office.DocumentMode")]
    public enum DocumentMode
    {
        [PreserveCase]
        ReadOnly,
        [PreserveCase]
        ReadWrite
    }
    [Imported, IgnoreNamespace, ScriptName("Office.InitializationReason")]
    public enum InitializationEnum
    {
        [PreserveCase]
        Inserted,
        [PreserveCase]
        DocumentOpenend
    }
    #endregion
}
