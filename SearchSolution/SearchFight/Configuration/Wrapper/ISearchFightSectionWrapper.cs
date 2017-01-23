using System.Collections.Generic;

namespace SearchFight.Configuration.Wrapper
{
    public interface ISearchFightSectionWrapper
    {
        IEnumerable<SearcherElementWrapper> Searchers { get; }
    }

    public class SearchFightSectionWrapper : ISearchFightSectionWrapper
    {
        public SearchFightSectionWrapper(SearchFightSection searchFightSection)
        {
            LoadConfiguration(searchFightSection);
        }

        public IEnumerable<SearcherElementWrapper> Searchers { get; private set; }


        private void LoadConfiguration(SearchFightSection searchFightSection)
        {
            var searchers = new List<SearcherElementWrapper>();
            foreach (SearcherElement searcherElement in searchFightSection.SearcherCollection)
            {
                var requestHeaders = new Dictionary<string, string>();
                foreach (HeaderElement header in searcherElement.HeaderCollection)
                {
                    requestHeaders.Add(header.Key, header.Value);
                }

                searchers.Add(new SearcherElementWrapper
                {
                    Name = searcherElement.Name,
                    Type = searcherElement.Type,
                    Url = searcherElement.Url,
                    RequestHeaders = requestHeaders
                });
            }

            Searchers = searchers;
        }
    }

    public class SearcherElementWrapper
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }

        public IReadOnlyDictionary<string, string> RequestHeaders { get; set; }
    }
}
