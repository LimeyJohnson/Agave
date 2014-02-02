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
        public static string FolderID = "folder.a729d230873cf73c.A729D230873CF73C!84491";
        public static string FileName;
        public static ulong FileSize;
        public static string TableBinding = "TableBinding";
        public static int CurrentID = 0;
        static SkyDrive()
        {
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                ViewManager.SwitchToView(ViewManager.SignIn);
                InitOptions initOptions = new InitOptions();
                initOptions.client_id = "000000004C100093";
                initOptions.redirect_uri = "https://friendsinoffice.com/skydrive/App/callback.html";
                initOptions.Scope = new string[] { "wl.skydrive_update", "wl.signin" };
                initOptions.response_type = "code";
                initOptions.logging = true;
                LiveApi.Init(initOptions).Then(OnInitSuccess, OnFailure);
                LiveApi.Event.subscribe("auth.login", OnLogon);
                LiveApi.Event.subscribe("wl.log", OnLog);
                LiveApi.GetLoginStatus().Then(OnLogon, OnFailure);
                    
                Element dropzone = Document.GetElementById("dropzone");
                dropzone.AddEventListener("dragenter", NoOpHandler, false);
                dropzone.AddEventListener("dragexit", NoOpHandler, false);
                dropzone.AddEventListener("dragover", NoOpHandler, false);
                dropzone.AddEventListener("drop", Drop, false);
                
                BindingOptions bo = new BindingOptions();
                bo.ID = TableBinding;
                bo.Columns = new string[]{"ID"};
                Office.Context.Document.Bindings.AddFromSelectionAsync(BindingType.Table, bo, delegate(ASyncResult result)
                {
                    Office.Select("bindings#"+TableBinding).AddHandlerAsync(EventType.BindingSelectionChanged, OnBindingSelectionChanged);
                    OnBindingSelectionChanged(null);
                });
                   
            };
        }
        public static void OnBindingSelectionChanged(BindingSelectionChangedEventArgs args)
        {
            GetCurrentRecordID(delegate(int record) { FolderHelper.RefreshView(); });
        }
        public static void GetCurrentRecordID(Action<int> callback)
        {
            GetDataAsyncOptions gdo = new GetDataAsyncOptions();
            gdo.Rows = RowType.ThisRow;
            gdo.CoercionType = CoercionType.Matrix;
            Office.Select("bindings#" + TableBinding).GetDataAsync(gdo, delegate(ASyncResult result)
            {
                if (result.Status == AsyncResultStatus.Succeeded && Script.Boolean(result.MatrixValue[0]) && Script.Boolean(result.MatrixValue[0][0]))
                {
                    int recordID = (int)result.MatrixValue[0][0];
                    CurrentID = recordID;
                    if (Script.Boolean(callback))
                    {
                        callback(recordID);
                    }
                }
            });
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
            jQuery.Select("#content-main").Hide();
            if (response.Status == "connected")
            {
                jQuery.Select("#first_name").Value(response.Status);
                ViewManager.SwitchToView(ViewManager.FileList);
            }
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
            uiOptions.onloggedin = OnLogon;
            LiveApi.Ui(uiOptions);
        }
    }
}