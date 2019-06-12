using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Api.Models
{
    public class SubmissionViewModel
    {
        public int SubmissionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

    }
}
