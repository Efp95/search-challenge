using SearchFight.Configuration.Wrapper;
using System;
using System.Collections.Generic;

namespace SearchFight.Core
{
    public interface ISearcherLoader
    {
        IEnumerable<ISearcher> Handle();
    }

    public class SearcherLoader : ISearcherLoader
    {
        private ISearchFightSectionWrapper SearchFightSectionWrapper { get; }

        public SearcherLoader(ISearchFightSectionWrapper searchFightSectionWrapper)
        {
            SearchFightSectionWrapper = searchFightSectionWrapper;
        }

        public IEnumerable<ISearcher> Handle()
        {
            var seacherList = new List<ISearcher>();

            ISearcher searcher = null;
            foreach (var searcherElement in SearchFightSectionWrapper.Searchers)
            {
                var searcherType = Type.GetType(searcherElement.Type);
                if (searcherType == null)
                    throw new InvalidOperationException($"[{searcherElement.Type}] type could not be loaded.");

                searcher = (ISearcher)Activator.CreateInstance(searcherType,
                                    searcherElement.Name, searcherElement.Url, searcherElement.RequestHeaders);

                seacherList.Add(searcher);
            }

            return seacherList;
        }
    }
}
