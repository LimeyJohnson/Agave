//! FacebookScript.debug.js
//

(function($) {

Type.registerNamespace('FacebookScript');

////////////////////////////////////////////////////////////////////////////////
// FacebookScript.FacebookScript

FacebookScript.FacebookScript = function FacebookScript_FacebookScript() {
    /// <field name="userID" type="String" static="true">
    /// </field>
    /// <field name="accessToken" type="String" static="true">
    /// </field>
    /// <field name="tableBinding" type="String" static="true">
    /// </field>
}
FacebookScript.FacebookScript.checkTable = function FacebookScript_FacebookScript$checkTable() {
    var myTable = new Office.TableData();
    myTable.headers = [ [ 'Cities', 'Names' ] ];
    myTable.rows = [ [ 'Berlin', 'Andrew' ], [ 'Roma', 'Eric' ], [ 'Tokyo', 'Johnson' ], [ 'Seattle', 'People' ] ];
    var options = {};
    options.coercionType = Office.CoercionType.Table;
    Office.context.document.setSelectedDataAsync(myTable, options, function(result) {
        var namedOptions = {};
        namedOptions.id = FacebookScript.FacebookScript.tableBinding;
        Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, namedOptions, function(results) {
            Office.select('bindings#' + FacebookScript.FacebookScript.tableBinding);
        });
    });
}
FacebookScript.FacebookScript.insertFriends = function FacebookScript_FacebookScript$insertFriends(eventArgs) {
    /// <param name="eventArgs" type="jQueryEvent">
    /// </param>
    var query = 'Select first_name, last_name, birthday_date, sex, friend_count from user WHERE uid IN (SELECT uid2 from friend WHERE uid1 = me())';
    var queryOptions = {};
    queryOptions.q = query;
    var td = new Office.TableData();
    td.headers = [ [ 'First Name', 'Last Name', 'Birthday', 'Gender', 'Friend Count' ] ];
    FB.api('fql', queryOptions, function(response) {
        td.rows = new Array(response.data.length);
        for (var i = 0; i < response.data.length; i++) {
            td.rows[i] = new Array(5);
            td.rows[i][0] = response.data[i].first_name || 'null';
            td.rows[i][1] = response.data[i].last_name || 'null';
            td.rows[i][2] = response.data[i].birthday_date || 'null';
            td.rows[i][3] = response.data[i].sex || 'null';
            td.rows[i][4] = response.data[i].friend_count || 'null';
        }
        var options = {};
        options.coercionType = Office.CoercionType.Table;
        Office.context.document.setSelectedDataAsync(td, options, function(result) {
            if (result.status === Office.AsyncResultStatus.Failed) {
                write(result.error.name + ' : '+result.error.message);
            }
        });
    });
}


FacebookScript.FacebookScript.registerClass('FacebookScript.FacebookScript');
FacebookScript.FacebookScript.userID = null;
FacebookScript.FacebookScript.accessToken = null;
FacebookScript.FacebookScript.tableBinding = 'TableBinding';
Office.initialize = function(initReason) {
    var options = {};
    options.channelUrl = 'http://facebookagave.azurewebsites.net/pages/channel.ashx';
    options.appId = '263395420459543';
    options.status = true;
    options.cookie = false;
    FB.init(options);
    $('#GetFriends').click(FacebookScript.FacebookScript.insertFriends);
    $('#FBLogin').click(function(eventArgs) {
        var LoginOptions = {};
        LoginOptions.scope = 'email,create_event,user_likes,publish_stream,user_about_me,friends_about_me,user_activities,friends_activities,user_birthday,friends_birthday,user_checkins,friends_checkins,user_education_history,friends_education_history,user_events,friends_events,user_groups,friends_groups,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_notes,friends_notes,user_photos,friends_photos,user_questions,friends_questions,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_subscriptions,friends_subscriptions,user_videos,friends_videos,user_website,user_work_history,friends_work_history';
        FB.login(function(response) {
            FacebookScript.FacebookScript.userID = response.authResponse.userID;
        }, LoginOptions);
    });
    FB.getLoginStatus(function(loginResponse) {
        if (loginResponse.status === 'connected') {
            FacebookScript.FacebookScript.userID = loginResponse.authResponse.userID;
            FacebookScript.FacebookScript.accessToken = loginResponse.authResponse.accessToken;
        }
    });
};
Office.initialize(Office.InitializationReason.DocumentOpenend);
})(jQuery);

//! This script was generated using Script# v0.7.6.0
