using SearchFight.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Searchers
{
    public abstract class BaseSearcher : ISearcher
    {
        public string Name { get; }
        public string Url { get; }
        public Dictionary<string, string> Headers { get; }

        public BaseSearcher(string name, string url)
        {
            Name = name;
            Url = url;
        }
        public BaseSearcher(string name, string url, Dictionary<string, string> headers)
            : this(name, url)
        {
            Headers = headers;
        }
        
        public abstract Task<SearcherResponse> Handle(string searchTerm);
    }
}
