using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF.Entities
{
    public class Crop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SeedPrice { get; set; }
        public decimal CropQuantityNonFertilizing { get; set; }
        public decimal CropQuantityFullFertilizing { get; set; }
        public decimal TaxForHarvesting { get; set; }
    }

    public class CropData
    {
        public CropData()
        {
            FillData();
        }

        public List<Crop> Crops { get; private set; }

        private void FillData()
        {
            var crops = new List<Crop>()
            {
                new Crop()
                {
                    Id=1,
                    Name="Баклажаны",
                    SeedPrice=0.1m,
                    TaxForHarvesting=0.3m,
                    CropQuantityNonFertilizing=8,
                    CropQuantityFullFertilizing=24
                },
                new Crop()
                {
                    Id=2,
                    Name="Брюква",
                    SeedPrice=0.1m,
                    TaxForHarvesting=0.3m,
                    CropQuantityNonFertilizing=10,
                    CropQuantityFullFertilizing=30
                },
                new Crop()
                {
                    Id=3,
                    Name="Капуста",
                    SeedPrice=0.1m,
                    TaxForHarvesting=0.45m,
                    CropQuantityNonFertilizing=12,
                    CropQuantityFullFertilizing=36
                }
            };

            Crops = crops;
        }
    }
}
