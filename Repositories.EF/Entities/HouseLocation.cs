using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF.Entities
{
    public class HouseLocation
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }

    public class HouseLocationData
    {
        public HouseLocationData()
        {
            FillData();
        }

        public List<HouseLocation> HouseLocations { get; private set; }

        private void FillData()
        {
            var locations = new List<HouseLocation>()
            {
                new HouseLocation()
                {
                    LocationId=1,
                    LocationName="Фэйлун"
                },
                new HouseLocation()
                {
                    LocationId=2,
                    LocationName="Дубец"
                },
                new HouseLocation()
                {
                    LocationId=3,
                    LocationName="Белояр"
                },
                new HouseLocation()
                {
                    LocationId=4,
                    LocationName="Обережное"
                },
                new HouseLocation()
                {
                    LocationId=5,
                    LocationName="Кленники"
                }
            };

            HouseLocations = locations;
        }
    }
}
