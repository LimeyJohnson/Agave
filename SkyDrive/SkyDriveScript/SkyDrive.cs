// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AppForOffice;
using Live;
using System.Html.Data.Files;
namespace SkyDriveScript
{
   
    public static class SkyDrive
    {
        public static string FolderID;
        public static string FileName;
        public static ulong FileSize;
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
                LiveApi.Init(initOptions).Then(OnInitSuccess, OnFailure);
                LiveApi.Event.subscribe("auth.login", OnLogon);
                LiveApi.Event.subscribe("wl.log", OnLog);
                Element dropzone = Document.GetElementById("dropzone");
                dropzone.AddEventListener("dragenter", NoOpHandler, false);
                dropzone.AddEventListener("dragexit", NoOpHandler, false);
                dropzone.AddEventListener("dragover", NoOpHandler, false);
                dropzone.AddEventListener("drop", Drop, false);
               
            };
        }
        public static void NoOpHandler(ElementEvent evt)
        {
            evt.StopPropagation();
            evt.PreventDefault();
        }

        public static void Drop(ElementEvent evt)
        {
            evt.StopPropagation();
            evt.PreventDefault();
            FileList fl = (FileList)Script.Literal("{0}.dataTransfer.files", evt);
            if (fl.Length > 0)
            {
                SetTextBox(fl[0].Name);
                for(int x = 0; x<fl.Length; x++)
                {
                    FileHelper.AddFileToUploadQueue(fl[x]);
                }
            }
        }
        private static void GetRootFolder()
        {
            //Folder.CreateFolder("MyNewFolderAgain","My brand new folder").Then(OnSuccess, OnFailure);
            FolderHelper.GetRootFolder.Then(delegate(Response response) 
            {
                Folder folderResponse = (Folder)response;
                SetTextBox(folderResponse.ID);
                FolderID = folderResponse.ID;
            });
        }

        public static void SetTextBox(string p)
        {
            jQuery.Select("#first_name").Value(p);
        }
        public static void SetProgressTextBox(string p)
        {
            jQuery.Select("#progress").Value(p);
        }
        public static void OnLog(Response response)
        {
            jQuery.Select("#first_name").Value("Log");
        }
        public static void OnLogon(Response response)
        {
            jQuery.Select("#first_name").Value(response.Status);
            GetName();
            GetRootFolder();
        }
        public static void GetName()
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
        public static void OnInitSuccess(Response successResponse)
        {
            UiOptions uiOptions = new UiOptions();
            uiOptions.Name = "signin";
            uiOptions.Element = "signin";
            uiOptions.brand = "skydrive";
            LiveApi.Ui(uiOptions);
            jQuery.Select("#first_name").Value("OnInitSuccess");
            LiveApi.Ui(new UiOptions("name", "skydrivepicker", "mode", "open", "element", "picker", "onselected", new Action<LoginResponse>(OnPickerSuccess)));
        }

        private static void OnPickerSuccess(LoginResponse arg)
        {
            SetTextBox("OnPickerSuccess");
        }
        
        
    }
}