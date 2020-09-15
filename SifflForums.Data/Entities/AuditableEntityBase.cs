using SifflForums.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SifflForums.Data.Entities
{
    public abstract class AuditableEntityBase : EntityBase, IAuditableEntity
    {
        public DateTime CreatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public string ModifiedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser Creator { get; set; }
        [ForeignKey("ModifiedBy")]
        public ApplicationUser Modifier { get; set; }
    }
}
