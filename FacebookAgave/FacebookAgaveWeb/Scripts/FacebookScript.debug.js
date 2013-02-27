//! FacebookScript.debug.js
//

(function($) {

Type.registerNamespace('FacebookScript');

////////////////////////////////////////////////////////////////////////////////
// FacebookScript.FacebookScript

FacebookScript.FacebookScript = function FacebookScript_FacebookScript() {
}
FacebookScript.FacebookScript.login = function FacebookScript_FacebookScript$login() {
    alert('You are here');;
}


FacebookScript.FacebookScript.registerClass('FacebookScript.FacebookScript');
Office.initialize = function(initReason) {
    var options = {};
    options.channelUrl = 'http://facebookagave.azurewebsites.net/pages/channel.ashx';
    options.appId = '263395420459543';
    options.status = true;
    options.cookie = false;
    FB.init(options);
    $('#FBLogin').click(function(eventArgs) {
        var LoginOptions = {};
        LoginOptions.scope = 'email, user_likes, publish_stream';
        FB.login(function(response) {
            alert('We are logged in');
        }, LoginOptions);
    });
};
})(jQuery);

//! This script was generated using Script# v0.7.6.0
