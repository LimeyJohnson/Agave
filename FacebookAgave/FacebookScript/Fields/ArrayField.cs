using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
using System.Collections;


namespace FacebookScript
{
    public class ArrayField:Field
    {
        string DictField;
        string SubDictField;
        public ArrayField(string fieldName, string displayName, string dictField, string subDictField)
            : base(fieldName, displayName)
        {
            this.DictField = dictField;
            this.SubDictField = subDictField;
        }

        public override string ParseResult(System.Collections.Dictionary row)
        {
            string retVal= null;
            try
            {
                retVal = (string)Script.Literal("{0}[{1}][{2}][{3}][{4}]", row,FieldName, 0, DictField, SubDictField);
            }
            catch
            {
            }
            return retVal ?? "null";
        }
    }
}
