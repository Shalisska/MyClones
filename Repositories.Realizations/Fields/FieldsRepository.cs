using Data.EF;
using Data.EF.Entities;
using Domain.Entities.Fields;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Realizations.Fields
{
    public class FieldsRepository : IFieldsRepository
    {
        ClonesDbContext _context;

        public FieldsRepository()
        {
            _context = new ClonesDbContext();
        }

        private FieldsStage GetCurrentStage(AgriculturalStageEnum id, List<FieldsStage> stages)
        {
            var stage = stages.FirstOrDefault(s => s.Id == id);
            return stage;
        }

        public void AddFields(List<Field> fields)
        {
            var fieldsData = _context.Fields;
            foreach(var field in fields)
            {
                var stages = field.FieldsStages;

                var item = new FieldsData
                {
                    HouseLocation = field.HouseLocation,

                    CultureSeedPrice = field.CultureSeedPrice,
                    FertilizePrice = field.FertilizePrice,
                    HarvestTax = field.HarvestTax,

                    Ready = GetCurrentStage(AgriculturalStageEnum.Ready, field.FieldsStages).StartDate,
                    GrazingPeriod = GetCurrentStage(AgriculturalStageEnum.Grazing, field.FieldsStages).Duration,
                    FertilizingPeriod = GetCurrentStage(AgriculturalStageEnum.Fertilizing, field.FieldsStages).Duration,
                    SowingPeriod = GetCurrentStage(AgriculturalStageEnum.Sowing, field.FieldsStages).Duration,
                    GrowingPeriod = GetCurrentStage(AgriculturalStageEnum.Growing, field.FieldsStages).Duration,
                    HarvestingPeriod = GetCurrentStage(AgriculturalStageEnum.Harvesting, field.FieldsStages).Duration,
                    RestoringPeriod = GetCurrentStage(AgriculturalStageEnum.Restoring, field.FieldsStages).Duration,

                    Grazing = GetCurrentStage(AgriculturalStageEnum.Grazing, field.FieldsStages).StartDate,
                    Fertilizing = GetCurrentStage(AgriculturalStageEnum.Fertilizing, field.FieldsStages).StartDate,
                    Sowing = GetCurrentStage(AgriculturalStageEnum.Sowing, field.FieldsStages).StartDate,
                    Growing = GetCurrentStage(AgriculturalStageEnum.Growing, field.FieldsStages).StartDate,
                    Harvesting = GetCurrentStage(AgriculturalStageEnum.Harvesting, field.FieldsStages).StartDate,
                    Restoring = GetCurrentStage(AgriculturalStageEnum.Restoring, field.FieldsStages).StartDate
                };
                fieldsData.Add(item);
            }

            _context.SaveChanges();
        }

        public Field GetField(Guid id)
        {
            var field = _context.Fields.Find(id);
            _context.Entry(field).State = EntityState.Detached;
            return ConvertFromDto(field);
        }

        private Field ConvertFromDto(FieldsData field)
        {
            var result = new Field
            {
                Id = field.Id,
                Culture = field.Culture,

                HouseLocation = field.HouseLocation,

                CultureSeedPrice = field.CultureSeedPrice,
                FertilizePrice = field.FertilizePrice,
                HarvestTax = field.HarvestTax,

                Ready = field.Ready,
                GrazingPeriod = field.GrazingPeriod,
                FertilizingPeriod = field.FertilizingPeriod,
                SowingPeriod = field.SowingPeriod,
                GrowingPeriod = field.GrowingPeriod,
                HarvestingPeriod = field.HarvestingPeriod,
                RestoringPeriod = field.RestoringPeriod,

                Grazing = field.Grazing,
                Fertilizing = field.Fertilizing,
                Sowing = field.Sowing,
                Growing = field.Growing,
                Harvesting = field.Harvesting,
                Restoring = field.Restoring
            };

            var stages = GetFieldsStages(result);
            result.FieldsStages = stages;

            return result;
        }

        private List<FieldsStage> GetFieldsStages(Field field)
        {
            var stages = new List<FieldsStage>();

            stages.Add(BuildFieldStage(AgriculturalStageEnum.Ready, field.Ready, new TimeSpan(0)));
            stages.Add(BuildFieldStage(AgriculturalStageEnum.Grazing, field.Grazing, field.GrazingPeriod));
            stages.Add(BuildFieldStage(AgriculturalStageEnum.Fertilizing, field.Fertilizing, field.FertilizingPeriod));
            stages.Add(BuildFieldStage(AgriculturalStageEnum.Sowing, field.Sowing, field.SowingPeriod));
            stages.Add(BuildFieldStage(AgriculturalStageEnum.Growing, field.Growing, field.GrowingPeriod));
            stages.Add(BuildFieldStage(AgriculturalStageEnum.Harvesting, field.Harvesting, field.HarvestingPeriod));
            stages.Add(BuildFieldStage(AgriculturalStageEnum.Restoring, field.Restoring, field.RestoringPeriod));

            return stages;
        }

        private FieldsStage BuildFieldStage(AgriculturalStageEnum id, DateTime date, TimeSpan duration)
        {
            var isCurrent = date <= DateTime.Now && date + duration > DateTime.Now;
            return new FieldsStage
            {
                Id = id,
                StartDate = date,
                Duration = duration,
                IsCurrent = isCurrent
            };
        }

        public IEnumerable<Field> GetFields()
        {
            var fieldsDto = _context.Fields;
            var fields = new List<Field>();

            foreach(var fieldDto in fieldsDto)
            {
                var field = ConvertFromDto(fieldDto);
                fields.Add(field);
            }

            return fields;
        }

        public void UpdateField(Field field)
        {
            var fieldDto = new FieldsData
            {
                Id = field.Id,
                Culture = field.Culture,

                HouseLocation = field.HouseLocation,

                CultureSeedPrice = field.CultureSeedPrice,
                FertilizePrice = field.FertilizePrice,
                HarvestTax = field.HarvestTax,

                Ready = GetCurrentStage(AgriculturalStageEnum.Ready, field.FieldsStages).StartDate,
                GrazingPeriod = GetCurrentStage(AgriculturalStageEnum.Grazing, field.FieldsStages).Duration,
                FertilizingPeriod = GetCurrentStage(AgriculturalStageEnum.Fertilizing, field.FieldsStages).Duration,
                SowingPeriod = GetCurrentStage(AgriculturalStageEnum.Sowing, field.FieldsStages).Duration,
                GrowingPeriod = GetCurrentStage(AgriculturalStageEnum.Growing, field.FieldsStages).Duration,
                HarvestingPeriod = GetCurrentStage(AgriculturalStageEnum.Harvesting, field.FieldsStages).Duration,
                RestoringPeriod = GetCurrentStage(AgriculturalStageEnum.Restoring, field.FieldsStages).Duration,

                Grazing = GetCurrentStage(AgriculturalStageEnum.Grazing, field.FieldsStages).StartDate,
                Fertilizing = GetCurrentStage(AgriculturalStageEnum.Fertilizing, field.FieldsStages).StartDate,
                Sowing = GetCurrentStage(AgriculturalStageEnum.Sowing, field.FieldsStages).StartDate,
                Growing = GetCurrentStage(AgriculturalStageEnum.Growing, field.FieldsStages).StartDate,
                Harvesting = GetCurrentStage(AgriculturalStageEnum.Harvesting, field.FieldsStages).StartDate,
                Restoring = GetCurrentStage(AgriculturalStageEnum.Restoring, field.FieldsStages).StartDate
            };

            _context.Entry(fieldDto).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
