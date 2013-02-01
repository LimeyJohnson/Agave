// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AgaveApi;
using System.Runtime.CompilerServices;
namespace OfficeApp1Script
{
    [IgnoreNamespace]
    public static class AgaveScript
    {
        [ScriptName("Office")]
        public static void Initialize(InializationEnum initEnum)
        {
            Script.Alert("something");
        }
        public static void SetBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue();
            NameItemAsyncOptions options = new NameItemAsyncOptions();
            options.ID = bindingID + "binding";
            bindings.AddFromNamedItemAsync(bindingID, BindingType.Text, options);
        }
    }
    
}
