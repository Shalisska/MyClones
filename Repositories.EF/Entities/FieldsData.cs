﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.EF.Entities
{
    [Table("Fields")]
    public class FieldsData
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
}
