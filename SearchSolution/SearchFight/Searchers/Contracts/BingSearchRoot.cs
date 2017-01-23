using System.Runtime.Serialization;

namespace SearchFight.Searchers.Contracts
{
    [DataContract]
    public class BingSearchRoot
    {
        [DataMember]
        public WebPages webPages { get; set; }
    }

    [DataContract]
    public class WebPages
    {
        [DataMember]
        public long totalEstimatedMatches { get; set; }
    }
}
