using Data.EF;
using Data.EF.Entities;
using Domain.Entities.Fields;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces.Fields;
using System;
using System.Collections.Generic;
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

        public void AddFields(List<Field> fields)
        {
            var fieldsData = _context.Fields;
            foreach(var field in fields)
            {
                var item = new FieldsData
                {
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
                fieldsData.Add(item);
            }

            _context.SaveChanges();
        }

        public Field GetField(Guid id)
        {
            var field = _context.Fields.Find(id);
            _context.Entry(field).State = EntityState.Detached;
            return new Field
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

            return result;
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

            _context.Entry(fieldDto).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
