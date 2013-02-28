﻿using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;
namespace FreindsLibrary
{

    [Imported]
    [IgnoreNamespace]
    [ScriptName("FB")]
    public static class Facebook
    {
        public delegate void ApiDelegate(ApiResponse response);
        public delegate void QueryDelgate(QueryResponse[] response);
        public delegate void LoginDelegate(LoginResponse response);
        public delegate void LogoutDelegate();
        public static void init(InitOptions options) { }
        public static void api(string apiCall, ApiDelegate response) { }
        public static void api(string apiCall, ApiOptions options, ApiDelegate response) { }
        public static void api(ApiOptions options, QueryDelgate response) { }
        public static void login(LoginDelegate d) { }
        public static void login(LoginDelegate d, LoginOptions options) { }
        public static void logout(LogoutDelegate d) { }
        public static void getLoginStatus(LoginDelegate response) { }
        [ScriptName("Event")]
        public static FBEvent Event;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class InitOptions
    {
        public string appId;
        public string channelUrl;
        public bool status;
        public bool cookie;
        public bool xfbml;
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
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class LoginOptions
    {
        public string scope;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class ApiOptions
    {
        public string message;
        public string method;
        public string Q;
        public Queries queries;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
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
        public FriendInfo[] data;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class QueryResponse
    {
        public MultiQueryResults[] fql_result_set;
    }
    [Imported, IgnoreNamespace, ScriptName("Object")]
    public sealed class MultiQueryResults
    {
        public string uid1;
        public string uid2;
        public string first_name;
        public string last_name;
        public string email;
        public string sex;
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
    }
    public delegate void EventChange(LoginResponse response);

}

