using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SifflForums.Service.Models.Dto
{
    public class ForumSectionModel
    {
        public string Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
