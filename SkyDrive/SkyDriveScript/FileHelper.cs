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
            request.Send(fileContents);

        }

        private static void OnReadyChange()
        {
            if (request.ReadyState == ReadyState.Sent)
            {

            }
        }
        
    }
}