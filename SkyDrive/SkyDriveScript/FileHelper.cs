// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using System.Net;
using Live;
using System.Html.Data.Files;
using System.Collections;
using AppForOffice;
namespace SkyDriveScript
{

    public static class FileHelper
    {
        public static XmlHttpRequest request;
        public static string APIBaseUrl = @"https://apis.live.net/v5.0";
        public static int Counter = 0;
        public static ArrayList Files;
        public static File CurrentFile;
        public static FileReader Reader;
        public static int RecordID =1;
        static FileHelper()
        {
            Files = new ArrayList();
        }

        public static void AddFileToUploadQueue(File newFiles)
        {
            Files.Add(newFiles);
            LoadNextFile();
        }

        private static void LoadNextFile()
        {
            if (CurrentFile == null && Files.Count > 0)
            {
                CurrentFile = (File)Files[0];
                Reader = new FileReader();
                Reader.OnLoad = new Action<FileProgressEvent>(OnFileLoad);
                Reader.ReadAsArrayBuffer(CurrentFile);
            }
            else
            {
                //We are truly done at this point refresh the view
                FolderHelper.RefreshView(RecordID, "filelist");
            }
            
        }

        private static void OnFileLoad(FileProgressEvent arg)
        {
            SkyDrive.SetTextBox("File Loaded" + CurrentFile.Name);
            SaveFileNoApi(Reader.Result);
        }

        public static void SaveFileNoApi(object fileContents)
        {
            GetDataAsyncOptions gdo = new GetDataAsyncOptions();
            gdo.Rows = RowType.ThisRow;
            gdo.CoercionType = CoercionType.Matrix;
            Office.Select("bindings#" + SkyDrive.TableBinding).GetDataAsync(gdo, delegate(ASyncResult result)
            {
                RecordID = (int)result.MatrixValue[0][0];
                FolderHelper.GetRecordFolderID(RecordID, delegate(string folderID)
                {
                    request = new XmlHttpRequest();
                    string URL = string.Format("{0}/{1}/files/{2}?access_token={3}", APIBaseUrl, folderID, CurrentFile.Name, CookieHelper.AccessToken);
                    request.Open("PUT", URL, true);
                    request.OnReadyStateChange = OnReadyChange;
                    request.OnError = OnUploadError;
                    request.OnLoad = OnLoad;
                    request.ResponseType = XmlHttpRequestResponseType.Json;
                    request.Upload.OnProgress = OnUploadProgress;
                    request.Send(fileContents);
                });
            });
        }

        public static void OnLoad(XmlHttpRequestProgressEvent arg)
        {
            SkyDrive.SetTextBox("DONE "+CurrentFile.Name);
            Files.Remove(CurrentFile);
            CurrentFile = null;
            LoadNextFile();
        }

        public static void OnUploadError(XmlHttpRequestProgressEvent arg)
        {
            SkyDrive.SetTextBox("Error During Upload");
        }

        public static void OnUploadProgress(XmlHttpRequestProgressEvent arg)
        {
            int progress = (int) (((ulong)arg.Loaded/CurrentFile.Size) * 100);
            SkyDrive.SetProgressTextBox(string.Format("{2} Loaded:{0}, Total:{1}, {3}%", arg.Loaded, CurrentFile.Size, (Counter++), progress));
        }


        public static void OnReadyChange()
        {
            switch(request.ReadyState)
            {
                case ReadyState.Open: SkyDrive.SetTextBox("Open" + (Counter++));
                    break;
                case ReadyState.Uninitialized: SkyDrive.SetTextBox("Uninitialized" + (Counter++));
                    break;
                case ReadyState.HeadersReceived: SkyDrive.SetTextBox("HeadersReceived" + (Counter++));
                    break;
                case ReadyState.Receiving: SkyDrive.SetTextBox("Receiving" + (Counter++));
                    break;
                case ReadyState.Done: SkyDrive.SetTextBox("Done" + (Counter++));
                    break;
            }
        }
        

    }
}