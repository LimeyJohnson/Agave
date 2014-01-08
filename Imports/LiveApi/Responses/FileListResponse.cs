// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class FileListResponse:Response
    {
        [ScriptField, ScriptName("data")]
        public Folder[] Files { get { return null; } set { } }
    }
}
