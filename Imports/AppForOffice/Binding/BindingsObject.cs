// BindingObject.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AppForOffice
{

    [ScriptImport, ScriptIgnoreNamespace]
    public class BindingsObject
    {
        public void AddFromNamedItemAsync(string bindingID, BindingType bindingType, BindingOptions options) { }
        public void AddFromNamedItemAsync(string bindingID, BindingType bindingType, BindingOptions options, Action<ASyncResult> callback) { }
        public void AddFromSelectionAsync(BindingType bindingType, BindingOptions options) { }
        public void AddFromSelectionAsync(BindingType bindingType, BindingOptions options, Action<ASyncResult> callback) { }
        public void AddFromPromptAsync(BindingType bindingType, PromptBindingOptions options) { }
        public void AddFromPromptAsync(BindingType bindingType, PromptBindingOptions options, Action<ASyncResult> callback) { }
        public void GetByIdAsync(string id, Action<ASyncResult> callback) { }
    }
}
