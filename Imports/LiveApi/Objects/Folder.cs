using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Html.Data;
using System.Collections.Generic;
namespace Live
{

   [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class Folder:Response
    {
       [ScriptField]
       public string ID { get { return null; } set { } }

       [ScriptField]
       public string Name { get { return null; } set { } }

       [ScriptField]
       public string Source { get { return null; } set { } }
       	
    }
    
}
