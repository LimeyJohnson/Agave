// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using AgaveApi;
namespace TestScript
{

    public class BaseClass
    {
       public BaseClass()
       {
            Office.Initialize = delegate(InitializationEnum initReason)
            {

            };
       }
    }
}
