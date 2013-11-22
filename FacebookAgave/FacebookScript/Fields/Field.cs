using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using FreindsLibrary;
using AppForOffice;
using System.Collections;
using System.Runtime.CompilerServices;

namespace FacebookScript
{
    public class Field
    {
        private string m_displayText;
        private string m_permission;
        private string m_fieldName;
        private string m_containerName;
        public bool m_checked = true;
        public static string checkBoxPrefix = "fieldscb";
        protected const string nullToken = "Unknown";

       
        public extern Field(string fieldName, string displayName, string containerName, string permission);
        public Field(string fieldName, string displayName, string containerName, string permission, bool? defaultChecked)
        {
            this.m_displayText = displayName;
            this.m_fieldName = fieldName;
            this.m_containerName = containerName;
            this.m_permission = permission;
            bool savedChecked;
            if (Script.Boolean(savedChecked = (bool)Office.Context.Document.Settings.Get(this.ID)))
            {
                this.m_checked = savedChecked;
            }
            else
            {
                m_checked = defaultChecked ?? false;
                Office.Context.Document.Settings.Set(this.ID, this.m_checked);
            }
        }
        public virtual string ParseResult(Dictionary row)
        {
            return (string)row[FieldName] ?? nullToken;
        }
        public string DisplayText
        {
            get
            {
                return this.m_displayText;
            }
        }
        public string Permission
        {
            get
            {
                return this.m_permission;
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
                string template = @"<input id='{0}' type='checkbox' " + ((m_checked) ? "checked='checked'" : "") + " />{1}";
                return string.Format(template, ID, DisplayText);
            }
        }
        public virtual bool Checked
        {
            get
            {
                return jQuery.Select("#" + ID).Is(":checked");
            }
            set
            {
                ((CheckBoxElement)jQuery.Select("#" + ID)[0]).Checked = value;
            }
        }
        public string ID
        {
            get
            {
                return checkBoxPrefix + FieldName;
            }
        }
        public void UpdateChecked(bool value)
        {
            Office.Context.Document.Settings.Set(this.ID, value);
        }

    }
}
