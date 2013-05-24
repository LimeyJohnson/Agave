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
    enum AppState
    {
        LoggedOut = 1,
        FieldSelection = 2,
        Results = 3,
        Main = 4
    };
    public static class FacebookScript
    {
        public static string UserID;
        public static string AccessToken;
        public static string TableBinding = "TableBinding";
        public static string FriendID;
        public static Dictionary<string, Field> fields;
        private static jQueryObject Logon;
        private static jQueryObject Insert;
        private static jQueryObject Friend;
        private static jQueryObject Modal;
        private static jQueryObject Main;
        private static bool FacebookInited = false;
        private static AppState CurrentAppState = AppState.LoggedOut;

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

                Facebook.Event.subscribe("auth.authResponseChange", new EventChange(HandleFacebookAuthEvent));
                Facebook.getLoginStatus(delegate(LoginResponse loginResponse)
                {
                    HandleFacebookAuthEvent(loginResponse);
                    if (loginResponse.status != "connected")
                    {

                        LogIntoFacebook(null);
                    }
                });
                FacebookInited = true;
            };
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                jQuery.Select("#GetFriends").Click(new jQueryEventHandler(InsertFriends));
                jQuery.Select("#LogOut").Click(new jQueryEventHandler(LogOutOfFacebook));
                jQuery.Select("#postfriendstatus").Click(new jQueryEventHandler(PostFriendStatus));
                jQuery.Select("#posttoallfriends").Click(new jQueryEventHandler(PostToAllFreinds));
                jQuery.Select("#btnlogon").Click(new jQueryEventHandler(LogIntoFacebook));
                jQuery.Select("#selectallcheckbox").Change(new jQueryEventHandler(HandleSelectAll));
                jQuery.Select("#insertfreinds").Click(new jQueryEventHandler(InsertFriends));
                //Sync up goto main buttons they are all insert tags ending in main
                jQuery.Select("input[id$='main']").Click(new jQueryEventHandler(GotoMain));
                Friend = jQuery.Select("#friend");
                Logon = jQuery.Select("#logon");
                Insert = jQuery.Select("#insert");
                Modal = jQuery.Select("#modal");
                Main = jQuery.Select("#main");
                InitFields();
                InsertAccordions();
                BindingOptions options = new BindingOptions();
                options.ID = TableBinding;
                Office.Context.Document.Bindings.AddFromNamedItemAsync("Names", BindingType.Table, options);
                Script.SetTimeout(delegate()
                {
                    if (!FacebookInited) Script.Literal("window.fbAsyncInit()");
                }, 2000);
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
            };

        }
        public static void GotoMain(jQueryEvent eventArgs)
        {
            CurrentAppState = AppState.Main;
            UpdateView();
        }

        public static void UpdateView()
        {
            switch (CurrentAppState)
            {
                case AppState.LoggedOut:
                    Show(Logon);
                    Hide(Insert);
                    Hide(Friend);
                    Hide(Main);
                    break;
                case AppState.FieldSelection:
                    Show(Insert);
                    Hide(Logon);
                    Hide(Friend);
                    Hide(Main);
                    break;
                case AppState.Main:
                    Show(Main);
                    Hide(Logon);
                    Hide(Friend);
                    Hide(Insert);
                    break;
            }
        }
        public static void HandleFacebookAuthEvent(LoginResponse response)
        {
            if (response.status == "connected")
            {
                UserID = response.authResponse.userID;
                AccessToken = response.authResponse.accessToken;
                CurrentAppState = AppState.FieldSelection;
            }
            else
            {
                UserID = null;
                AccessToken = null;
                CurrentAppState = AppState.LoggedOut;
            }
            UpdateView();
        }
        public static void LogIntoFacebook(jQueryEvent eventArgs)
        {
            LoginOptions LoginOptions = new LoginOptions();
            LoginOptions.scope = "email,publish_actions,create_event,user_likes,friends_education_history, friends_likes,publish_stream,user_about_me,friends_about_me,user_activities,friends_activities,user_birthday,friends_birthday,user_checkins,friends_checkins,user_education_history,friends_education_history,user_events,friends_events,user_groups,friends_groups,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_notes,friends_notes,user_photos,friends_photos,user_questions,friends_questions,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_subscriptions,friends_subscriptions,user_videos,friends_videos,user_website,user_work_history,friends_work_history";
            Facebook.login(delegate(LoginResponse response) { }, LoginOptions);
        }
        public static void InitFields()
        {
            fields = new Dictionary<string, Field>();
            fields["uid"] = new RequiredField("uid", "FBID");
            fields["first_name"] = new Field("first_name", "First Name", "Basic");
            fields["last_name"] = new Field("last_name", "Last Name", "Basic");
            fields["birthday_date"] = new Field("birthday_date", "Birthday", "Basic");
            fields["sex"] = new Field("sex", "Sex", "Basic");
            fields["mutual_friend_count"] = new Field("mutual_friend_count", "Mutual Friends", "Counts");
            fields["quotes"] = new Field("quotes", "Quotes", "Extended", false);
            fields["political"] = new Field("political", "Political", "Extended");
            fields["relationship_status"] = new Field("relationship_status", "Relationship Status", "Extended");
            fields["religion"] = new Field("religion", "Religion", "Extended");
            fields["wall_count"] = new Field("wall_count", "Wall Count", "Counts");
            fields["friend_count"] = new Field("friend_count", "Friend Count", "Counts");
            fields["work_Employer"] = new StructField("work", "Employer", "employer", "name", "Employment", 0);
            fields["work_Position"] = new StructField("work", "Position", "position", "name", "Employment", 0);
            fields["current_location_City"] = new StructField("current_location", "Current City", "city", null, "Location");
            fields["current_location_State"] = new StructField("current_location", "Current State", "state", null, "Location");
            fields["current_location_Country"] = new StructField("current_location", "Current Country", "country", null, "Location");
            fields["interests"] = new Field("interests", "Interests", "Extended");
            fields["profile_url"] = new Field("profile_url", "Profile URL", "Extended");
            fields["sports"] = new ArrayField("sports", "Sports", "name", "Basic");
            fields["status_Message"] = new StructField("status", "Current Extended", "message", null, "Status");
            fields["status_Time"] = new StructField("status", "Current Status Time", "time", null, "Status");

            //temporary work around to disable all of the fields
            jQuery.Each(fields, delegate(string s, object o)
            {
                Field f = (Field)o;
                f.m_defaultChecked = false;
            });
        }
        public static void InsertAccordions()
        {
            jQueryObject comboBoxLocation = jQuery.Select("#FieldChoices");
            Dictionary<string, Array> accordions = new Dictionary<string, Array>();
            jQuery.Each(fields, delegate(string s, object o)
            {
                Field f = (Field)o;
                if (f.ContainerName != null)
                {
                    if (accordions[f.ContainerName] == null) accordions[f.ContainerName] = new Array();
                    accordions[f.ContainerName][accordions[f.ContainerName].Length] = f.Html;
                }
            });
            jQuery.Each(accordions, delegate(string s, object o)
           {
               string template = "<div class='group' id='group{0}'><h3>{0}</h3><div><input id='ah{0}' type='checkbox' />Select All<br/>{1}</div></div>";

               comboBoxLocation.Append(string.Format(template, s, ((Array)o).Join("<br/>")));
               jQuery.Select("#ah" + s).Change(HandleAccordionSelectAll);
           });
            Script.Literal("$('#FieldChoices').accordion({header: '> div > h3', collapsible: true, heightStyle:'content' } )");

        }
        public static void HandleSelectAll(jQueryEvent eventArgs)
        {
            bool isChecked = jQuery.Select("#" + eventArgs.Target.ID).Is(":checked");
            jQuery.Select("div[id^='group'] input[type='checkbox']").Each(delegate(int x, Element e)
                {
                    ((CheckBoxElement)e).Checked = isChecked;
                });

        }
        public static void HandleAccordionSelectAll(jQueryEvent eventArgs)
        {
            string accordian = eventArgs.Target.ID.Substr(2);
            bool isChecked = jQuery.Select("#" + eventArgs.Target.ID).Is(":checked");
            jQuery.Select("#group" + accordian + " input[type='checkbox']").Each(delegate(int i, Element e)
           {
               ((CheckBoxElement)e).Checked = isChecked;
           });
            //For some reason we need to also set the actual check box checked
            // jQuery.Select("#" + eventArgs.Target.ID).Attribute("checked", "checked");
        }
        public static void LogOutOfFacebook(jQueryEvent eventArgs)
        {
            Facebook.logout(delegate()
            {

            });
        }
        public static void Hide(jQueryObject element)
        {

            Script.SetTimeout(delegate()
            {
                element.Hide();
            }, 0);

        }
        public static void Show(jQueryObject element)
        {
            Script.SetTimeout(delegate()
            {
                element.Show();
            }, 0);

        }
        public static void InsertFriends(jQueryEvent eventArgs)
        {
            Show(Modal);
            Hide(Insert);
            Hide(Friend);
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
                //  SelectObject obj = Office.Select("bindings#" + TableBinding);
                //obj.SetDataAsync(td,options, delegate(ASyncResult result)
                Office.Context.Document.SetSelectedDataAsync(td, options, delegate(ASyncResult result)
                {
                    if (result.Status == AsyncResultStatus.Failed)
                    {
                        Script.Literal("document.write({0} + ' : '+{1})", result.Error.Name, result.Error.Message);
                    }
                    if (result.Status == AsyncResultStatus.Succeeded)
                    {
                        BindingOptions bindingOptions = new BindingOptions();
                        bindingOptions.ID = TableBinding;


                        Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Table, bindingOptions, delegate(ASyncResult bindingResult)
                        {
                            Office.Select("bindings#" + TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, new BindingSelectionChanged(HandleTableSelection));
                        });
                        Hide(Modal);
                        Show(Friend);
                    }
                });
            });
        }
        public static void HandleTableSelection(BindingSelectionChangedEventArgs args)
        {
            if (args.StartRow > 0) // do nothing when the header column is selected
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
                    if (UIResp.Post_id != null && UIResp.Post_id != "")
                    {
                        Script.Literal("document.write({0});", UIResp.Post_id);
                    }
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
