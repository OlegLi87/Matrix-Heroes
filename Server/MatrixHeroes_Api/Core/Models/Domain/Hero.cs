using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixHeroes_Api.Core.Models.Domain
{
    public class Hero : BaseEntity
    {
        private const int MAX_ALLOWED_SESSION_TRAININGS = 5;
        private const int SESSION_DURATION_IN_DAYS = 1;
        public HashSet<String> SuitColors { get; set; }

        [Column(TypeName = "decimal(8,3)")]
        public decimal StartingPower { get; set; }

        [Column(TypeName = "decimal(8,3)")]
        public decimal CurrentPower { get; set; }
        public DateTimeOffset TrainingStartDate { get; set; }
        public DateTimeOffset TrainingSessionStart { get; set; }
        public int TrainingsInCurrentSession { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Ability Ability { get; set; }

        public bool TryResetCounter()
        {
            if ((DateTimeOffset.UtcNow - TrainingSessionStart).Days > SESSION_DURATION_IN_DAYS)
            {
                TrainingsInCurrentSession = 0;
                return true;
            }
            return false;
        }

        public bool Train()
        {
            if (TrainingsInCurrentSession < 5)
            {
                if (TrainingStartDate == default(DateTimeOffset))
                    TrainingStartDate = DateTimeOffset.UtcNow;

                if (TrainingsInCurrentSession == 0)
                    TrainingSessionStart = DateTimeOffset.UtcNow;

                var rand = new Random();
                TrainingsInCurrentSession++;
                CurrentPower += CurrentPower * rand.Next(1, 11) / 100;
                return true;
            }
            return false;
        }
    }
}