using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Service.Models.Dto
{
    public class SubmissionModel
    {
        public string SubmissionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CurrentUserVoteWeight { get; set; }
        public int Upvotes { get; set; }
        public int CommentsCount { get; set; }
        public int ForumSectionId { get; set; }
    }
}
