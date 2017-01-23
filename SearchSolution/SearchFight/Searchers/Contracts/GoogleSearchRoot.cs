using System.Runtime.Serialization;

namespace SearchFight.Searchers.Contracts
{
    [DataContract]
    public class GoogleSearchRoot
    {
        [DataMember]
        public Queries queries { get; set; }
    }

    [DataContract]
    public class Queries
    {
        [DataMember]
        public Request[] request { get; set; }
    }

    [DataContract]
    public class Request
    {
        [DataMember]
        public long totalResults { get; set; }
    }
}
