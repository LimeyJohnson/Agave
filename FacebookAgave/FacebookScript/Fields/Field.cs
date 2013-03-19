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
    public class Field
    {
        private string m_displayText;
        private string m_fieldName;
        private string m_containerName;
        private bool m_defaultOff;
        private static string checkBoxPrefix = "fieldscb";
        [AlternateSignature]
        public extern Field(string fieldName, string displayName, string containerName);
        public Field(string fieldName, string displayName, string containerName, bool defaultOff)
        {
            this.m_displayText = displayName;
            this.m_fieldName = fieldName;
            this.m_containerName = containerName;
            m_defaultOff = defaultOff;
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
        public string ContainerName
        {
            get
            {
                return m_containerName;
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
                string template = @"<input id='{0}' type='checkbox' "+((m_defaultOff!=null && m_defaultOff == true)? "": "checked='checked'")+" />{1}";
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
