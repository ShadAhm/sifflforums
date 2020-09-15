using System.Collections.Generic;

namespace SifflForums.Data.Entities
{
    public class VotingBox : EntityBase
    {
        public List<Upvote> Upvotes { get; set; }
    }
}
