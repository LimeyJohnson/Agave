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
    #endregion
    #region Classes
    [Imported, IgnoreNamespace]
    public static class Office
    {
        public static ContextObject Context;
        public static extern SelectObject Select(string binding);
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
        public extern void AddFromNamedItemAsync(string bindingID, BindingType bindingType, NameItemAsyncOptions options);
        public extern void AddFromNamedItemAsync(string bindingID, BindingType bindingType, NameItemAsyncOptions options, ASyncResultCallBack callback);
        public extern void AddFromSelectionAsync(BindingType bindingType, NameItemAsyncOptions options);
        public extern void AddFromSelectionAsync(BindingType bindingType, NameItemAsyncOptions options, ASyncResultCallBack callback);
    }
    [Imported, IgnoreNamespace]
    public class DocumentObject
    {
        public extern void AddHandlerAsync(EventType eventType, DocumentSelectionChanged handler);
        public BindingsObject Bindings;
        public DocumentMode Mode;
    }
    public sealed class SelectObject
    {
        public extern void GetDataAsync(ASyncResultCallBack callback);
        public extern void GetDataAsync(GetDataAsyncOptions options, ASyncResultCallBack callback);
        public extern void SetDataAsync(string data, GetDataAsyncOptions options);
        public extern void AddHandlerAsync(EventType eventType, BindingDataChanged handler);
        public string Id;
        public EventType Type;
    }
    #region Options and Callback Args
    public sealed class ASyncResult
    {
        public AsyncResultStatus Status;
        [ScriptName("value")]
        public string TextValue;
        [ScriptName("value")]
        public object[][] MatrixValue;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class NameItemAsyncOptions
    {
        public string ID;
    }


    [Imported, IgnoreNamespace, ScriptName("Object")]
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
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class BindingDataChangedEventArgs
    {
        public SelectObject Binding;
        public EventType Type;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class DocumentSelectionChangedEventArgs
    {
        public DocumentObject Document;
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
    public enum InitializationEnum
    {
        Inserted,
        DocumentOpenend
    }
    #endregion
    
   
    
}
