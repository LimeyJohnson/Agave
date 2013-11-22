// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{
    [ScriptImport, ScriptIgnoreNamespace]
    public sealed class Error
    {
        public string Name;
        public string Message;
        public int Code;
    }
}
