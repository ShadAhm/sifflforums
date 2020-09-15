using System;
using System.Collections.Generic;
using System.Text;

namespace SifflForums.Data.Interfaces
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; } 
    }
}
