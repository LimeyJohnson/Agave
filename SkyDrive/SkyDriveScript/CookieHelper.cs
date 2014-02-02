// SkyDrive.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using Live;
using System.Html.Data.Files;
namespace SkyDriveScript
{

    public static class CookieHelper
    {
        private static string AccessTokenString = "access_token";
        private static string WLAuth = "wl_auth";
        public static string AccessToken
        {
            get
            {
                string[] cookiePairs = Document.Cookie.Split(";");
                for (int i = 0; i < cookiePairs.Length; i++)
                {
                    string[] cookie = cookiePairs[i].Split("=");
                    if (cookie[0].Trim() == WLAuth)
                    {
                        string[] authPairs = cookiePairs[i].Replace(WLAuth+"=", "").Split("&");
                        for (int x = 0; x < authPairs.Length; x++)
                        {
                            string[] authPair = authPairs[x].Split("=");
                            if (authPair[0].Trim() == AccessTokenString)
                            {
                                return authPair[1];
                            }
                        }
                    }
                }
                throw new Exception("Could not find authtoken");
            }
        }
        
    }
}