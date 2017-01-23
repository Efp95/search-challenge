using System.Configuration;

namespace SearchFight.Configuration
{
    public class SearchFightSection : ConfigurationSection
    {
        private static SearchFightSection _configuration = ConfigurationManager.GetSection("searchFight") as SearchFightSection;

        public static SearchFightSection Configuration
        {
            get
            {
                return _configuration;
            }
        }

        [ConfigurationProperty(Attributes.Searchers)]
        public SearcherCollection SearcherCollection
        {
            get
            {
                return this[Attributes.Searchers] as SearcherCollection;
            }
        }


        private struct Attributes
        {
            public const string Searchers = "searchers";
        }
    }
}
