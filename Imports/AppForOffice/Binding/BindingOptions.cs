// BindingOptions.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{

    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class BindingOptions
    {
        public BindingOptions(params object[] nameValuePairs) { }
        [ScriptField]
        public string ID
        {
            get { return null; }
            set{ }
        }
        [ScriptField]
        public object AsyncContext
        {
            get { return null; }
            set { }
        }
    }
}
