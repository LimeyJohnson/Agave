using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class LoginOptions
    {
        public LoginOptions(params object[] nameValuePairs) { }
        public extern LoginScope Scope { set; }
    }
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("")]
    public enum LoginScope
    {
        [ScriptName("wl.skydrive_update")]
        SkydriveUpdate,
    }
}
