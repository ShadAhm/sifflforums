using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Service.Models.Dto
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int SubmissionId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public int VotingBoxId { get; set; }
        public int CurrentUserVoteWeight { get; set; }
        public int Upvotes { get; set; }
        public string CreatedAtUtc { get; set; }
    }
}
