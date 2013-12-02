// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using Live;
namespace SkyDriveScript
{

    public static class FolderHelper
    {
        public static PromiseGeneric<Response> CreateFolder(string folderName, string description)
        {
            return LiveApi.Api(new ApiOptions("path", "/me/skydrive", "method", "post", "body", new CreateFolderOptions("name", folderName, "description", description)));
        }
        public static PromiseGeneric<Response> GetRootFolder
        {
            get
            {
                return LiveApi.Api(new ApiOptions("path", "/me/skydrive", "method", "get"));
            }
        }
    }
}