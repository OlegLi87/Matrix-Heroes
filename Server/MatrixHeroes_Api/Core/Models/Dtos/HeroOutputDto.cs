using System;

namespace MatrixHeroes_Api.Core.Models.Dtos
{
    public class HeroOutputDto : HeroInputDto
    {
        public Guid Id { get; set; }
        public decimal CurrentPower { get; set; }
        public DateTimeOffset TrainingStartDate { get; set; }
        public byte TrainingsInCurrentSession { get; set; }
        public string AbilityName { get; set; }
    }
}