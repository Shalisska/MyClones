using Data.EF.Entities;
using System;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IFieldService
    {
        IEnumerable<HouseLocation> GetHouseLocations();
        IEnumerable<Crop> GetCrops();
        IEnumerable<Field> GetFields();
        Field GetField(Guid id);

        void AddFields(int count, int locationId);
        void UpdateField(Guid id, int cultureId, AgriculturalStageEnum stage, DateTime startDate);
    }
}
