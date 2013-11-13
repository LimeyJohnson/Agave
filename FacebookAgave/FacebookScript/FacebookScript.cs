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
        public static Dictionary<string, Field> fields;
        private static jQueryObject Logon;
        private static jQueryObject Insert;
        private static jQueryObject Friend;
        private static jQueryObject Modal;
        private static jQueryObject Main;
        private static jQueryObject ErrorDiv;
        private static Array Views = new Array();
        private static bool FacebookInited = false;

        static FacebookScript()
        {

            FacebookWindow.AsyncInit = delegate()
            {
                InitOptions options = new InitOptions();
#if DEBUG
                options.appId = "143445839182832";
                //options.channelUrl = "http://localhost:62587/pages/channel.ashx";
#else

                options.channelUrl = "https://friendsinoffice.com/pages/channel.ashx";
                options.appId = "263395420459543";
#endif
                options.status = true;
                options.cookie = false;
                Facebook.init(options);

                //Facebook.Event.subscribe("auth.authResponseChange", new EventChange(HandleFacebookAuthEvent));
                Facebook.getLoginStatus(HandleFacebookAuthEvent);


                FacebookInited = true;
                jQuery.Select("body").Height(jQuery.Window.GetHeight());
                jQuery.Select("body").Width(jQuery.Window.GetWidth() - 25);
            };
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                jQuery.Select("#GetFriends").Click(new jQueryEventHandler(InsertFriends));
                jQuery.Select("#LogOut").Click(new jQueryEventHandler(LogOutOfFacebook));
                jQuery.Select("#postfriendstatus").Click(new jQueryEventHandler(PostFriendStatus));
                jQuery.Select("#btnlogon").Click(new jQueryEventHandler(HandleFacebookLogon));
                jQuery.Select("#insertfreinds").Click(new jQueryEventHandler(InsertFriends));
                //Sync up goto main buttons they are all insert tags ending in main
                jQuery.Select("img[id$='main']").Click(delegate(jQueryEvent e) { SetView(Main); });
                jQuery.Select("#settings").Click(delegate(jQueryEvent eventargs) { SetView(Insert); });
                //Office.Context.Document.Settings.RefreshAsync(delegate(ASyncResult result) { });
                Friend = jQuery.Select("#friend");
                Views[Views.Length] = Friend;
                Logon = jQuery.Select("#logon");
                Views[Views.Length] = Logon;
                Insert = jQuery.Select("#insert");
                Views[Views.Length] = Insert;
                Modal = jQuery.Select("#modal");
                Views[Views.Length] = Modal;
                Main = jQuery.Select("#main");
                Views[Views.Length] = Main;
                ErrorDiv = jQuery.Select("#error");
                Views[Views.Length] = ErrorDiv;

                InitFields();
                InsertAccordions();
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
                //BindingOptions bo = new BindingOptions();
                //bo.ID = TableBinding;
                //bo.columnNames = new string[] {"FBID","First Name","Last Name","Birthday","Gender"};
                //Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Table, bo, delegate(ASyncResult bindingResult)
                //{
                //    Office.Select("bindings#" + TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, new BindingSelectionChanged(HandleTableSelection));
                //});
                Requests.LogAction("Init", UserID ?? "unknown", "", "");
            };

        }
        public static void SetError(string ErrorText)
        {
            ErrorDiv.Find("#errorText").Text(ErrorText);
            SetView(ErrorDiv);
        }
        public static void SetView(jQueryObject view)
        {
            jQuery.Each(Views, delegate(int i, object o)
            {
                if (o == view)
                {
                    Show((jQueryObject)o);
                }
                else
                {
                    Hide((jQueryObject)o);
                }
            });
        }
        public static void HandleFacebookAuthEvent(LoginResponse response)
        {
            if (response.status == "connected")
            {
                UserID = response.authResponse.userID;
                AccessToken = response.authResponse.accessToken;
                SetView(Main);
                Requests.LogAction("LogIn", UserID ?? "Unknown", "", AccessToken);
            }
            else
            {
                //Needs to be called before the session
                Requests.LogAction("LogOut", UserID ?? "Unknown", "", AccessToken);
                UserID = null;
                AccessToken = null;
                SetView(Logon);

            }

        }
        public static void HandleFacebookLogon(jQueryEvent eventArgs)
        {
            Requests.LogAction("Login Button Pressed", UserID, "", "");
            LogonToFacebook("email", HandleFacebookAuthEvent);
        }
        public static void LogonToFacebook(string scope, Action<LoginResponse> callback)
        {
            LoginOptions LoginOptions = new LoginOptions();
            LoginOptions.scope = scope;
            Facebook.login(callback, LoginOptions);
        }
        public static void InitFields()
        {
            fields = new Dictionary<string, Field>();
            fields["uid"] = new RequiredField("uid", "FBID", "1");
            fields["first_name"] = new Field("first_name", "First Name", "Basic", null, "Andrew", true);
            fields["last_name"] = new Field("last_name", "Last Name", "Basic", null, "Johnson", true);
            fields["birthday_date"] = new Field("birthday_date", "Birthday", "Basic", "friends_birthday", "07/20/1986", true);
            fields["sex"] = new Field("sex", "Gender", "Basic", null, "Male", true);
            fields["mutual_friend_count"] = new Field("mutual_friend_count", "Mutual Friends", "Counts", null, "360");
            fields["quotes"] = new Field("quotes", "Quotes", "Extended", "friends_likes", "To each his own", false);
            fields["political"] = new Field("political", "Political", "Extended", "friends_religion_politics", "Centrist");
            fields["relationship_status"] = new Field("relationship_status", "Relationship Status", "Extended", "friends_relationships", null);
            fields["religion"] = new Field("religion", "Religion", "Extended", "friends_religion_politics", "Religion");
            fields["wall_count"] = new Field("wall_count", "Wall Count", "Counts", null, "2045");
            fields["friend_count"] = new Field("friend_count", "Friend Count", "Counts", null, "360");
            fields["work_Employer"] = new StructField("work", "Employer", "employer", "name", "Employment", "friends_work_history", "Microsoft", 0);
            fields["work_Position"] = new StructField("work", "Position", "position", "name", "Employment", "friends_work_history", "Microsoft", 0);
            fields["current_location_City"] = new StructField("current_location", "Current City", "city", null, "Location", "friends_location", "Redmond");
            fields["current_location_State"] = new StructField("current_location", "Current State", "state", null, "Location", "friends_location", "Washington");
            fields["current_location_Country"] = new StructField("current_location", "Current Country", "country", null, "Location", "friends_location", "USA");
            fields["interests"] = new Field("interests", "Interests", "Extended", "friends_interests", "Bowling");
            fields["profile_url"] = new Field("profile_url", "Profile URL", "Extended", null, "profile URL");
            fields["sports"] = new ArrayField("sports", "Sports", "name", "Extended", "friends_likes", "Soccer");
            fields["status_Message"] = new StructField("status", "Current Status", "message", null, "Extended", null, "Latest Status Message");

        }
        public static void InsertAccordions()
        {
            jQueryObject comboBoxLocation = jQuery.Select("#fieldchoices");
            Dictionary<string, Array> accordions = new Dictionary<string, Array>();
            jQuery.Each(fields, delegate(string s, object o)
            {
                Field f = (Field)o;
                if (f.ContainerName != null)
                {
                    if (accordions[f.ContainerName] == null) accordions[f.ContainerName] = new Array();
                    accordions[f.ContainerName][accordions[f.ContainerName].Length] = f;
                }
            });
            jQuery.Each(accordions, delegate(string s, object o)
           {
               Array checkBoxHtml = new Array();
               bool selectAllCheckboxSelected = true;
               jQuery.Each((Array)o, delegate(int i, object field)
               {
                   if (!((Field)field).m_checked)
                   {
                       selectAllCheckboxSelected = false;
                   }
                   checkBoxHtml[checkBoxHtml.Length] = ((Field)field).Html;
               });
               string template = "<div class='group' id='group{0}'><h3>{0}</h3><div><input id='ah{0}' {1} type='checkbox'  />Select All<br/>{2}</div></div>";

               comboBoxLocation.Append(string.Format(template, s, selectAllCheckboxSelected ? "checked='checked'" : "", checkBoxHtml.Join("<br/>")));
               jQuery.Select("#ah" + s).Change(HandleAccordionSelectAll);
           });
            Script.Literal("$('#fieldchoices').accordion({header: '> div > h3', collapsible: true, heightStyle:'content' } )");
            jQuery.Select("input[id^='" + Field.checkBoxPrefix + "']").Change(HandleFieldChange);
            // Office.Context.Document.Settings.SaveAsync(delegate(ASyncResult SaveResult) { });
        }
        public static void UpdateFieldChecked(string ID, bool isChecked)
        {

            jQuery.Each(fields, delegate(string s, object o)
            {
                Field f = (Field)o;
                if (f.ID == ID)
                {
                    f.UpdateChecked(isChecked);
                    //Office.Context.Document.Settings.SaveAsync(delegate(ASyncResult SaveResult) { });
                }
            });
        }
        public static void HandleFieldChange(jQueryEvent eventArgs)
        {
            bool isChecked = jQuery.Select("#" + eventArgs.Target.ID).Is(":checked");
            UpdateFieldChecked(eventArgs.Target.ID, isChecked);

        }
        public static void HandleAccordionSelectAll(jQueryEvent eventArgs)
        {
            string accordian = eventArgs.Target.ID.Substr(2);
            bool isChecked = jQuery.Select("#" + eventArgs.Target.ID).Is(":checked");
            jQuery.Select("#group" + accordian + " input[type='checkbox']").Each(delegate(int i, Element e)
           {
               ((CheckBoxElement)e).Checked = isChecked;
               UpdateFieldChecked(e.ID, isChecked);
           });
            //For some reason we need to also set the actual check box checked
            // jQuery.Select("#" + eventArgs.Target.ID).Attribute("checked", "checked");
        }
        public static void LogOutOfFacebook(jQueryEvent eventArgs)
        {
            Facebook.logout(HandleFacebookAuthEvent);
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
            SetView(Modal);
            TableData td = new TableData();
            td.HeadersDouble = new Array[1];
            td.HeadersDouble[0] = new Array();
            TableData sample = new TableData();
            sample.HeadersDouble = new Array[1];
            sample.HeadersDouble[0] = new Array();
            sample.Rows = new Array[1][];
            sample.Rows[0] = new Array[1];
            Array fieldNames = new Array();
            
            Array permissions = new Array();
            Dictionary<string, Field> dict = new Dictionary<string, Field>();
            jQuery.Each(fields, delegate(string name, object value)
            {
                Field ff = (Field)value;
                if (ff.Checked)
                {
                    dict[name] = ff;
                    td.HeadersDouble[0][td.HeadersDouble[0].Length] = ff.DisplayText;
                    sample.HeadersDouble[0][td.HeadersDouble[0].Length] = ff.DisplayText;
                    sample.Rows[0][sample.Rows[0].Length] = ff.Sample;
                    fieldNames[fieldNames.Length] = ff.FieldName;
                    if (ff.Permission != null && permissions.IndexOf(ff.Permission) < 0) permissions[permissions.Length] = ff.Permission;
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
            PromptBindingOptions bo = new PromptBindingOptions();
            bo.ID = TableBinding;
            bo.PromptText = "Please map the field that will be imported from facebook to field in your database";
            //bo.sampleData = sample;
            Office.Context.Document.Bindings.AddFromPromptAsync(bo, delegate(ASyncResult promptResult)
            {
                LogonToFacebook(permissions.Join(), delegate(LoginResponse logonResponse)
                {
                    if (logonResponse.status == "connected")
                    {
                        Facebook.api("fql", queryOptions, delegate(ApiResponse response)
                        {
                            if (Script.Boolean(response.error))
                            {
                                Requests.LogAction("GetDataFromFacebook", UserID, response.error, "Could not get data from facebook");
                            }
                            else
                            {
                                InsertFreindsIntoExcel(response.data, td, dict);
                            }
                        });
                    }
                    else
                    {
                        HandleFacebookAuthEvent(logonResponse);
                    }
                });

            });
        }
        public static void InsertFreindsIntoExcel(Dictionary[] data, TableData td, Dictionary<string, Field> dict)
        {
            td.Rows = new string[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                td.Rows[i] = new string[td.HeadersDouble[0].Length];
                for (int y = 0; y < td.HeadersDouble[0].Length; y++)
                {
                    string fn = dict.Keys[y];
                    Field f = fields[fn];
                    td.Rows[i][y] = f.ParseResult(data[i]);
                }
            }
            GetDataAsyncOptions options = new GetDataAsyncOptions();
            options.CoercionType = CoercionType.Table;
            BindingOptions bo = new BindingOptions();
            bo.columnNames = (Array)td.Headers[0];
            bo.ID = TableBinding;
            Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Table, bo, delegate(ASyncResult createBindingCallback)
            {
                Office.Select("bindings#" + TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, HandleTableSelection, delegate(ASyncResult addHandlerResult)
                {
                    Requests.LogAction("Something", "IO", "Error", "Message");
                });
                GetDataAsyncOptions newOptions = new GetDataAsyncOptions();
                newOptions.CoercionType = CoercionType.Table;
                Office.Select("bindings#" + TableBinding).SetDataAsync(td, newOptions, delegate(ASyncResult callResult)
                {
                    if (callResult.Status == AsyncResultStatus.Failed)
                    {
                        SetError("An error has occurred please try again");
                        Requests.LogAction("Insert Data", UserID, "Message: " + callResult.Error.Message + " Code: " + callResult.Error.Code, "Existing Table");
                    }
                    else
                    {

                        Requests.LogAction("Insert Data", UserID, "", "Existing Table");
                    }
                });
            });
            // UpdateFriendView((string)td.Rows[0][0]);
            SetView(Friend);

        }
        public static TableData GenerateTableData(int size, int length)
        {
            TableData td = new TableData();
            td.HeadersDouble = new Array[1];
            td.HeadersDouble[0] = new Array();
            td.Rows = new string[length][];
            for (int x = 0; x < size; x++)
            {
                td.HeadersDouble[0][td.HeadersDouble[0].Length] = "Column" + x;
            }
            for (int y = 0; y < length; y++)
            {
                td.Rows[y] = new string[size];
                for (int z = 0; z < size; z++)
                {
                    td.Rows[y][z] = "Data" + y + z;
                }
            }
            return td;
        }
        public static void SetProfilePic(string FriendID)
        {
            jQuery.Select("#profilepic").CSS("background", "url(http://graph.facebook.com/" + FriendID + "/picture?width=200&height=200) no-repeat center center");
        }
        public static void HandleTableSelection(BindingSelectionChangedEventArgs args)
        {
            if (UserID != null)
            {
                GetDataAsyncOptions options = new GetDataAsyncOptions();
                options.CoercionType = CoercionType.Table;
                options.ScopeType = ScopeType.SelectedRows;
                Office.Select("bindings#" + TableBinding).GetDataAsync(options, delegate(ASyncResult result)
                {
                    if (result.Status == AsyncResultStatus.Succeeded)
                    {
                        UpdateFriendView(((string)result.TableValue.Rows[0][0]));
                    }
                });
                SetView(Friend);
            }
        }
        public static void UpdateFriendView(string friendID)
        {
            Array friendsNames = new Array();
            SetProfilePic(friendID);
            if (UserID != null)
            {
                Facebook.api(@"/" + friendID + "?fields=name", delegate(ApiResponse response)
                {
                    jQuery.Select("#friendname").Html(response.name);
                });

                string graphCall = UserID + @"/mutualfriends/" + friendID;
                Facebook.api(graphCall, delegate(ApiResponse response)
                {
                    for (int i = 0; i < response.data.Length; i++)
                    {
                        friendsNames[friendsNames.Length] = response.data[i]["name"];
                    }
                    friendsNames.Sort();
                    jQuery.Select("#friendlist").Html(friendsNames.Join("<br/>"));
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
    }
}
