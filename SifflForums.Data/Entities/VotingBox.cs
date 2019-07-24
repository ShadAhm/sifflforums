using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class VotingBox
    {
        public int VotingBoxId { get; set; }
        public List<Upvote> Upvotes { get; set; }
    }
}
