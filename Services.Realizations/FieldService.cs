using Data.EF;
using Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Realizations
{
    public class FieldService : IFieldService
    {
        ClonesDbContext _context;
        HouseLocationData _houseLocationData = new HouseLocationData();
        AgriculturalStageData _agriculturalStageData = new AgriculturalStageData();
        CropData _cropData = new CropData();

        public FieldService()
        {
            _context = new ClonesDbContext();
        }

        public IEnumerable<HouseLocation> GetHouseLocations()
        {
            return _houseLocationData.HouseLocations;
        }

        public IEnumerable<Crop> GetCrops()
        {
            return _cropData.Crops;
        }

        public IEnumerable<Field> GetFields()
        {
            var fields = _context.Fields;

            return fields;
        }

        public Field GetField(Guid id)
        {
            var field = _context.Fields.Find(id);
            return field;
        }

        public void UpdateField(Guid id, int cultureId, AgriculturalStageEnum stage, DateTime startDate)
        {
            var field = GetField(id);
            var culture = _cropData.Crops.FirstOrDefault(c => c.Id == cultureId);
            field.Culture = culture.Name;

            switch (stage)
            {
                case AgriculturalStageEnum.Grazing:
                    field.Grazing = startDate;
                    CalcFertilizing(field);
                    break;
                case AgriculturalStageEnum.Fertilizing:
                    field.Fertilizing = startDate;
                    CalcSowing(field);
                    break;
                case AgriculturalStageEnum.Sowing:
                    field.Sowing = startDate;
                    CalcGrowing(field);
                    break;
                case AgriculturalStageEnum.Harvesting:
                    field.Harvesting = startDate;
                    CalcRestoring(field);
                    break;
            }

            _context.Entry(field).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddFields(int count, int locationId)
        {
            var location = _houseLocationData.HouseLocations.FirstOrDefault(l => l.LocationId == locationId);

            while(count > 0)
            {
                AddNewField(location.LocationName);
                count--;
            }
            _context.SaveChanges();
        }

        private void AddNewField(string location)
        {
            var stages = _agriculturalStageData.AgriculturalStages;

            var field = new Field()
            {
                HouseLocation = location,

                CultureSeedPrice = 0m,
                FertilizePrice = 0m,
                HarvestTax = 0m,

                Ready = DateTime.Now,

                GrazingPeriod = GetPeriodByStage(stages, AgriculturalStageEnum.Grazing),
                FertilizingPeriod = GetPeriodByStage(stages, AgriculturalStageEnum.Fertilizing),
                SowingPeriod = GetPeriodByStage(stages, AgriculturalStageEnum.Sowing),
                GrowingPeriod = GetPeriodByStage(stages, AgriculturalStageEnum.Growing),
                HarvestingPeriod = GetPeriodByStage(stages, AgriculturalStageEnum.Harvesting),
                RestoringPeriod = GetPeriodByStage(stages, AgriculturalStageEnum.Restoring)
            };

            field.Grazing = field.Ready;
            CalcFertilizing(field);

            _context.Fields.Add(field);
        }

        private TimeSpan GetPeriodByStage(List<AgriculturalStage> stages, AgriculturalStageEnum currentStage)
        {
            var stage = stages.FirstOrDefault(s => s.Id == currentStage);
            return stage.Duration;
        }

        private void CalcFertilizing(Field field)
        {
            field.Fertilizing = field.Grazing + field.GrazingPeriod;

            CalcSowing(field);
        }

        private void CalcSowing(Field field)
        {
            field.Sowing = field.Fertilizing + field.FertilizingPeriod;

            CalcGrowing(field);
        }

        private void CalcGrowing(Field field)
        {
            field.Growing = field.Sowing + field.SowingPeriod;

            CalcHarvesting(field);
        }

        private void CalcHarvesting(Field field)
        {
            field.Harvesting = field.Growing + field.GrowingPeriod;

            CalcRestoring(field);
        }

        private void CalcRestoring(Field field)
        {
            field.Restoring = field.Harvesting + field.HarvestingPeriod;

            CalcReady(field);
        }

        private void CalcReady(Field field)
        {
            if (DateTime.Now > field.Growing)
                field.Ready = field.Restoring + field.RestoringPeriod;
        }
    }
}
