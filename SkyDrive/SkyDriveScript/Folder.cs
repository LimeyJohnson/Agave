// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using Live;
namespace SkyDriveScript
{

    public static class Folder
    {
        public static PromiseGeneric<ApiResponse> CreateFolder(string folderName, string description)
        {
            return LiveApi.Api(new ApiOptions("path", "/me/skydrive", "method", "post", "body", new CreateFolderOptions("name", folderName, "description", description)));
        }
        
    }
}