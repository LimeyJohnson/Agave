// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
namespace FacebookScript
{

    public static class FacebookScript
    {
        static FacebookScript()
        {
            Office.Initialize = delegate(InitializationEnum initReason)
            {

                InitOptions options = new InitOptions();
                options.channelUrl = "http://facebookagave.azurewebsites.net/pages/channel.ashx";
                options.appId = "263395420459543";
                options.status = true;
                options.cookie = false;
                Facebook.init(options);
                jQuery.Select("#FBLogin").Click(new jQueryEventHandler(delegate(jQueryEvent eventArgs)
                    {
                        LoginOptions LoginOptions = new LoginOptions();
                        LoginOptions.scope = "email, user_likes, publish_stream";
                        Facebook.login(delegate(LoginResponse response) 
                        {
                            Script.Literal("alert('We are logged in')");
                        }, LoginOptions);
                    }));
            };
        }
        public static void login()
        {
            Script.Literal("alert('You are here');");
        }

    }
}
