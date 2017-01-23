using System.Collections.Generic;

namespace SearchFight.Core
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public IEnumerable<SearcherResponse> SearcherResults { get; set; }
    }
}
