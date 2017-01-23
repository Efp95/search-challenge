using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Core
{
    public class SearchProcess
    {
        private IEnumerable<ISearcher> SearcherList { get; }
        private IWinnerSearchCalculator WinnerSearchCalculator { get; }

        public SearchProcess(ISearcherLoader searcherLoader, IWinnerSearchCalculator winnerSearchCalculator)
        {
            SearcherList = searcherLoader.Handle();
            WinnerSearchCalculator = winnerSearchCalculator;
        }


        public async Task<TotalSearchResult> Run(IEnumerable<string> searchTerms)
        {
            var searchResults = await ExecuteSearch(searchTerms);

            var result = WinnerSearchCalculator.Handle(searchResults);

            return result;
        }

        private async Task<IEnumerable<SearchResult>> ExecuteSearch(IEnumerable<string> searchTerms)
        {
            var searchs = new List<Task<SearchResult>>();
            foreach (var searchTerm in searchTerms)
            {
                searchs.Add(DoSingleSearch(searchTerm));
            }

            var searchResults = await Task.WhenAll(searchs);

            return searchResults;
        }

        private async Task<SearchResult> DoSingleSearch(string searchTerm)
        {
            var searchs = new List<Task<SearcherResponse>>();
            foreach (var searcher in SearcherList)
            {
                searchs.Add(searcher.Handle(searchTerm));
            }

            var searchResults = await Task.WhenAll(searchs);

            return new SearchResult { SearchTerm = searchTerm, SearcherResults = searchResults };
        }
    }
}
