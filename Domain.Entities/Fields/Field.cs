using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Fields
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Culture { get; set; }
        public string HouseLocation { get; set; }
        public decimal CultureSeedPrice { get; set; }
        public decimal FertilizePrice { get; set; }
        public decimal HarvestTax { get; set; }
        public DateTime Ready { get; set; }
        public DateTime Grazing { get; set; }
        public DateTime Fertilizing { get; set; }
        public DateTime Sowing { get; set; }
        public DateTime Growing { get; set; }
        public DateTime Harvesting { get; set; }
        public DateTime Restoring { get; set; }
        public TimeSpan GrazingPeriod { get; set; }
        public TimeSpan FertilizingPeriod { get; set; }
        public TimeSpan SowingPeriod { get; set; }
        public TimeSpan GrowingPeriod { get; set; }
        public TimeSpan HarvestingPeriod { get; set; }
        public TimeSpan RestoringPeriod { get; set; }
    }
}
