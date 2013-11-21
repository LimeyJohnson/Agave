using System;
using System.Html;
using System.Runtime.CompilerServices;
using System.Html.Data;
using System.Collections.Generic;
namespace Live
{

    #region Classes
    public class PromiseGeneric<T>
    {
        public extern void Then(Action<T> action);
        public extern void Then(Action<T> success, Action<T> failure);
        public extern void Then(Action action);
    }
    public class Promise : PromiseGeneric<object>
    {
    }
    #endregion
    
}
