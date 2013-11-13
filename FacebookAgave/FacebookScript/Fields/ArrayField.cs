using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
using System.Collections;
using System.Runtime.CompilerServices;


namespace FacebookScript
{
    public class ArrayField:Field
    {
        public string SubField;
        public ArrayField(string fieldName, string displayText, string subField, string containerName, string permission, string sample): base(fieldName, displayText, containerName, permission, sample)
        {
            this.SubField = subField;
        }

        public override string ParseResult(System.Collections.Dictionary row)
        {
            string join= null;
            try
            {
                Array a = (Array)row[FieldName];
                Array b = new Array();
                for (int x = 0; x < a.Length; x++)
                {
                    b[b.Length] = ((Dictionary<string, string>)a[x])[SubField];
                }
                join = b.Join(",");
            }
            catch { }
            return join ?? nullToken;
        }
    }
}
