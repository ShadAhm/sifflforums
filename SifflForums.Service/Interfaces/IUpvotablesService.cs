using SifflForums.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Service
{
    public interface IUpvotablesService
    {
        IUpvotableEntity ResolveUpvotableEntity(string entityId); 
    }
}
