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
        
        public static PromiseGeneric<Response> CreateFolder(string baseFolder, string folderName, string description)
        {
            return LiveApi.Api(new ApiOptions("path", baseFolder, "method", "post", "body", new CreateFolderOptions("name", folderName, "description", description)));
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
        public static void RefreshView()
        {
            //Clear out any files that in the list
            
            GetRecordFolderID(delegate(string folderID)
            {
                GetFolderContents(folderID).Then(delegate(FileListResponse response)
                {
                    ViewManager.FileList.Empty();
                    ViewManager.Hide(FileHelper.PB);
                    for (int x = 0; x < response.Files.Length; x++)
                    {
                        string template = "<li class='ui-widget-content'><a href={0}>{1}</a></li>";
                        string listtag = string.Format(template, response.Files[x].Source, response.Files[x].Name);
                        ViewManager.FileList.Append(listtag);
                    }
                }, SkyDrive.OnFailure);
            });
        }
        public static void GetRecordFolderID(Action<string> callback)
        {
            string folderID = null;
            RootFolderContents.Then(delegate(FileListResponse response)
            {
                for (int x = 0; x < response.Files.Length; x++)
                {
                    if (response.Files[x].Name == SkyDrive.CurrentID.ToString())
                    {
                        folderID = response.Files[x].ID;
                        callback(folderID);
                        return;
                    }
                    
                    
                }
                //If we reach here we need to create the folder
                CreateFolder(SkyDrive.FolderID, SkyDrive.CurrentID.ToString(), "Access Agave Record Folder for Record " + SkyDrive.CurrentID.ToString()).Then(delegate(Response r)
                {
                    Folder f = (Folder)r;
                    callback(f.ID);
                    return;
                });
            }, SkyDrive.OnFailure);

        }
    }
}