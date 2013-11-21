using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Html.Data;
using System.Collections.Generic;
namespace Live
{

    #region Classes
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("WL")]
    public static class LiveApi
    {
        public extern static PromiseObject Login(params object[] nameValuePairs);
        public extern static PromiseObject Login(LoginOptions options);
        public extern static PromiseObject Api(ApiOptions options);
        public extern static PromiseObject Init(InitOptions options);
        public extern static PromiseObject Ui(UiOptions options);
        public extern static PromiseObject FileDialog(FileDialogOptions options);
        [ScriptName(PreserveCase=true)]
        public static EventObject Event;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class UiOptions
    {
        public string name;
        public string element;
        public string brand;
        public Action<LoginResponse> onloggedin;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class FileDialogOptions
    {
        public string mode;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class InitOptions
    {

        public string client_id;
        public string redirect_uri;
        public string scope;
        public string response_type;
        public bool logging;

    }
    [ScriptImport, ScriptIgnoreNamespace]
    public class EventObject
    {
        public extern void subscribe(string authType, Action<LoginResponse> callback);
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class LoginOptions
    {
        public extern LoginScope Scope { set; }
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class PromiseObject
    {
        public extern void Then(Action<LoginResponse> action);
        public extern void Then(Action<LoginResponse> success, Action<LoginResponse> failure);
        public extern void Then(Action<Dictionary<string, string>> action);
        public extern void Then(Action action);

    }

    public class LoginResponse
    {
        public string status;
    }


    #endregion
    #region Enums
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("")]
    public enum LoginScope
    {
        [ScriptName("wl.skydrive_update")]
        SkydriveUpdate,
    }

    #endregion
}
