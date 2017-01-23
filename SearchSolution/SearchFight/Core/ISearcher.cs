using System.Threading.Tasks;

namespace SearchFight.Core
{
    public interface ISearcher
    {
        string Name { get; }
        Task<SearcherResponse> Handle(string searchTerm);
    }
}
