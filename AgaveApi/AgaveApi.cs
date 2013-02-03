// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace AgaveApi
{
    public delegate void ASyncResultCallBack(ASyncResult result);
    public delegate void InitReason(InializationEnum inializationEnum);
   [Imported, IgnoreNamespace, ScriptName("Office.context.document.bindings")]
    public static class bindings  
    {
        
        public static extern void AddFromNamedItemAsync(string bindingID, BindingType bindingType, NameItemAsyncOptions options);
        
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
    }
    [Imported, IgnoreNamespace, ScriptName("Office.BindingType")]
    public enum BindingType
    {
        [PreserveCase]
        Text
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class NameItemAsyncOptions
    {
        public string ID;
    }
    public sealed class SelectObject
    {
        public extern void GetDataAsync(ASyncResultCallBack callback);
    }
    public enum InializationEnum
    {
        Inserted,
        DocumentOpenend
    }

}
