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
        public extern static PromiseGeneric<LoginResponse> Login(params object[] nameValuePairs);
        public extern static PromiseGeneric<LoginResponse> Login(LoginOptions options);
        public extern static PromiseGeneric<ApiResponse> Api(ApiOptions options);
        public extern static PromiseGeneric<LoginResponse> Init(InitOptions options);
        public extern static Promise Ui(UiOptions options);
        public extern static Promise FileDialog(FileDialogOptions options);
        [ScriptName(PreserveCase=true)]
        public static EventObject Event;
    }
    [ScriptImport, ScriptIgnoreNamespace]
    public class EventObject
    {
        public extern void subscribe(string authType, Action<LoginResponse> callback);
    }
    #endregion
   
}
