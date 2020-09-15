using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Interfaces
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public string ModifiedBy { get; set; }
    }
}
