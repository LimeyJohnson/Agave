using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using System.Collections;

namespace FacebookScript
{
    public class RequiredField: Field
    {
        public RequiredField(string fieldName, string displayText) : base(fieldName, displayText, null,null) { }
        public override bool Checked
        {
            get
            {
                return true;
            }
            set
            {
                
            }
        }
        public override string Html
        {
            get
            {
                return null;
            }
        }
    }
}
