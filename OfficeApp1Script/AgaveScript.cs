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
        static AgaveScript()
        {
            Office.Initialize = delegate(InializationEnum reason)
            {
                InitOptions options = new InitOptions();
                options.appId = "263395420459543";
                //options.channelUrl = "//limeyhouse.dyndns.org/channel.aspx";
                options.status = true;
                options.cookie = true;
                options.xfbml = false;
                Facebook.init(options);
                Facebook.getLoginStatus(delegate(LoginResponse loginResponse)
                {
                    if (loginResponse.status == "connected")
                    {
                        string UserID = loginResponse.authResponse.userID;
                        jQuery.Select("results").Value(UserID);
                    }
                });

            };
        }
        public static void LogIn()
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
    }
    
}
