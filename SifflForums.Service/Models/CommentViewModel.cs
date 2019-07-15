using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Models
{
    public class CommentViewModel
    {
        public int SubmissionId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public string CreatedAtUtc { get; set; }
    }
}
