// BindingOptions.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{

    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class PromptBindingOptions: BindingOptions
    {
        public PromptBindingOptions(params object[] nameValuePairs) { }
        [ScriptField]
        public string PromptText
        {
            get { return null; }
            set{ }
        }
    }
}
