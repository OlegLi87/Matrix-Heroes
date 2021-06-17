using System.Collections.Generic;

namespace MatrixHeroes_Api.Core.Models.Domain
{
    public class Ability : BaseEntity
    {
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}