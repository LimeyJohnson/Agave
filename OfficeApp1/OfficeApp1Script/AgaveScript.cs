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
        public static string TableBinding = "TableBinding";
        public static string RowBinding = "DataTypes";
        static AgaveScript()
        {
            Office.Initialize = delegate(InializationEnum reason)
            {
                SetBinding(RowBinding, BindingType.Matrix);
                SetBinding(TableBinding, BindingType.Table);
                PopulateRowCombo();
                GetRowValues();
                Office.Context.Document.AddHandlerAsync(EventType.DocumentSelectionChanged, delegate(DocumentSelectionChangedEventArgs args)
                {
                    jQuery.Select("#eventResults").Append("Event fired: " + args.Document.Mode.ToString() + " Type: " + args.Type.ToString() + "<br/>");
                    GetRowValues();
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
            Office.Context.Document.Bindings.AddFromNamedItemAsync(bindingID, BindingType.Text, CreateOptions(bindingID + FieldBindingSuffix));
        }
        public static void GetFieldBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + FieldBindingSuffix;
            Office.Select("bindings#" + bindingID).GetDataAsync(delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Succeeded)
                {
                    jQuery.Select("#selectedDataTxt").Value(result.TextValue);
                }
            });
        }
        public static void SetFieldData()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + FieldBindingSuffix;
            string data = jQuery.Select("#selectedDataTxt").GetValue();
            Office.Select("bindings#" + bindingID).SetDataAsync(data, CreateCoercionTypeOptions(CoercionType.Text));
            // ComboBoxElement e = new ComboBoxElement();
        }
        public static void SetTableBinding()
        {
            string bindingID = jQuery.Select("#BindingField").GetValue() + TableBinding;
            Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Matrix, CreateOptions(bindingID));
        }
        private static GetDataAsyncOptions CreateCoercionTypeOptions(CoercionType type)
        {
            GetDataAsyncOptions options = new GetDataAsyncOptions();
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
            Select(RowBinding).GetDataAsync(CreateCoercionTypeOptions(CoercionType.Matrix), delegate(ASyncResult result)
            {
                jQueryObject combo = jQuery.Select("#rows");
                combo.Html("");
                Array fields = (Array)result.MatrixValue[0][0];
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
            if (type == BindingType.Matrix)
            {
                Office.Context.Document.Bindings.AddFromSelectionAsync(type, CreateOptions(bindingID), new ASyncResultCallBack(CheckAsyncCallbackForErrors));
            }
            else
            {
                Office.Context.Document.Bindings.AddFromNamedItemAsync(bindingID, type, CreateOptions(bindingID), new ASyncResultCallBack(CheckAsyncCallbackForErrors));
            }

        }
        public static void GetRowValues()
        {
            Select(RowBinding).GetDataAsync(delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Succeeded)
                {
                    jQueryObject combo = jQuery.Select("#row");
                    combo.Html("");
                    Array fields = (Array)result.MatrixValue[1];
                    jQuery.Each(fields, delegate(int i, object o)
                    {
                        string[] fieldNames = (string[])result.MatrixValue[0][0];
                        string appendText = fieldNames[i].ToString() + " : " + (o != null ? o.ToString() : "JSNULL") + "<br/>";
                        combo.Append(appendText);
                    });
                }
                else
                {
                    SetError("GetDataAsync in GetRowValues() failed");
                }
            });
        }
        public static void GetTableBinding()
        {
            Select(TableBinding).GetDataAsync(delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Succeeded)
                {
                    jQueryObject combo = jQuery.Select("#table");
                    combo.Html("");
                    Array fields = (Array)result.MatrixValue[1];
                    jQuery.Each(fields, delegate(int i, object o)
                    {
                        string[] fieldNames = (string[])result.MatrixValue[0][0];
                        string appendText = fieldNames[i].ToString() + " : " + (o != null ? o.ToString() : "JSNULL") + "<br/>";
                        combo.Append(appendText);
                    });
                }
                else
                {
                    SetError("GetDataAsync in GetRowValues() failed");
                }
            });
        }
        public static void CheckAsyncCallbackForErrors(ASyncResult result)
        {
            if (result.Status != AsyncResultStatus.Succeeded) SetError("ASync Result Failed");
        }
        public static void SetError(string errorText)
        {
            jQuery.Select("#error").Append(errorText);
        }
    }

}