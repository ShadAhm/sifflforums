using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Service.Models.Dto
{
    public class ForumSectionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
