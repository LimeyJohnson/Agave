// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Html.Data;
using System.Collections.Generic;
namespace Live
{

    #region Classes
    [Imported, IgnoreNamespace, ScriptName("WL")]
    public static class LiveApi
    {
        public extern static PromiseObject Login(Dictionary<string, string> args);
        public extern static PromiseObject Login(LoginOptions options);
        public extern static PromiseObject Api(Dictionary<string, string> args);
        public extern static PromiseObject Init(InitOptions options);
        public extern static PromiseObject Ui(UiOptions options);
        public extern static PromiseObject FileDialog(FileDialogOptions options);
        [PreserveCase]
        public static EventObject Event;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class UiOptions
    {
        public string name;
        public string element;
        public string brand;
        public Action<LoginResponse> onloggedin;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class FileDialogOptions
    {
        public string mode;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class InitOptions
    {

        public string client_id;
        public string redirect_uri;
        public string scope;
        public string response_type;
        public bool logging;

    }
    [Imported, IgnoreNamespace]
    public class EventObject
    {
        public extern void subscribe(string authType, Action<LoginResponse> callback);
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class LoginOptions
    {
        public extern LoginScope Scope { set; }
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class PromiseObject
    {
        public extern void Then(Action<LoginResponse> action);
        public extern void Then(Action<LoginResponse> success, Action<LoginResponse> failure);
        public extern void Then(Action<Dictionary<string, string>> action);
        public extern void Then(Action action);

    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class LoginResponse
    {
        public string status;
    }

    [Imported, IgnoreNamespace, ScriptName("Object")]
    public class ApiOptions
    {
        public string Path;
        public string Method;
    }



    #endregion
    #region Enums
    [Imported, IgnoreNamespace, ScriptName("")]
    public enum LoginScope
    {
        [PreserveCase, ScriptName("wl.skydrive_update")]
        SkydriveUpdate,
    }

    #endregion
}
