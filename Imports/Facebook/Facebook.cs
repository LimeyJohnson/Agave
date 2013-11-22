using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
namespace FreindsLibrary
{
    public delegate void AsyncInitDelegate();
    [ScriptImport, ScriptIgnoreNamespace,ScriptName("FB")]
    public static class Facebook
    {
        public delegate void ApiDelegate(ApiResponse response);
        public delegate void QueryDelgate(QueryResponse[] response);
        //public delegate void LoginDelegate(LoginResponse response);
        public delegate void UIDelegate(UIResponse response);
        
        
        public static void init(InitOptions options) { }
        public static void api(string apiCall, ApiDelegate response) { }
        public static void api(string apiCall, ApiOptions options, ApiDelegate response) { }
        public static void api(string apiCall, string noun, ApiOptions options, ApiDelegate response) { }
        public static void api(ApiOptions options, QueryDelgate response) { }
        public static void login(Action<LoginResponse> d) { }
        public static void login(Action<LoginResponse> d, LoginOptions options) { }
        public static void logout(Action<LoginResponse> d) { }
        public static void getLoginStatus(Action<LoginResponse> response) { }
        public static void ui(UIOptions options, UIDelegate response) { }
        [ScriptName("Event")]
        public static FBEvent Event;
      
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("window")]
    public static class FacebookWindow
    {
        [ScriptField, ScriptName("fbAsyncInit")]
        public static extern AsyncInitDelegate AsyncInit { get; set; }
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class InitOptions
    {
        public string appId;
        public string channelUrl;
        public bool status;
        public bool cookie;
        public bool xfbml;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class UIOptions
    {
        public string Method;
        public string Display;
        public string Redirect_uri;
        public string Link;
        public string Picture;
        public string Name;
        public string Caption;
        public string Discription;
        public string From;
        public string To;
        [ScriptName("to")]
        public Array ToArray;

    }
    public sealed class UIResponse
    {
        public string Post_id;
    }
    public sealed class LoginResponse
    {
        public AuthResponse authResponse;
        public string status;

    }
    public class AuthResponse
    {
        public string userID;
        public string accessToken;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class LoginOptions
    {
        public string scope;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class ApiOptions
    {
        public string message;
        public string method;
        public string Q;
        public Queries queries;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class Queries
    {
        public string friendsAll;
        public string friendsLimit;
        public string friendsoffriends;
    }
    public sealed class ApiResponse
    {
        public string name;
        public string id;
        public string error;
        public Dictionary[] data;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class QueryResponse
    {
        public MultiQueryResults[] fql_result_set;
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public sealed class MultiQueryResults
    {
        public string uid1;
        public string uid2;
        
    }
    public class FBEvent
    {
        public void subscribe(string eventName, EventChange response) { }
    }
    public sealed class FriendInfo
    {
        public string id;
        public string first_name;
        public string last_name;
        public string email;
        public string sex;
        public string birthday_date;
        public string friend_count;
    }
    public delegate void EventChange(LoginResponse response);

}

