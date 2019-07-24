using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class Upvote
    {
        public int UpvoteId { get; set; }
        public int VotingBoxId { get; set; }
        public int UserId { get; set; }
        public int Weight { get; set; }
        public VotingBox VotingBox { get; set; }
        public User User { get; set; }
    }
}
