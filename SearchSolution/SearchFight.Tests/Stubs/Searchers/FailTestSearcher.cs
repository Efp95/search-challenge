using SearchFight.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Tests.Stubs.Searchers
{
    public class FailTestSearcher : ISearcher
    {
        public string Name
        {
            get
            {
                return nameof(FailTestSearcher);
            }
        }


        public FailTestSearcher()
        {
        }
        public FailTestSearcher(string name, string url)
        {
        }
        public FailTestSearcher(string name, string url, Dictionary<string, string> headers)
        {
        }

        public async Task<SearcherResponse> Handle(string searchTerm)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }
    }
}
