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
    /// <field name="friendID" type="String" static="true">
    /// </field>
}
FacebookScript.FacebookScript.logOutOfFacebook = function FacebookScript_FacebookScript$logOutOfFacebook(eventArgs) {
    /// <param name="eventArgs" type="jQueryEvent">
    /// </param>
    FB.logout(function() {
        FacebookScript.FacebookScript.userID = null;
        FacebookScript.FacebookScript.accessToken = null;
    });
}
FacebookScript.FacebookScript.insertFriends = function FacebookScript_FacebookScript$insertFriends(eventArgs) {
    /// <param name="eventArgs" type="jQueryEvent">
    /// </param>
    var dict = {};
    dict['uid'] = 'ID';
    var comboBoxes = $('#FieldChoices input:checked');
    comboBoxes.each(function(i, e) {
        dict[e.getAttribute('field')] = e.getAttribute('display');
    });
    var td = new Office.TableData();
    td.headers = new Array(1);
    td.headers[0] = [];
    var $dict1 = dict;
    for (var $key2 in $dict1) {
        var entry = { key: $key2, value: $dict1[$key2] };
        td.headers[0][td.headers[0].length] = entry.value;
    }
    var query = 'SELECT ' + Object.keys(dict).join(',') + ' FROM user WHERE uid IN (SELECT uid2 from friend WHERE uid1 = me())';
    var queryOptions = {};
    queryOptions.q = query;
    FB.api('fql', queryOptions, function(response) {
        td.rows = new Array(response.data.length);
        for (var i = 0; i < response.data.length; i++) {
            td.rows[i] = new Array(td.headers[0].length);
            for (var y = 0; y < td.headers[0].length; y++) {
                td.rows[i][y] = response.data[i][Object.keys(dict)[y]] || 'null';
            }
        }
        (document.getElementById('profilepic')).src = 'http://graph.facebook.com/' + td.rows[0][0] + '/picture';
        var options = {};
        options.coercionType = Office.CoercionType.Table;
        Office.context.document.setSelectedDataAsync(td, options, function(result) {
            if (result.status === Office.AsyncResultStatus.Failed) {
                write(result.error.name + ' : '+result.error.message);
            }
            if (result.status === Office.AsyncResultStatus.Succeeded) {
                var bindingOptions = {};
                bindingOptions.id = FacebookScript.FacebookScript.tableBinding;
                $('#friend').show();
                $('#insert').hide();
                Office.context.document.bindings.addFromSelectionAsync(Office.BindingType.Table, bindingOptions, function(bindingResult) {
                    Office.select('bindings#' + FacebookScript.FacebookScript.tableBinding).addHandlerAsync(Office.EventType.BindingSelectionChanged, FacebookScript.FacebookScript.handleTableSelection);
                });
            }
        });
    });
}
FacebookScript.FacebookScript.handleTableSelection = function FacebookScript_FacebookScript$handleTableSelection(args) {
    /// <param name="args" type="Object">
    /// </param>
    var options = {};
    options.startRow = args.startRow;
    options.startColumn = 0;
    options.rowCount = 1;
    options.columnCount = 1;
    options.coercionType = Office.CoercionType.Table;
    Office.select('bindings#' + FacebookScript.FacebookScript.tableBinding).getDataAsync(options, function(result) {
        if (result.status === Office.AsyncResultStatus.Succeeded) {
            FacebookScript.FacebookScript.friendID = result.value.rows[0][0];
            (document.getElementById('profilepic')).src = 'http://graph.facebook.com/' + FacebookScript.FacebookScript.friendID + '/picture';
        }
    });
}
FacebookScript.FacebookScript.handleSelectAllCheckBox = function FacebookScript_FacebookScript$handleSelectAllCheckBox(eventArgs) {
    /// <param name="eventArgs" type="jQueryEvent">
    /// </param>
    var convertToChecked = $('#ckbSelectAll').is(':checked');
    $('#FieldChoices input').each(function(i, e) {
        (e).checked = convertToChecked;
    });
}
FacebookScript.FacebookScript.postFriendStatus = function FacebookScript_FacebookScript$postFriendStatus(eventArgs) {
    /// <param name="eventArgs" type="jQueryEvent">
    /// </param>
    if (FacebookScript.FacebookScript.friendID != null && !!FacebookScript.FacebookScript.friendID) {
        var options = {};
        options.to = FacebookScript.FacebookScript.friendID;
        options.from = FacebookScript.FacebookScript.userID;
        options.method = 'feed';
        options.display = 'page';
        FB.ui(options, function(response) {
            document.write(response.post_id);;
        });
    }
}
FacebookScript.FacebookScript.postToAllFreinds = function FacebookScript_FacebookScript$postToAllFreinds(eventArgs) {
    /// <param name="eventArgs" type="jQueryEvent">
    /// </param>
    var options = {};
    options.filterType = Office.FilterType.OnlyVisible;
    options.startColumn = 0;
    options.columnCount = 1;
    options.coercionType = Office.CoercionType.Table;
    Office.select('bindings#' + FacebookScript.FacebookScript.tableBinding).getDataAsync(options, function(result) {
        var friendsArray = [];
        for (var x = 0; x < result.value.rows.length; x++) {
            friendsArray[friendsArray.length] = result.value.rows[x][0];
        }
        var uiOptions = {};
        uiOptions.display = 'popup';
        uiOptions.method = 'send';
        uiOptions.to = friendsArray.join(',');
        uiOptions.from = FacebookScript.FacebookScript.userID;
        uiOptions.link = '';
        FB.ui(uiOptions, function(UIResp) {
            document.write(UIResp.post_id);;
        });
    });
}


FacebookScript.FacebookScript.registerClass('FacebookScript.FacebookScript');
FacebookScript.FacebookScript.userID = null;
FacebookScript.FacebookScript.accessToken = null;
FacebookScript.FacebookScript.tableBinding = 'TableBinding';
FacebookScript.FacebookScript.friendID = null;
(function () {
    window.fbAsyncInit = function() {
        var options = {};
        options.channelUrl = 'http://facebookagave.azurewebsites.net/pages/channel.ashx';
        options.appId = '263395420459543';
        options.status = true;
        options.cookie = false;
        FB.init(options);
        $('#GetFriends').click(FacebookScript.FacebookScript.insertFriends);
        $('#LogOut').click(FacebookScript.FacebookScript.logOutOfFacebook);
        $('#SelectAll').click(FacebookScript.FacebookScript.handleSelectAllCheckBox);
        $('#postfriendstatus').click(FacebookScript.FacebookScript.postFriendStatus);
        $('#posttoallfriends').click(FacebookScript.FacebookScript.postToAllFreinds);
        FB.getLoginStatus(function(loginResponse) {
            if (loginResponse.status === 'connected') {
                FacebookScript.FacebookScript.userID = loginResponse.authResponse.userID;
                FacebookScript.FacebookScript.accessToken = loginResponse.authResponse.accessToken;
            }
            else {
                var LoginOptions = {};
                LoginOptions.scope = 'email,publish_actions,create_event,user_likes,publish_stream,user_about_me,friends_about_me,user_activities,friends_activities,user_birthday,friends_birthday,user_checkins,friends_checkins,user_education_history,friends_education_history,user_events,friends_events,user_groups,friends_groups,user_hometown,friends_hometown,user_interests,friends_interests,user_location,friends_location,user_notes,friends_notes,user_photos,friends_photos,user_questions,friends_questions,user_relationships,friends_relationships,user_relationship_details,friends_relationship_details,user_religion_politics,friends_religion_politics,user_status,friends_status,user_subscriptions,friends_subscriptions,user_videos,friends_videos,user_website,user_work_history,friends_work_history';
                FB.login(function(response) {
                    FacebookScript.FacebookScript.userID = response.authResponse.userID;
                }, LoginOptions);
            }
        });
    };
    Office.initialize = function(initReason) {
    };
    var reference = document.getElementsByTagName('script')[0];
    var JSID = 'facebook-jssdk';
    if (reference.id !== JSID) {
        var js = document.createElement('script');
        js.id = JSID;
        js.setAttribute('async', true);
        js.src = '//connect.facebook.net/en_US/all.js';
        reference.parentNode.insertBefore(js, reference);
    }
})();
})(jQuery);

//! This script was generated using Script# v0.7.6.0
