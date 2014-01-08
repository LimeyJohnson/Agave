// AppForOffice.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace AppForOffice
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
    [ScriptImport, ScriptIgnoreNamespace]
    public static class Office
    {
        public static ContextObject Context;
        public static extern BindingObject Select(string binding);
        [ScriptField]
        public static extern InitReason Initialize { get; set; }
    }
    [ScriptImport, ScriptIgnoreNamespace]
    public class ContextObject
    {
        public DocumentObject Document;
    }

    [ScriptImport, ScriptIgnoreNamespace]
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
    [ScriptImport, ScriptIgnoreNamespace]
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
    [ScriptImport, ScriptIgnoreNamespace]
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
    [ScriptImport, ScriptIgnoreNamespace]
    public sealed class Error
    {
        public string Name;
        public string Message;
        public int Code;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.TableData")]
    public sealed class TableData
    {
        public object[] Headers;
        [ScriptName("headers")]
        public Array[] HeadersDouble;
        public object[][] Rows;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class BindingOptions
    {
        public string ID;
        public object AsyncContext;
        public string[] Columns;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
   public sealed class PromptBindingOptions: BindingOptions
    {
        string PromptText;
    }

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
        public RowType Rows;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class BindingDataChangedEventArgs
    {
        public BindingObject Binding;
        public EventType Type;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class DocumentSelectionChangedEventArgs
    {
        public DocumentObject Document;
        public EventType Type;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
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
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.AsyncResultStatus")]
    public enum AsyncResultStatus
    {
        [ScriptName(PreserveCase=true)]
        Succeeded,
        [ScriptName(PreserveCase=true)]
        Failed,
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.Table")]
    public enum RowType
    {
        [ScriptName(PreserveCase=true)]
        ThisRow
    }


    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.FilterType")]

    public enum FilterType
    {
        [ScriptName(PreserveCase=true)]
        All,
        [ScriptName(PreserveCase=true)]
        OnlyVisible,
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.ValueFormat")]
    public enum ValueFormat
    {
        [ScriptName(PreserveCase=true)]
        Formatted,
        [ScriptName(PreserveCase=true)]
        UnFormatted,
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.CoercionType")]
    public enum CoercionType
    {
        [ScriptName(PreserveCase=true)]
        Text,
        [ScriptName(PreserveCase=true)]
        Matrix,
        [ScriptName(PreserveCase=true)]
        Table
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.BindingType")]
    public enum BindingType
    {
        [ScriptName(PreserveCase=true)]
        Text,
        [ScriptName(PreserveCase=true)]
        Matrix,
        [ScriptName(PreserveCase=true)]
        Table
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.EventType")]
    public enum EventType
    {
        [ScriptName(PreserveCase=true)]
        DocumentSelectionChanged,
        [ScriptName(PreserveCase=true)]
        BindingSelectionChanged,

        [ScriptName(PreserveCase=true)]
        BindingDataChanged
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.DocumentMode")]
    public enum DocumentMode
    {
        [ScriptName(PreserveCase=true)]
        ReadOnly,
        [ScriptName(PreserveCase=true)]
        ReadWrite
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.InitializationReason")]
    public enum InitializationEnum
    {
        [ScriptName(PreserveCase=true)]
        Inserted,
        [ScriptName(PreserveCase=true)]
        DocumentOpenend
    }
    #endregion
}
