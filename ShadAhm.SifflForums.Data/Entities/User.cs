using System;
using System.Collections.Generic;
using System.Text;

namespace ShadAhm.SifflForums.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAtUtc { get; set; }
    }
}
