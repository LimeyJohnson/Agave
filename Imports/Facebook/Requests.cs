// Class1.cs
//

using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Collections;

namespace FriendsRequests
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class LogEntry
    {
        public string Action, UserID, Environment, Error, Message;
    }
}
