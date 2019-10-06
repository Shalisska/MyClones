using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.EF.Entities
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
        [Column(TypeName = "bigint")]
        public TimeSpan GrazingPeriod { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan FertilizingPeriod { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan SowingPeriod { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan GrowingPeriod { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan HarvestingPeriod { get; set; }
        [Column(TypeName = "bigint")]
        public TimeSpan RestoringPeriod { get; set; }
    }

    public enum FieldPeriodType
    {
        Ready = 0,
        Grazing = 1,
        Fertilizing = 2,
        Sowing = 3,
        Growing = 4,
        Harvesting = 5,
        Restoring = 6
    }
}
