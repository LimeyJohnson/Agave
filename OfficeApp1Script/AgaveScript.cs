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
        public static string FieldBindingSuffix = "FieldBinding";
        public static string RowBindingSuffix = "RowBinding";
        public static string TableBindingSuffix = "TableBinding";
        static AgaveScript()
        {
            Office.Initialize = delegate(InializationEnum reason)
            {
         //       Script.Alert("Is this working");
            };
        }
        public static void SetFieldBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue();
            bindings.AddFromNamedItemAsync(bindingID, BindingType.Text, CreateOptions(bindingID + FieldBindingSuffix));
        }
        public static void GetFieldBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + FieldBindingSuffix;
            Office.Select("bindings#" + bindingID).GetDataAsync(delegate(ASyncResult result)
            {
                if(result.status == "succeeded")
                {
                    jQuery.Select("#selectedDataTxt").Value(result.value);
                }
            });
        }
        public static void SetFieldData()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + FieldBindingSuffix;
            string data = jQuery.Select("#selectedDataTxt").GetValue();
            Office.Select("bindings#" + bindingID).SetDataAsync(data, CreateCoercionType("text"));
        }
        public static void SetTableBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + TableBindingSuffix;
            bindings.AddFromSelectionAsync(BindingType.Matrix, CreateOptions(bindingID));
        }
        public static void GetTableBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + TableBindingSuffix;
            Office.Select("bindings#" + bindingID).GetDataAsync(CreateCoercionType("table"), delegate(ASyncResult result)
            {
                Script.Alert("Break point");
            });
        }

        private static CoercionTypeOptions CreateCoercionType(string type)
        {
            CoercionTypeOptions options = new CoercionTypeOptions();
            options.CoercionType = type;
            return options;
        }
        private static NameItemAsyncOptions CreateOptions(string ID)
        {
            NameItemAsyncOptions options = new NameItemAsyncOptions();
            options.ID = ID;
            return options;
        }
    }
    
}
