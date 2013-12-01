using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class InitOptions
    {
        public InitOptions(params object[] nameValuePairs) { }
        public string client_id;
        public string redirect_uri;
        [ScriptField]
        public string[] Scope{get{return null;} set {}}
        public string response_type;
        public bool logging;

    }
    
}
