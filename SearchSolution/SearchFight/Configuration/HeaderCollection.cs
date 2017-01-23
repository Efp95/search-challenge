using System.Configuration;

namespace SearchFight.Configuration
{
    public class HeaderCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return Attributes.Header;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            bool isName = false;
            if (!string.IsNullOrEmpty(elementName))
                isName = elementName.Equals(Attributes.Header);
            return isName;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new HeaderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HeaderElement)element).Key;
        }


        private struct Attributes
        {
            public const string Header = "header";
        }
    }
}
