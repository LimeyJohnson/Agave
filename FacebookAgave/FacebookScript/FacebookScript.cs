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
        public static string FriendID;
        public static TableData DeleteMedata;
        public static Dictionary<string, Field> fields;
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
                jQuery.Select("#postfriendstatus").Click(new jQueryEventHandler(PostFriendStatus));
                jQuery.Select("#posttoallfriends").Click(new jQueryEventHandler(PostToAllFreinds));
                jQuery.Select("#btnlogon").Click(new jQueryEventHandler(LogIntoFacebook));
                Facebook.Event.subscribe("auth.authResponseChange", new EventChange(HandleFacebookAuthEvent));
                Facebook.getLoginStatus(delegate(LoginResponse loginResponse)
                {
                    HandleFacebookAuthEvent(loginResponse);
                    if (loginResponse.status != "connected")
                    {
                        LogIntoFacebook(null);
                    }
                });
            };
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                InitFields();
                InsertFieldCheckboxes();
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
        public static void HandleFacebookAuthEvent(LoginResponse response)
        {
            if (response.status == "connected")
            {
                jQuery.Select("#logon").Hide();
                jQuery.Select("#insert").Show();
                jQuery.Select("#friend").Show();
                UserID = response.authResponse.userID;
                AccessToken = response.authResponse.accessToken;
            }
            else
            {
                UserID = null;
                AccessToken = null;
                jQuery.Select("#insert").Hide();
                jQuery.Select("#friend").Hide();
                jQuery.Select("#logon").Show();
            }
        }
        public static void LogIntoFacebook(jQueryEvent eventArgs)
        {
            LoginOptions LoginOptions = new LoginOptions();
            LoginOptions.scope = "email,publish_actions,create_event,user_likes,friends_education_history, friends_likes,publish_stream,user_about_me,friends_about_me,user_activities,friends_activities,user_birthday,friends_birthday,user_checkins,friends_checkins,user_education_history,friends_education_history,user_events,friends_events,user_groups,friends_groups,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_notes,friends_notes,user_photos,friends_photos,user_questions,friends_questions,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_subscriptions,friends_subscriptions,user_videos,friends_videos,user_website,user_work_history,friends_work_history";
            Facebook.login(delegate(LoginResponse response) {}, LoginOptions);
        }
        public static void InitFields()
        {
            fields = new Dictionary<string, Field>();
            fields["uid"] = new RequiredField("uid", "ID");
            fields["first_name"] = new Field("first_name", "First Name");
            fields["last_name"] = new Field("last_name", "Last Name");
            fields["birthday_date"] = new Field("birthday_date", "Birthday");
            fields["sex"] = new Field("sex", "Sex");
            fields["mutual_friend_count"] = new Field("mutual_friend_count", "Mutual Friends");
            fields["quotes"] = new Field("quotes", "Quotes");
            fields["political"] = new Field("political", "Political");
            fields["relationship_status"] = new Field("relationship_status", "Relationship Status");
            fields["religion"] = new Field("religion", "Religion");
            fields["wall_count"] = new Field("wall_count", "Wall Count");
            fields["friend_count"] = new Field("friend_count", "Friend Count");
            fields["work_Employer"] = new StructField("work", "Employer", "employer", "name", 0);
            fields["work_Position"] = new StructField("work", "Position", "position", "name", 0);
            fields["current_location_City"] = new StructField("current_location", "Current City", "city", null);
            fields["current_location_State"] = new StructField("current_location", "Current State", "state", null);
            fields["current_location_Country"] = new StructField("current_location", "Current Country", "country", null);
            fields["interests"] = new Field("interests", "Interests");
            fields["profile_url"] = new Field("profile_url", "Profile URL");
            fields["sports"] = new ArrayField("sports", "Sports", "name");
            fields["status_Message"] = new StructField("status", "Current Status", "message", null);
            fields["status_Time"] = new StructField("status", "Current Status Time", "time", null);
        }
        public static void InsertFieldCheckboxes()
        {
            jQueryObject comboBoxLocation = jQuery.Select("#FieldChoices");
            jQuery.Each(fields, delegate(string s, object f)
            {
                string html = ((Field)f).Html;
                if (html != null && html != "")
                {
                    comboBoxLocation.Append(html);
                }
            });
                     
        }
        public static void LogOutOfFacebook(jQueryEvent eventArgs)
        {
            Facebook.logout(delegate()
            {
               
            });
        }
        public static void InsertFriends(jQueryEvent eventArgs)
        {
            TableData td = new TableData();
            Array fieldNames = new Array();
            td.HeadersDouble = new Array[1];
            td.HeadersDouble[0] = new Array();

            Dictionary<string, Field> dict = new Dictionary<string, Field>();
            jQuery.Each(fields, delegate(string name, object value)
            {
                Field ff = (Field)value;
                if (ff.Checked)
                {
                    dict[name] = ff;
                    td.HeadersDouble[0][td.HeadersDouble[0].Length] = ff.DisplayText;
                    fieldNames[fieldNames.Length] = ff.FieldName;
                }
            });

            //remove duplicates from field name
            fieldNames = fieldNames.Filter(delegate(object o, int i, Array a)
            {
                return fieldNames.IndexOf(o) == i;
            });
            string fieldList = fieldNames.Join(",");
            string query = "SELECT " + fieldList + " FROM user WHERE uid IN (SELECT uid2 from friend WHERE uid1 = me())";
            ApiOptions queryOptions = new ApiOptions();
            queryOptions.Q = query;

            Facebook.api("fql", queryOptions, delegate(ApiResponse response)
            {
                td.Rows = new string[response.data.Length][];
                for (int i = 0; i < response.data.Length; i++)
                {
                    td.Rows[i] = new string[td.HeadersDouble[0].Length];
                    for (int y = 0; y < td.HeadersDouble[0].Length; y++)
                    {
                        string fn = dict.Keys[y];
                        Field f = fields[fn];
                        td.Rows[i][y] = f.ParseResult(response.data[i]);
                    }
                }
                ((ImageElement)Document.GetElementById("profilepic")).Src = "http://graph.facebook.com/" + td.Rows[0][0] + "/picture";
                GetDataAsyncOptions options = new GetDataAsyncOptions();
                options.CoercionType = CoercionType.Table;
                DeleteMedata = td;
                Office.Context.Document.SetSelectedDataAsync(td, options, delegate(ASyncResult result)
                {
                    if (result.Status == AsyncResultStatus.Failed)
                    {
                        Script.Literal("write({0} + ' : '+{1})", result.Error.Name, result.Error.Message);
                    }
                    if (result.Status == AsyncResultStatus.Succeeded)
                    {
                        BindingOptions bindingOptions = new BindingOptions();
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
            if (args.StartRow > 1) // do nothing when the header column is selected
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
                        FriendID = (string)result.TableValue.Rows[0][0];
                        ((ImageElement)Document.GetElementById("profilepic")).Src = "http://graph.facebook.com/" + FriendID + "/picture";
                    }
                });
            }
        }
        public static void HandleSelectAllCheckBox(jQueryEvent eventArgs)
        {
            bool convertToChecked = jQuery.Select("#ckbSelectAll").Is(":checked");

            jQuery.Select("#FieldChoices input").Each(delegate(int i, Element e)
            {
                ((CheckBoxElement)e).Checked = convertToChecked;
            });
        }
        public static void PostFriendStatus(jQueryEvent eventArgs)
        {
            if (FriendID != null && FriendID != "")
            {
                UIOptions options = new UIOptions();
                options.To = FriendID;
                options.From = UserID;
                options.Method = "feed";
                options.Display = "page";
                Facebook.ui(options, delegate(UIResponse response)
                {
                    Script.Literal("document.write({0});", response.Post_id);
                });
            }
        }
        public static void PostToAllFreinds(jQueryEvent eventArgs)
        {
            GetDataAsyncOptions options = new GetDataAsyncOptions();
            options.FilterType = FilterType.OnlyVisible;
            options.StartColumn = 0;
            options.ColumnCount = 1;
            options.CoercionType = CoercionType.Table;
            Office.Select("bindings#" + TableBinding).GetDataAsync(options, delegate(ASyncResult result)
            {
                Array friendsArray = new Array();
                for (int x = 0; x < result.TableValue.Rows.Length; x++)
                {
                    friendsArray[friendsArray.Length] = result.TableValue.Rows[x][0];
                }
                UIOptions uiOptions = new UIOptions();
                uiOptions.Display = "popup";
                uiOptions.Method = "send";
                uiOptions.ToArray = friendsArray;
                uiOptions.From = UserID;
                uiOptions.Link = "http://google.com";
                Facebook.ui(uiOptions, delegate(UIResponse UIResp)
                {
                    Script.Literal("document.write({0});", UIResp.Post_id);
                });
            });
        }
        public static void setBinding()
        {
            BindingOptions options = new BindingOptions();
            options.ID = "TextBinding";
            Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Text, options, delegate(ASyncResult result)
            {
                Office.Select("bindings#TextBinding").AddHandlerAsync(EventType.BindingDataChanged, new BindingSelectionChanged(DataChanged));
            });

        }
        public static void DataChanged(BindingSelectionChangedEventArgs args)
        {
            GetDataAsyncOptions options = new GetDataAsyncOptions();
            options.CoercionType = CoercionType.Text;
            Office.Select("bindings#TextBinding").GetDataAsync(options, delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Succeeded)
                {
                    jQuery.Select("#selectedDataTxt").Value(result.TextValue);
                }
            });

        }
    }
}
