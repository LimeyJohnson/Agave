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
    public static class AgaveScript
    {
        static AgaveScript()
        {
            Office.Initialize = delegate(InializationEnum reason)
            {
         //       Script.Alert("Is this working");
            };
        }
        public static void SetBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue();
            NameItemAsyncOptions options = new NameItemAsyncOptions();
            options.ID = bindingID + "binding";
            bindings.AddFromNamedItemAsync(bindingID, BindingType.Text, options);
        }
        public static void GetBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue();
            Office.Select("bindings#" + bindingID + "binding").GetDataAsync(delegate(ASyncResult result)
            {
                if(result.status == "succeeded")
                {
                    jQuery.Select("#selectedDataTxt").Value(result.value);
                }
            });
        }
    }
    
}
