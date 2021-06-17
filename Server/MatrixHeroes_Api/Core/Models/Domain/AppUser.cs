using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MatrixHeroes_Api.Core.Models.Domain
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Hero> Heroes { get; set; }
    }
}