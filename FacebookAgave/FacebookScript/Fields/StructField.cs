﻿using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using System.Collections;
using System.Runtime.CompilerServices;


namespace FacebookScript
{
    public class StructField:Field
    {
        string DictField;
        string SubDictField;
        int? ArrayIndex;
        
        public extern StructField(string fieldName, string displayName, string dictField, string subDictField, string containerName, string permission);
        public StructField(string fieldName, string displayName, string dictField, string subDictField, string containerName, string permission, int? arrayIndex)
            : base(fieldName, displayName, containerName, permission)
        {
            this.DictField = dictField;
            this.SubDictField = subDictField;
            this.ArrayIndex = arrayIndex;
        }

        public override string ParseResult(System.Collections.Dictionary row)
        {
            string retVal= null;
            try
            {
                if (ArrayIndex == null)
                {
                    if (SubDictField == null)
                    {
                        retVal = (string)Script.Literal("{0}[{1}][{2}]", row, FieldName, DictField);
                    }
                    else
                    {
                        throw Exception.Create("Not Implemented", null);
                    }
                }
                else
                {
                    retVal = (string)Script.Literal("{0}[{1}][{2}][{3}][{4}]", row, FieldName, ArrayIndex, DictField, SubDictField);
                }
                
            }
            catch
            {
            }
            return retVal ?? nullToken;
        }
    }
}
