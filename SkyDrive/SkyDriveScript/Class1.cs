// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AgaveApi;
namespace SkyDriveScript
{

    public static class SkyDrive
    {
        static SkyDrive()
        {
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                jQuery.Select("#test").Html("I Like Cheese");
            };
        }
    }
}
