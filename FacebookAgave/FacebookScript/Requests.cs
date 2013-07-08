// Requests.cs
//

using System;
using System.Collections.Generic;
using jQueryApi;
using FriendsRequests;
namespace FacebookScript
{
    public static class Requests
    {
#if DEBUG
        public static string Environment = "Test";
        public static string URL = "http://localhost:62587/";
#else
        public static string Environment = "Production";
        public static string URL = "https://friendsinoffice.com/";
#endif
        public static void LogAction(string Action, string UserID, string ErrorText, string Message)
        {
            LogEntry actionLog = new LogEntry();
            actionLog.Action = Action;
            actionLog.UserID = UserID;
            actionLog.Environment = Environment;
            actionLog.Error = ErrorText;
            actionLog.Message = Message;
            jQuery.Get(URL + "Friends.svc/LogAction", actionLog, delegate(object o)
            {

            }).Error(delegate(jQueryXmlHttpRequest request, string message, Exception e)
            {
                string strings = message;
            });
        }
    }
}
