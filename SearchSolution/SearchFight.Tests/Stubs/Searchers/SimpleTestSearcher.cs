using SearchFight.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Tests.Stubs.Searchers
{
    public class SimpleTestSearcher : ISearcher
    {
        public string Name
        {
            get
            {
                return nameof(SimpleTestSearcher);
            }
        }

        public SimpleTestSearcher()
        {
        }
        public SimpleTestSearcher(string name, string url)
        {
        }
        public SimpleTestSearcher(string name, string url, Dictionary<string, string> headers)
        {
        }

        public async Task<SearcherResponse> Handle(string searchTerm)
        {
            await Task.Delay(1);

            return new SearcherResponse { SearchTerm = searchTerm, SearcherName = Name, Total = 15500 };
        }
    }
}
