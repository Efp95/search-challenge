using SearchFight.Core;
using SearchFight.Searchers.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace SearchFight.Searchers
{
    public class BingSearcher : BaseSearcher
    {
        public BingSearcher(string name, string url)
            : base(name, url)
        {
        }

        public BingSearcher(string name, string url, Dictionary<string, string> headers)
            : base(name, url, headers)
        {
        }


        public override async Task<SearcherResponse> Handle(string searchTerm)
        {
            long searchCount = default(long);
            string requestUrl = $"{base.Url}{searchTerm}";

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestUrl)
                };

                foreach (var header in base.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                using (var response = await httpClient.SendAsync(request))
                {
                    var serializer = new DataContractJsonSerializer(typeof(BingSearchRoot));
                    var streamContent = await response.Content.ReadAsStreamAsync();
                    var data = (BingSearchRoot)serializer.ReadObject(streamContent);

                    searchCount = data.webPages.totalEstimatedMatches;
                }
            }

            return new SearcherResponse { SearchTerm = searchTerm, SearcherName = base.Name, Total = searchCount };
        }
    }
}
