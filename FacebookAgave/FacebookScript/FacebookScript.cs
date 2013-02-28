// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
namespace FacebookScript
{

    public static class FacebookScript
    {
        public static string UserID;
        public static string AccessToken;
        static FacebookScript()
        {

            Office.Initialize = delegate(InitializationEnum initReason)
            {

                InitOptions options = new InitOptions();
                options.channelUrl = "http://facebookagave.azurewebsites.net/pages/channel.ashx";
                options.appId = "263395420459543";
                options.status = true;
                options.cookie = false;
                Facebook.init(options);
                jQuery.Select("#GetFriends").Click(new jQueryEventHandler(InsertFriends));
                jQuery.Select("#FBLogin").Click(new jQueryEventHandler(delegate(jQueryEvent eventArgs)
                    {
                        LoginOptions LoginOptions = new LoginOptions();
                        LoginOptions.scope = "email,create_event,user_likes,publish_stream,user_about_me,friends_about_me,user_activities,friends_activities,user_birthday,friends_birthday,user_checkins,friends_checkins,user_education_history,friends_education_history,user_events,friends_events,user_groups,friends_groups,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_notes,friends_notes,user_photos,friends_photos,user_questions,friends_questions,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_subscriptions,friends_subscriptions,user_videos,friends_videos,user_website,user_work_history,friends_work_history";
                        Facebook.login(delegate(LoginResponse response)
                        {
                            UserID = response.authResponse.userID;
                        }, LoginOptions);
                    }));
                Facebook.getLoginStatus(delegate(LoginResponse loginResponse)
                {
                    if (loginResponse.status == "connected")
                    {
                        UserID = loginResponse.authResponse.userID;
                        AccessToken = loginResponse.authResponse.accessToken;
                    }
                });
            };
            Office.Initialize(InitializationEnum.DocumentOpenend);
        }
        public static void CheckTable()
        {
            TableData myTable = new TableData();
            myTable.HeadersDouble = new string[][] { new string[] { "Cities", "Names" } };
            myTable.Rows = new string[][] { new string[] { "Berlin", "Andrew" }, new string[] { "Roma", "Eric" }, new string[] { "Tokyo", "Johnson"}, new string[] { "Seattle","People" } };
            GetDataAsyncOptions options = new GetDataAsyncOptions();
            options.CoercionType = CoercionType.Table;
            Office.Context.Document.SetSelectedDataAsync(myTable, options, delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Failed)
                {
                    Script.Literal("write('Script Failed')");
                }
            });
        }
        public static void InsertFriends(jQueryEvent eventArgs)
        {
            string query = "Select first_name, last_name, birthday_date, sex, friend_count from user WHERE uid IN (SELECT uid2 from friend WHERE uid1 = me())";
            ApiOptions queryOptions = new ApiOptions();
            queryOptions.Q = query;
            TableData td = new TableData();
            td.HeadersDouble = new string[][] { new string[]{"First Name", "Last Name", "Birthday", "Gender", "Friend Count"} };

            Facebook.api("fql", queryOptions, delegate(ApiResponse response)
            {
                td.Rows = new string[response.data.Length][];
                for (int i = 0; i < response.data.Length; i++)
                {
                    td.Rows[i] = new string[5];
                    td.Rows[i][0] = response.data[i].first_name ?? "null";
                    td.Rows[i][1] = response.data[i].last_name ?? "null";
                    td.Rows[i][2] = response.data[i].birthday_date ?? "null";
                    td.Rows[i][3] = response.data[i].sex ?? "null";
                    td.Rows[i][4] = response.data[i].friend_count ?? "null";
                }
                GetDataAsyncOptions options = new GetDataAsyncOptions();
                options.CoercionType = CoercionType.Table;
                Office.Context.Document.SetSelectedDataAsync(td, options, delegate(ASyncResult result)
                {
                    if (result.Status == AsyncResultStatus.Failed)
                    {
                        Script.Literal("write({0} + ' : '+{1})", result.Error.Name, result.Error.Message);
                    }
                });
            });
            //Facebook.api(queryOptions, delegate(QueryResponse[] queryResponse)
            //{
            //    for (int i = 0; i < queryResponse[2].fql_result_set.Length; i++)
            //    {
            //        MultiQueryResults results = queryResponse[2].fql_result_set[i];
            //        td.Rows[i][0] = results.first_name;
            //        td.Rows[i][1] = results.last_name;
            //        td.Rows[i][2] = results.email;
            //        td.Rows[i][3] = results.sex;
            //    }
            //    GetDataAsyncOptions options = new GetDataAsyncOptions();
            //    options.CoercionType = CoercionType.Table;
            //    Office.Context.Document.SetSelectedDataAsync(td, options);
            //});

        }

    }
}
