// AppForOffice.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    public delegate void EventHandler();
    
    [ScriptImport, ScriptIgnoreNamespace]
    public static class Office
    {
        public static ContextObject Context;
        public static BindingObject Select(string binding) { return null; }
        [ScriptField]
        public static  Action<InitializationEnum> Initialize { get; set; }
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Office.InitializationReason")]
    public enum InitializationEnum
    {
        [ScriptName(PreserveCase=true)]
        Inserted,
        [ScriptName(PreserveCase=true)]
        DocumentOpenend
    }
    
}
