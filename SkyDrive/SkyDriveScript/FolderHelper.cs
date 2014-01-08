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
                return LiveApi.Api(new ApiOptions("path", "/me/skydrive/files", "method", "get"));
            }
        }
        public static PromiseGeneric<FileListResponse> RootFolderContents
        {
            get
            {
                return GetFolderContents(SkyDrive.FolderID);
            }
        }
        public static PromiseGeneric<FileListResponse> GetFolderContents(string folderID)
        {
            return LiveApi.FileListApi(new ApiOptions("path", folderID + "/files?download=true", "method", "get"));
        }
        public static void RefreshView(int record, string divID)
        {
            GetRecordFolderID(record, delegate(string folderID)
            {
                jQuery.Select("#" + divID).Empty();
                GetFolderContents(folderID).Then(delegate(FileListResponse response)
                {
                    for (int x = 0; x < response.Files.Length; x++)
                    {
                        string template = "<a href={0}>{1}</a><br/>";
                        string atag = string.Format(template, response.Files[x].Source, response.Files[x].Name);
                        jQuery.Select("#" + divID).Append(atag);
                    }
                }, SkyDrive.OnFailure);
            });
        }
        public static void GetRecordFolderID(int recordID, Action<string> callback)
        {
            string folderID = null;
            RootFolderContents.Then(delegate(FileListResponse response)
            {
                for (int x = 0; x < response.Files.Length; x++)
                {
                    if (response.Files[x].Name == recordID.ToString())
                    {
                        folderID = response.Files[x].ID;
                        callback(folderID);
                        return;
                    }
                }
            }, SkyDrive.OnFailure);

        }
    }
}