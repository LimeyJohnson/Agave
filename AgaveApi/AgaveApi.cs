// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace AgaveApi
{
    #region Delegates
    public delegate void ASyncResultCallBack(ASyncResult result);
    public delegate void InitReason(InializationEnum inializationEnum);
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
        public extern void AddFromSelectionAsync(BindingType bindingType, NameItemAsyncOptions options);
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
        public extern void GetDataAsync(CoercionTypeOptions options, ASyncResultCallBack callback);
        public extern void SetDataAsync(string data, CoercionTypeOptions options);
        public extern void AddHandlerAsync(EventType eventType, BindingDataChanged handler);
        public string Id;
        public EventType Type;
    }
    #region Options and Callback Args
    public sealed class ASyncResult
    {
        public string status;
        public string value;
        [ScriptName("value")]
        public object[][] matrixValue;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class NameItemAsyncOptions
    {
        public string ID;
    }


    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class CoercionTypeOptions
    {
        public string CoercionType;
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
    public enum InializationEnum
    {
        Inserted,
        DocumentOpenend
    }
    #endregion
    
   
    
}
