// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AgaveApi;

namespace DropboxScript
{

    public static class Dropbox
    {
        static Dropbox()
        {
            Office.Initialize = delegate(InitializationEnum initReason)
            {
                jQuery.Select("#test").Html("I Like Cheese");
            };
        }
    }
}
