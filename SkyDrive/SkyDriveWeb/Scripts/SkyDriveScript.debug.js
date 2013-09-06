//! SkyDriveScript.debug.js
//

(function($) {

Type.registerNamespace('SkyDriveScript');

////////////////////////////////////////////////////////////////////////////////
// SkyDriveScript.SkyDrive

SkyDriveScript.SkyDrive = function SkyDriveScript_SkyDrive() {
}


SkyDriveScript.SkyDrive.registerClass('SkyDriveScript.SkyDrive');
(function () {
    Office.initialize = function(initReason) {
        $('#test').html('I Like Cheese');
    };
})();
})(jQuery);

//! This script was generated using Script# v0.7.4.0
