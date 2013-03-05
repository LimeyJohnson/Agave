// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
using System.Collections;
namespace FacebookScript
{

    public static class FacebookScript
    {
        public static string UserID;
        public static string AccessToken;
        public static string TableBinding = "TableBinding";
        static FacebookScript()
        {

            FacebookWindow.AsyncInit = delegate()
            {

                InitOptions options = new InitOptions();
                options.channelUrl = "http://facebookagave.azurewebsites.net/pages/channel.ashx";
                options.appId = "263395420459543";
                options.status = true;
                options.cookie = false;
                Facebook.init(options);
                jQuery.Select("#GetFriends").Click(new jQueryEventHandler(InsertFriends));
                jQuery.Select("#LogOut").Click(new jQueryEventHandler(LogOutOfFacebook));
                jQuery.Select("#SelectAll").Click(new jQueryEventHandler(HandleSelectAllCheckBox));
                jQuery.Select("#friend").Hide();
                Facebook.getLoginStatus(delegate(LoginResponse loginResponse)
                {
                    if (loginResponse.status == "connected")
                    {
                        UserID = loginResponse.authResponse.userID;
                        AccessToken = loginResponse.authResponse.accessToken;
                    }
                    else
                    {
                        LoginOptions LoginOptions = new LoginOptions();
                        LoginOptions.scope = "email,create_event,user_likes,publish_stream,user_about_me,friends_about_me,user_activities,friends_activities,user_birthday,friends_birthday,user_checkins,friends_checkins,user_education_history,friends_education_history,user_events,friends_events,user_groups,friends_groups,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_notes,friends_notes,user_photos,friends_photos,user_questions,friends_questions,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_subscriptions,friends_subscriptions,user_videos,friends_videos,user_website,user_work_history,friends_work_history";
                        Facebook.login(delegate(LoginResponse response)
                        {
                            UserID = response.authResponse.userID;
                        }, LoginOptions);
                    }
                });
            };
            Office.Initialize = delegate(InitializationEnum initReason)
            {

            };
            Element reference = Document.GetElementsByTagName("script")[0];
            string JSID = "facebook-jssdk";
            if (reference.ID != JSID)
            {
                ScriptElement js = (ScriptElement)Document.CreateElement("script");
                js.ID = JSID;
                js.SetAttribute("async", true);
                js.Src = "//connect.facebook.net/en_US/all.js";
                reference.ParentNode.InsertBefore(js, reference);
            }

        }
        public static void LogOutOfFacebook(jQueryEvent eventArgs)
        {
            Facebook.logout(delegate() 
            {
                UserID = null;
                AccessToken = null;
            });
        }
        public static void InsertFriends(jQueryEvent eventArgs)
        {
            jQueryObject comboBoxes = jQuery.Select("#FieldChoices input:checked");

            TableData td = new TableData();
            td.HeadersDouble = new string[1][];
            td.HeadersDouble[0] = new string[comboBoxes.Length+1];
            td.HeadersDouble[0][0] = "ID";

            Array fieldNames = new Array();
            fieldNames[fieldNames.Length] = "uid";
            
            comboBoxes.Each(delegate(int i, Element e)
            {
                fieldNames[fieldNames.Length] = (string)e.GetAttribute("field");
                td.HeadersDouble[0][i+1] = (string)e.GetAttribute("display");
            });
            string query = "SELECT " + fieldNames.Join(",") + " FROM user WHERE uid IN (SELECT uid2 from friend WHERE uid1 = me())";
            ApiOptions queryOptions = new ApiOptions();
            queryOptions.Q = query;
          
            Facebook.api("fql", queryOptions, delegate(ApiResponse response)
            {
                td.Rows = new string[response.data.Length][];
                for (int i = 0; i < response.data.Length; i++)
                {
                    td.Rows[i] = new string[fieldNames.Length];
                    for (int y = 0; y < fieldNames.Length; y++)
                    {
                        td.Rows[i][y] = response.data[i][(string)fieldNames[y]] ?? "null";
                    }
                }
                ((ImageElement)Document.GetElementById("profilepic")).Src = "http://graph.facebook.com/" + td.Rows[0][0] + "/picture";
                GetDataAsyncOptions options = new GetDataAsyncOptions();
                options.CoercionType = CoercionType.Table;
                Office.Context.Document.SetSelectedDataAsync(td, options, delegate(ASyncResult result)
                {
                    if (result.Status == AsyncResultStatus.Failed)
                    {
                        Script.Literal("write({0} + ' : '+{1})", result.Error.Name, result.Error.Message);
                    }
                    if (result.Status == AsyncResultStatus.Succeeded)
                    {
                        NameItemAsyncOptions bindingOptions = new NameItemAsyncOptions();
                        bindingOptions.ID = TableBinding;
                        jQuery.Select("#friend").Show();
                        jQuery.Select("#insert").Hide();
                        Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Table, bindingOptions, delegate(ASyncResult bindingResult)
                        {
                            Office.Select("bindings#" + TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, new BindingSelectionChanged(HandleTableSelection));
                        });
                    }
                });
            });
        }
        public static void HandleTableSelection(BindingSelectionChangedEventArgs args)
        {
            GetDataAsyncOptions options = new GetDataAsyncOptions();
            options.StartRow = args.StartRow;
            options.StartColumn = 0;
            options.RowCount = 1;
            options.ColumnCount = 1;
            options.CoercionType = CoercionType.Table;
            Office.Select("bindings#" + TableBinding).GetDataAsync(options, delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Succeeded)
                {
                    string ID = (string)result.TableValue.Rows[0][0];
                    ((ImageElement)Document.GetElementById("profilepic")).Src = "http://graph.facebook.com/" + ID + "/picture";
                }
            });
        }
        public static void HandleSelectAllCheckBox(jQueryEvent eventArgs)
        {
            bool convertToChecked = jQuery.Select("#ckbSelectAll").Is(":checked");

            jQuery.Select("#FieldChoices input").Each(delegate(int i, Element e)
            {
                ((CheckBoxElement)e).Checked = convertToChecked;
            });
        }
    }
}
