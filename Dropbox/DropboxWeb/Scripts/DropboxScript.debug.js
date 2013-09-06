//! DropboxScript.debug.js
//

(function($) {

Type.registerNamespace('DropboxScript');

////////////////////////////////////////////////////////////////////////////////
// DropboxScript.Dropbox

DropboxScript.Dropbox = function DropboxScript_Dropbox() {
}


DropboxScript.Dropbox.registerClass('DropboxScript.Dropbox');
(function () {
    Office.initialize = function(initReason) {
        $('#test').html('I Like Cheese');
    };
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
