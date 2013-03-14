using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AgaveApi;
using System.Collections;

namespace FacebookScript
{
    public class FacebookField
    {
        private string m_displayText;
        private string m_fieldName;
        private static string checkBoxPrefix = "fieldscb";
        public FacebookField(string fieldName, string displayName)
        {
            this.m_displayText = displayName;
            this.m_fieldName = fieldName;
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
        public CheckBoxElement CheckBox
        {
            get
            {
                CheckBoxElement cb = (CheckBoxElement)Document.CreateElement("input");
                cb.Type = "checkbox";
                cb.ID = ID;
                cb.Checked = true;
                return cb;
            }
        }
        public bool Checked
        {
            get
            {
                return jQuery.Select(ID).Is(":checked");
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
