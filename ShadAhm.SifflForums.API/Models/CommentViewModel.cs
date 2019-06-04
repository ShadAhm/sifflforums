using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadAhm.SifflForums.Api.Models
{
    public class CommentViewModel
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }
    }
}
