using Data.EF;
using Data.EF.Entities;
using Domain.Entities.Fields;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Realizations.Fields
{
    public class FieldsRepository : IFieldsRepository
    {
        //ClonesDbContext _context;

        public FieldsRepository()
        {
            //_context = new ClonesDbContext();
        }

        public void AddFields(List<Field> fields)
        {
            using var _context = new ClonesDbContext();
            var fieldsData = _context.Fields;

            foreach (var field in fields)
            {
                var item = new FieldsData
                {
                    HouseLocation = field.HouseLocation,

                    CultureSeedPrice = field.CultureSeedPrice,
                    FertilizePrice = field.FertilizePrice,
                    HarvestTax = field.HarvestTax,

                    Ready = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Ready).StartDate,
                    Grazing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Grazing).StartDate,
                    Fertilizing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Fertilizing).StartDate,
                    Sowing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Sowing).StartDate,
                    Growing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Growing).StartDate,
                    Harvesting = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Harvesting).StartDate,
                    Restoring = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Restoring).StartDate,

                    GrazingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Grazing).Duration,
                    FertilizingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Fertilizing).Duration,
                    SowingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Sowing).Duration,
                    GrowingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Growing).Duration,
                    HarvestingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Harvesting).Duration,
                    RestoringPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Restoring).Duration
                };
                fieldsData.Add(item);
            }

            _context.SaveChanges();
        }

        public Field GetField(Guid id)
        {
            using var _context = new ClonesDbContext();
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
            };

            var stages = GetFieldsStages(field);
            result.Stages = stages;

            return result;
        }

        private List<FieldsStage> GetFieldsStages(FieldsData field)
        {
            var stages = new List<FieldsStage>
            {
                { BuildFieldStage(AgriculturalStageEnum.Ready, field.Ready, new TimeSpan(0)) },
                { BuildFieldStage(AgriculturalStageEnum.Grazing, field.Grazing, field.GrazingPeriod) },
                { BuildFieldStage(AgriculturalStageEnum.Fertilizing, field.Fertilizing, field.FertilizingPeriod) },
                { BuildFieldStage(AgriculturalStageEnum.Sowing, field.Sowing, field.SowingPeriod) },
                { BuildFieldStage(AgriculturalStageEnum.Growing, field.Growing, field.GrowingPeriod) },
                { BuildFieldStage(AgriculturalStageEnum.Harvesting, field.Harvesting, field.HarvestingPeriod) },
                { BuildFieldStage(AgriculturalStageEnum.Restoring, field.Restoring, field.RestoringPeriod) }
            };

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
            using var _context = new ClonesDbContext();
            var fieldsDto = _context.Fields.ToList();
            var fields = new List<Field>();

            foreach (var fieldDto in fieldsDto)
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

                Ready = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Ready).StartDate,
                Grazing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Grazing).StartDate,
                Fertilizing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Fertilizing).StartDate,
                Sowing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Sowing).StartDate,
                Growing = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Growing).StartDate,
                Harvesting = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Harvesting).StartDate,
                Restoring = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Restoring).StartDate,

                GrazingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Grazing).Duration,
                FertilizingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Fertilizing).Duration,
                SowingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Sowing).Duration,
                GrowingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Growing).Duration,
                HarvestingPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Harvesting).Duration,
                RestoringPeriod = field.Stages.FirstOrDefault(f => f.Id == AgriculturalStageEnum.Restoring).Duration
            };

            using var _context = new ClonesDbContext();
            _context.Entry(fieldDto).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
