using SearchFight.Core;
using SearchFight.Searchers.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace SearchFight.Searchers
{
    public class GoogleSearcher : BaseSearcher
    {
        public GoogleSearcher(string name, string url)
            : base(name, url)
        {
        }
        public GoogleSearcher(string name, string url, Dictionary<string, string> headers)
            : base(name, url, headers)
        {
        }

        public override async Task<SearcherResponse> Handle(string searchTerm)
        {
            long searchCount = default(long);
            string requestUrl = $"{base.Url}{searchTerm}";

            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetStreamAsync(requestUrl))
            {
                var serializer = new DataContractJsonSerializer(typeof(GoogleSearchRoot));
                var data = (GoogleSearchRoot)serializer.ReadObject(response);

                searchCount = data.queries.request[0].totalResults;
            }

            return new SearcherResponse { SearchTerm = searchTerm, SearcherName = base.Name, Total = searchCount };
        }
    }
}
