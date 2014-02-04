// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Live;
namespace SkyDriveScript
{

    public static class ViewManager
    {
        static List<jQueryObject> Views = new List<jQueryObject>();
        public static jQueryObject FileList;
        public static jQueryObject SignIn;
        public static jQueryObject Settings;
        public static jQueryObject Modal;
        public static jQueryObject FolderPicker;
        static ViewManager()
        {
            FileList = jQuery.Select("#filelist");
            Views.Add(FileList);
            SignIn = jQuery.Select("#signin");
            Views.Add(SignIn);
            Modal = jQuery.Select("#modal");
            Views.Add(Modal);
            FolderPicker = jQuery.Select("#folderpicker");
            Views.Add(FolderPicker);
        }

        public static void SwitchToView(jQueryObject view)
        {
            for (int x = 0; x < Views.Count; x++)
            {
                if (view == Views[x])
                {
                    Show(view);
                }
                else
                {
                    Hide(Views[x]);
                }
            }
        }

        public static void Hide(jQueryObject element)
        {

            Script.SetTimeout(delegate()
            {
                element.Hide();
            }, 0);

        }
        public static void Show(jQueryObject element)
        {
            Script.SetTimeout(delegate()
            {
                element.Show();
            }, 0);
        }
    }
}