using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace Live
{
    [ScriptImport, ScriptIgnoreNamespace, ScriptName("Object")]
    public class CreateFolderOptions
    {
        public CreateFolderOptions(params object[] nameValuePairs) { }
        
        [ScriptField]
        public string Name
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
        public string Description
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
