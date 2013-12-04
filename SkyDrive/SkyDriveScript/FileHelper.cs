// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using System.Net;
using Live;
using System.Html.Data.Files;
namespace SkyDriveScript
{

    public static class FileHelper
    {
        public static XmlHttpRequest request;
        public static string APIBaseUrl = @"https://apis.live.net/v5.0";
        public static int Counter = 0;
        public static PromiseGeneric<Response> SaveFile(string folderID, string fileName, string fileContents)
        {
            string path = string.Format(@"/{0}/files/{1}", folderID, fileName);
            return LiveApi.Api(new ApiOptions("path", path, "method", "put", "body", fileContents));
        }

        public static void SaveFileNoApi(string folderID, string fileName, string fileContents)
        {
            request = new XmlHttpRequest();

            string URL = string.Format("{0}/{1}/files/{2}?access_token={3}", APIBaseUrl, folderID, fileName, CookieHelper.AccessToken);
            request.Open("PUT", URL, true);
            request.OnReadyStateChange = OnReadyChange;
            request.OnProgress = OnUploadProgress;
          //  request.OnError = OnUploadError;
          //  request.OnLoad = OnLoad;
            request.ResponseType = XMLHttpRequestResponseType.Json;
            Script.Literal("{0}.upload.onprogress = {1};", request, new Action<XmlHttpRequestProgressEvent>(OnUploadProgress));
            request.Send(fileContents);
        }

        public static void OnLoad(XmlHttpRequestProgressEvent arg)
        {
            SkyDrive.SetTextBox("DONE "+(Counter++) + request.ResponseText);
        }

        public static void OnUploadError(XmlHttpRequestProgressEvent arg)
        {
            SkyDrive.SetTextBox("Error During Upload");
        }

        public static void OnUploadProgress(XmlHttpRequestProgressEvent arg)
        {
            int progress = (int) (((ulong)arg.Loaded/SkyDrive.FileSize) * 100);
            SkyDrive.SetProgressTextBox(string.Format("{2} Loaded:{0}, Total:{1}, {3}%", arg.Loaded, SkyDrive.FileSize, (Counter++), progress));

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