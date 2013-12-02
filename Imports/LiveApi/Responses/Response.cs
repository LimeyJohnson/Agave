using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class Response
    {
        [ScriptField]
        public string Status { get { return null; } set { } }
        [ScriptField, ScriptName("data")]
        public Folder FolderData { get { return null; } set { } }
    }
}
