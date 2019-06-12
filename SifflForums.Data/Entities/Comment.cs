using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class Comment : EntityBase
    {
        public int CommentId { get; set;}
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
