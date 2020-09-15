using SifflForums.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SifflForums.Data.Entities
{
    public abstract class EntityBase : IEntity<string>
    {
        public string Id { get; set; }
    }

    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
