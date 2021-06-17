using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatrixHeroes_Api.Core.Models.Dtos
{
    public class HeroInputDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1.0, Double.MaxValue)]
        public decimal StartingPower { get; set; }
        public HashSet<string> SuitColors { get; set; }

        [Required]
        public Guid? AbilityId { get; set; }
    }
}