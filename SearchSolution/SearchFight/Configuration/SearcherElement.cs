using System.Configuration;

namespace SearchFight.Configuration
{
    public class SearcherElement : ConfigurationElement
    {
        [ConfigurationProperty(Attributes.Name, IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this[Attributes.Name];
            }
        }

        [ConfigurationProperty(Attributes.Type, IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this[Attributes.Type];
            }
        }

        [ConfigurationProperty(Attributes.Url, IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this[Attributes.Url];
            }
        }

        [ConfigurationProperty(Attributes.Headers)]
        public HeaderCollection HeaderCollection
        {
            get
            {
                return this[Attributes.Headers] as HeaderCollection;
            }
        }


        private struct Attributes
        {
            public const string Name = "name";
            public const string Type = "type";
            public const string Url = "url";
            public const string Headers = "headers";
        }
    }
}
