// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AgaveApi;
using Live;
namespace SkyDriveScript
{

    public static class SkyDrive
    {
        static SkyDrive()
        {
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                LiveApi.Event.subscribe("auth.login", OnLogon);
                LiveApi.Event.subscribe("wl.log", OnLog);
                InitOptions initOptions = new InitOptions();
                initOptions.client_id = "000000004C100093";
                initOptions.redirect_uri = "http://filesagave.azurewebsites.net/App/callback.html";
                //initOptions.scope = "wl.signin";
                //initOptions.response_type = "token";
                initOptions.logging = true;
                LiveApi.Init(initOptions).Then(OnSuccess, OnFailure) ;

                UiOptions uiOptions = new UiOptions();
                uiOptions.name = "signin";
                uiOptions.element = "signin";
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
            LiveApi.Api(new Dictionary<string, string>("path", "me", "method", "GET")).Then(delegate(Dictionary<string, string> apiResponse)
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
        }
    }
}
