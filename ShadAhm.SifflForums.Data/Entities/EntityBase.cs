using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShadAhm.SifflForums.Data.Entities
{
    public abstract class EntityBase
    {
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public int ModifiedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
        [ForeignKey("ModifiedBy")]
        public User Modifier { get; set; }
    }
}
