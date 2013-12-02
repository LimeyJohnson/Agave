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
                initOptions.Scope = new string[] { "wl.skydrive_update", "wl.signin" };
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
                jQuery.Select("#createFolder").Click(new jQueryEventHandler(CreateFolder));
                LiveApi.Ui(new UiOptions("name","skydrivepicker","mode", "open", "element", "picker","onselected",new Action<LoginResponse>(OnSuccess)));
            };
        }

        private static void CreateFolder(jQueryEvent e)
        {
            //Folder.CreateFolder("MyNewFolderAgain","My brand new folder").Then(OnSuccess, OnFailure);
            FolderHelper.GetRootFolder.Then(delegate(Response response) 
            {
                Folder folderResponse = (Folder)response;
                SetTextBox(folderResponse.ID);
            });
        }

        private static void SetTextBox(string p)
        {
            jQuery.Select("#first_name").Value(p);
        }
        public static void OnLog(Response response)
        {
            jQuery.Select("#first_name").Value("Log");
        }
        public static void OnLogon(Response response)
        {
            jQuery.Select("#first_name").Value(response.Status);
        }
        public static void GetName(Response response)
        {
            LiveApi.Api(new ApiOptions("path", "me", "method", "GET")).Then(delegate(Response newResponse)
            {
                ApiResponse apiResponse = (ApiResponse)newResponse;
                jQuery.Select("#first_name").Value(apiResponse.FirstName);
            });
        }
        public static void OnFailure(Response failResponse)
        {
            jQuery.Select("#first_name").Value("Fail");
        }
        public static void OnSuccess(Response successResponse)
        {
            jQuery.Select("#first_name").Value("Pass");
           
        }
        
    }
}