using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class ApiOptions
    {
        public ApiOptions(params object[] nameValuePairs) { }
        
        [ScriptField]
        public string Path
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
        [ScriptField]
        public string Method
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}
