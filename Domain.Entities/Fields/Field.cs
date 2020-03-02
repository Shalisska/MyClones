using System;
using System.Collections.Generic;

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

        public List<FieldsStage> Stages { get; set; }
    }
}
