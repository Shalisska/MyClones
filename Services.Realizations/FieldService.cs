using Data.EF;
using Data.EF.Entities;
using Domain.Entities.Fields;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces.Fields;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Realizations
{
    public class FieldService : IFieldService
    {
        IFieldsRepository _fieldsRepository;

        HouseLocationData _houseLocationData = new HouseLocationData();
        AgriculturalStageData _agriculturalStageData = new AgriculturalStageData();
        CropData _cropData = new CropData();

        public FieldService(
            IFieldsRepository fieldsRepository)
        {
            _fieldsRepository = fieldsRepository;
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
            var fields = _fieldsRepository.GetFields();

            return fields;
        }

        public Field GetField(Guid id)
        {
            var field = _fieldsRepository.GetField(id);
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

            _fieldsRepository.UpdateField(field);
        }

        public void AddFields(int count, int locationId)
        {
            var location = _houseLocationData.HouseLocations.FirstOrDefault(l => l.LocationId == locationId);
            var fields = new List<Field>();

            while(count > 0)
            {
                AddNewFieldToList(location.LocationName, fields);
                count--;
            }
            _fieldsRepository.AddFields(fields);
        }

        private void AddNewFieldToList(string location, List<Field> fields)
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

            fields.Add(field);
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
