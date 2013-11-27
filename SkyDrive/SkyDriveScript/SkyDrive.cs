// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AppForOffice;
using Live;
namespace SkyDriveScript
{

    public static class SkyDrive
    {
        static SkyDrive()
        {
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                InitOptions initOptions = new InitOptions();
                initOptions.client_id = "000000004C100093";
                initOptions.redirect_uri = "http://skydriveagave.azurewebsites.net/App/callback.html";
                initOptions.scope = "wl.signin";
                initOptions.response_type = "code";
                initOptions.logging = true;
                LiveApi.Init(initOptions).Then(OnSuccess, OnFailure);
                LiveApi.Event.subscribe("auth.login", OnLogon);
                LiveApi.Event.subscribe("wl.log", OnLog);
                UiOptions uiOptions = new UiOptions();
                uiOptions.Name = "signin";
                uiOptions.Element = "signin";
                uiOptions.brand = "skydrive";
                uiOptions.onloggedin = new Action<LoginResponse>(GetName);
                LiveApi.Ui(uiOptions);
            };
        }
        public static void OnLog(LoginResponse response)
        {
            jQuery.Select("#first_name").Value("Log");
        }
        public static void OnLogon(LoginResponse response)
        {
            jQuery.Select("#first_name").Value(response.status);
        }
        public static void GetName(LoginResponse response)
        {
            LiveApi.Api(new ApiOptions("path", "me", "method", "GET")).Then(delegate(Dictionary<string, string> apiResponse)
            {
                jQuery.Select("#first_name").Value(apiResponse["first_name"]);
            });
        }
        public static void OnFailure(LoginResponse failResponse)
        {
            jQuery.Select("#first_name").Value("Fail");
        }
        public static void OnSuccess(LoginResponse successResponse)
        {
            jQuery.Select("#first_name").Value("Pass");
            jQuery.Select("Something");
        }

    }
}