using Games.Core.Entities.Games;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Games.Core.Entities.Categories
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public Category(long id, string name) : base()
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
