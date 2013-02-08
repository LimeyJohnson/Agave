// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace AgaveApi
{
    public delegate void ASyncResultCallBack(ASyncResult result);
    public delegate void InitReason(InializationEnum inializationEnum);
    public delegate void EventHandler();
    public delegate void EventHandlerWithString(string ID);
    [Imported, IgnoreNamespace, ScriptName("Office.context.document.bindings")]
    public static class Bindings
    {
        public static extern void AddFromNamedItemAsync(string bindingID, BindingType bindingType, NameItemAsyncOptions options);
        public static extern void AddFromSelectionAsync(BindingType bindingType, NameItemAsyncOptions options);
    }
    [Imported, IgnoreNamespace, ScriptName("Office.context.document")]
    public static class Document
    {
        public static extern void AddHandlerAsync(EventType eventType, EventHandler handler);
        public static extern void AddHandlerAsync(EventType eventType, EventHandlerWithString handler);
    }
    [Imported, IgnoreNamespace, ScriptName("Office")]
    public static class Office
    {
        public static extern SelectObject Select(string binding);
        [IntrinsicProperty]
        public static extern InitReason Initialize { get; set; }
    }
    public sealed class ASyncResult
    {
        public string status;
        public string value;
        [ScriptName("value")]
        public object[][] matrixValue;
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
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class NameItemAsyncOptions
    {
        public string ID;
    }
    public sealed class SelectObject
    {
        public extern void GetDataAsync(ASyncResultCallBack callback);
        public extern void GetDataAsync(CoercionTypeOptions options, ASyncResultCallBack callback);
        public extern void SetDataAsync(string data, CoercionTypeOptions options);
        public extern void AddHandlerAsync(EventType eventType, EventHandlerWithString handler);
    }
    public enum InializationEnum
    {
        Inserted,
        DocumentOpenend
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class CoercionTypeOptions
    {
        public string CoercionType;
    }
   
    
}
