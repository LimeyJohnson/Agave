using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class FileDialogOptions
    {
        public FileDialogOptions(params object[] nameValuePairs) { }
        [ScriptField]
        public string Mode
        {
            get
            {
                return null;
            }
            set { }
        }
    }
}
