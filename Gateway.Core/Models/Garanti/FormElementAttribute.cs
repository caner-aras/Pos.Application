using System;

namespace Gateway.Core.Models.Garanti
{
    public class FormElementAttribute : Attribute
    {
        private string[] names;

        public FormElementAttribute(params string[] names)
        {
            this.names = names;
        }

        public string[] Names
        {
            get
            {
                return names;
            }
        }
    }
}