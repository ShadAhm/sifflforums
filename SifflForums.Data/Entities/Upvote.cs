using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class Upvote : EntityBase
    {
        public string VotingBoxId { get; set; }
        public string UserId { get; set; }
        public int Weight { get; set; }
        public VotingBox VotingBox { get; set; }
        public ApplicationUser User { get; set; }
    }
}
