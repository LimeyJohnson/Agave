// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AgaveApi;
using System.Runtime.CompilerServices;
using FreindsLibrary;
namespace OfficeApp1Script
{
    public static class AgaveScript
    {
        public static string FieldBindingSuffix = "FieldBinding";
        public static string RowBindingSuffix = "RowBinding";
        public static string TableBindingSuffix = "TableBinding";
        public static string RowBinding = "Row";
        static AgaveScript()
        {
            Office.Initialize = delegate(InializationEnum reason)
            {
                SetBinding(RowBinding, BindingType.Matrix);
                PopulateRowCombo();
                Select(RowBinding).AddHandlerAsync(EventType.BindingDataChanged, delegate(BindingDataChangedEventArgs args)
                {
                    jQuery.Select("#eventResults").Append("Event fired: " + args.Binding.Id + " Type: " + args.Type.ToString());
                });
            };

        }
        public static void Logon()
        {
            LoginOptions options = new LoginOptions();
            options.scope = "email, user_likes, publish_stream";
            Facebook.login(delegate(LoginResponse response) { }, options);
        }
        public static void SetFieldBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue();
            Bindings.AddFromNamedItemAsync(bindingID, BindingType.Text, CreateOptions(bindingID + FieldBindingSuffix));
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
           // ComboBoxElement e = new ComboBoxElement();
        }
        public static void SetTableBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + TableBindingSuffix;
            Bindings.AddFromSelectionAsync(BindingType.Matrix, CreateOptions(bindingID));
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
        public static void PopulateRowCombo()
        {
            Array items = new Array();
            Select(RowBinding).GetDataAsync(delegate(ASyncResult result)
            {
                jQueryObject combo = jQuery.Select("#rows");
                combo.Html("");
                Array fields = (Array)result.matrixValue[0][0];
                jQuery.Each(fields, delegate(int i, object o)
                {
                    string html = "<option>" + o.ToString() + "</option>";
                    combo.Append(html);
                });
            });
        }
        public static SelectObject Select(string bindingID)
        {
            return Office.Select("bindings#" + bindingID);
        }
        public static void SetBinding(string bindingID, BindingType type)
        {
            Bindings.AddFromNamedItemAsync(bindingID, type, CreateOptions(bindingID));
        }
        public static void GetRowValues()
        {
            Select(RowBinding).GetDataAsync(delegate(ASyncResult result)
            {
                jQueryObject combo = jQuery.Select("#results");
                combo.Html("");
                Array fields = (Array)result.matrixValue[1];
                jQuery.Each(fields, delegate(int i, object o)
                {
                    string[] fieldNames = (string[])result.matrixValue[0][0];
                    string appendText = fieldNames[i].ToString() + " : " + (o != null ? o.ToString() : "JSNULL") + "<br/>" ;
                    combo.Append(appendText);
                });
            });
        }
    }
    
}
