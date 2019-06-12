using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class Submission : EntityBase
    {
        public int SubmissionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
