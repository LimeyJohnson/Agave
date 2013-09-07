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
                InitOptions initOptions = new InitOptions();
                initOptions.client_id = "000000004C100093";
                initOptions.redirect_uri = "http://filesagave.azurewebsites.net/App/Home/callback.html";
                initOptions.scope = "wl.signin";
                initOptions.response_type = "token";
                initOptions.logging = true;
                LiveApi.Init(initOptions).Then(OnSuccess, OnFailure) ;

                UiOptions uiOptions = new UiOptions();
                uiOptions.name = "signin";
                uiOptions.element = "signin";
                LiveApi.Ui(uiOptions);
                

                
            };
        }
        public static void OnLogon(LoginResponse response)
        {
            LiveApi.Login(new Dictionary<string, string>("scope", new string[] { "wl.signin", "wl.basic", "wl.birthday", "wl.emails" })).Then(delegate(LoginResponse loginResponse)
            {
                LiveApi.Api(new Dictionary<string, string>("path", "me", "method", "GET")).Then(delegate(Dictionary<string, string> apiResponse)
                {
                    jQuery.Select("#first_name").Text(apiResponse["first_name"]);
                });
            });
        }
        public static void OnFailure(LoginResponse failResponse)
        {
            jQuery.Select("#first_name").Text(failResponse.ToString());
        }
        public static void OnSuccess(LoginResponse successResponse)
        {
            jQuery.Select("#first_name").Text(successResponse.ToString());
        }
    }
}
