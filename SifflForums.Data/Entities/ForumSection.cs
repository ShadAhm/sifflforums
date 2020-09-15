using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Entities
{
    public class ForumSection : AuditableEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
