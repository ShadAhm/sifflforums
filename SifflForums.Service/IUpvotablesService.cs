using SifflForums.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Service
{
    public interface IUpvotablesService
    {
        IUpvotable ResolveUpvotableEntity(int entityId); 
    }
}
