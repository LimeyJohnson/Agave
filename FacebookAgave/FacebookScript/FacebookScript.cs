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
                options.channelUrl = "http://localhost:62587/pages/channel.ashx";
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
                jQuery.Select("#btnlogon").Click(new jQueryEventHandler(LogIntoFacebook));
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
                Office.Context.Document.Bindings.GetByIdAsync(TableBinding, delegate(ASyncResult result)
               {
                   if (result.Error == null)
                   {
                       Office.Select("bindings#" + TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, new BindingSelectionChanged(HandleTableSelection));
                   }
               });
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
        public static void LogIntoFacebook(jQueryEvent eventArgs)
        {
            LoginOptions LoginOptions = new LoginOptions();
            LoginOptions.scope = "email,friends_education_history, friends_likes,user_activities,friends_activities,user_birthday,friends_birthday,user_education_history,friends_education_history,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_website,user_work_history,friends_work_history";
            Facebook.login(HandleFacebookAuthEvent, LoginOptions);
        }
        public static void InitFields()
        {
            fields = new Dictionary<string, Field>();
            fields["uid"] = new RequiredField("uid", "FBID");
            fields["first_name"] = new Field("first_name", "First Name", "Basic", true);
            fields["last_name"] = new Field("last_name", "Last Name", "Basic", true);
            fields["birthday_date"] = new Field("birthday_date", "Birthday", "Basic", true);
            fields["sex"] = new Field("sex", "Sex", "Basic", true);
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
            fields["sports"] = new ArrayField("sports", "Sports", "name", "Extended");
            fields["status_Message"] = new StructField("status", "Current Status", "message", null, "Extended");

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
            Office.Context.Document.Settings.SaveAsync(delegate(ASyncResult SaveResult) { });
        }
        public static void UpdateFieldChecked(string ID, bool isChecked)
        {

            jQuery.Each(fields, delegate(string s, object o)
            {
                Field f = (Field)o;
                if (f.ID == ID)
                {
                    f.UpdateChecked(isChecked);
                    Office.Context.Document.Settings.SaveAsync(delegate(ASyncResult SaveResult) { });
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
                GetDataAsyncOptions options = new GetDataAsyncOptions();
                options.CoercionType = CoercionType.Table;
                Office.Context.Document.Bindings.GetByIdAsync(TableBinding, delegate(ASyncResult result)
                {
                    if (result.Error == null)
                    {
                        //The binding already exists... use it
                        BindingObject binding = (BindingObject)result.Value;
                        // Now we have to do a getData to see if we need to add any columns
                        binding.GetDataAsync(options, delegate(ASyncResult getDataResult)
                        {
                            int columnDiff = td.HeadersDouble[0].Length - getDataResult.TableValue.HeadersDouble[0].Length;
                            if (columnDiff > 0)
                            {
                                TableData addColumnTable = GenerateTableData(columnDiff, getDataResult.TableValue.Rows.Length);
                                binding.AddColumnsAsync(addColumnTable, delegate(ASyncResult addColumnResult)
                                {
                                    Office.Context.Document.Bindings.GetByIdAsync(TableBinding, delegate(ASyncResult newBindResult)
                                     {
                                         BindingObject newBinding = (BindingObject)newBindResult.Value;
                                         GetDataAsyncOptions setoptions = new GetDataAsyncOptions();
                                         setoptions.CoercionType = CoercionType.Table;
                                         newBinding.SetDataAsync(td, setoptions, delegate(ASyncResult callResult)
                                         {
                                             Requests.LogAction("Insert Data", UserID, "", "Existing Table Resize");
                                             if (callResult.Status == AsyncResultStatus.Failed)
                                             {
                                                 SetError("An error has occurred please try again");
                                                 Requests.LogAction("Insert Data", UserID, "Message: " + callResult.Error.Message + " Code: " + callResult.Error.Code, "Existing Table Resize");
                                             }
                                             else
                                             {
                                                 Requests.LogAction("Insert Data", UserID, "", "Existing Table");
                                             }
                                         });
                                     });
                                });
                            }
                            else
                            {
                                GetDataAsyncOptions newOptions = new GetDataAsyncOptions();
                                newOptions.CoercionType = CoercionType.Table;
                                binding.SetDataAsync(td, newOptions, delegate(ASyncResult callResult)
                                {
                                    if (result.Status == AsyncResultStatus.Failed)
                                    {
                                        SetError("An error has occurred please try again");
                                        Requests.LogAction("Insert Data", UserID, "Message: " + callResult.Error.Message + " Code: " + callResult.Error.Code, "Existing Table");
                                    }
                                    else
                                    {
                                        Requests.LogAction("Insert Data", UserID, "", "Existing Table");
                                    }
                                });
                            }

                        });
                    }
                    else
                    {
                        //the binding does not exist, insert data and set binding
                        Office.Context.Document.SetSelectedDataAsync(td, options, delegate(ASyncResult setresult)
                        {
                            if (setresult.Status == AsyncResultStatus.Failed)
                            {
                                Requests.LogAction("Insert Data", UserID, "Message: " + setresult.Error.Message + " Code: " + setresult.Error.Code, "New Table");
                                if (setresult.Error.Code == 2003) // this is the not enough size exception
                                {
                                    SetError("Setting the friends list here would overwrite data in the spread sheet. Please select another area");
                                }
                                else
                                {
                                    SetError("An error has occurred please try again");
                                }
                            }
                            if (setresult.Status == AsyncResultStatus.Succeeded)
                            {
                                BindingOptions bindingOptions = new BindingOptions();
                                bindingOptions.ID = TableBinding;
                                Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Table, bindingOptions, delegate(ASyncResult bindingResult)
                                {
                                    Office.Select("bindings#" + TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, new BindingSelectionChanged(HandleTableSelection));
                                });
                                Requests.LogAction("Insert Data", UserID, "", "New Table");
                            }
                        });
                    }
                });
                UpdateFriendView((string)td.Rows[0][0]);
                SetView(Friend);
            });
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
                            UpdateFriendView(((string)result.TableValue.Rows[0][0]));
                        }
                    });
                }
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
