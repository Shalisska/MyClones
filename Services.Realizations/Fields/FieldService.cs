using Data.EF.Entities;
using Domain.Entities.Fields;
using Repositories.Interfaces.Fields;
using Services.Contracts.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations.Fields
{
    public class FieldService : IFieldService
    {
        IFieldsRepository _fieldsRepository;
        IFieldsStageService _fieldsStageService;

        HouseLocationData _houseLocationData = new HouseLocationData();
        CropData _cropData = new CropData();

        public FieldService(
            IFieldsRepository fieldsRepository,
            IFieldsStageService fieldsStageService)
        {
            _fieldsRepository = fieldsRepository;
            _fieldsStageService = fieldsStageService;
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

            var currentStage = new FieldsStage
            {
                Id = stage,
                StartDate = startDate,
                IsCurrent = true
            };
            var stages = _fieldsStageService.CalculateStagesFromCurrent(field.FieldsStages, currentStage);
            field.FieldsStages = stages;

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
            var field = new Field()
            {
                HouseLocation = location,

                CultureSeedPrice = 0m,
                FertilizePrice = 0m,
                HarvestTax = 0m
            };

            var currentStage = new FieldsStage
            {
                Id = AgriculturalStageEnum.Ready,
                StartDate = DateTime.Now,
                IsCurrent = true
            };

            var stages = _fieldsStageService.CalculateStagesFromCurrent(currentStage);
            field.FieldsStages = stages;

            fields.Add(field);
        }
    }
}
