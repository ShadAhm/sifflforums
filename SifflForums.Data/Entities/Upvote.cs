using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class Upvote
    {
        public int UpvoteId { get; set; }
        public int? SubmissionId { get; set; }
        public int? CommentId { get; set; }
        public int UserId { get; set; }
        public int Weight { get; set; }
        public Submission Submission { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
