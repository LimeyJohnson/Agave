using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
using System.Collections;

namespace FacebookScript
{
    public class Field
    {
        private string m_displayText;
        private string m_fieldName;
        private static string checkBoxPrefix = "fieldscb";
        public Field(string fieldName, string displayName)
        {
            this.m_displayText = displayName;
            this.m_fieldName = fieldName;
        }
        public virtual string ParseResult(Dictionary row)
        {
            return (string)row[FieldName] ?? "null";
        }
        public string DisplayText
        {
            get
            {
                return this.m_displayText;
            }
        }
        public string FieldName
        {
            get
            {
                return this.m_fieldName;
            }
        }
        public virtual string Html
        {
            get
            {
                string template = @"<input id='{0}' type='checkbox' checked='checked' />{1}<br />";
                return string.Format(template, ID, DisplayText);
            }
        }
        public virtual bool Checked
        {
            get
            {
                return jQuery.Select("#"+ID).Is(":checked");
            }
        }
        private string ID
        {
            get
            {
                return checkBoxPrefix + FieldName;
            }
        }
        
    }
}
