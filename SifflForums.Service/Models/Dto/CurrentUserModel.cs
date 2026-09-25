using System.Collections.Generic;

namespace SifflForums.Service.Models.Dto
{
    public class CurrentUserModel
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
