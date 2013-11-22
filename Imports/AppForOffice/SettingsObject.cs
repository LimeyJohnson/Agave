// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{

    [ScriptImport, ScriptIgnoreNamespace]
    public class SettingsObject
    {
        public void Set(string name, object value) { }
        public void SaveAsync(Action<ASyncResult> callback) { }
        public object Get(string name) { return null; }
    }
}
