using Data.EF;
using Data.EF.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Realizations
{
    public class FieldService : IFieldService
    {
        ClonesDbContext _context;

        public FieldService()
        {
            _context = new ClonesDbContext();
        }

        public IEnumerable<Field> GetFields()
        {
            var fields = _context.Fields;

            return fields;
        }

        public void AddFields(int count, string location)
        {
            while(count > 0)
            {
                AddNewField(location);
                count--;
            }
            _context.SaveChanges();
        }

        public void AddNewField(string location)
        {
            var field = new Field()
            {
                HouseLocation = location,

                CultureSeedPrice = 0m,
                FertilizePrice = 0m,
                HarvestTax = 0m,

                Ready = DateTime.Now,

                GrazingPeriod = TimeSpan.FromHours(48),
                FertilizingPeriod = TimeSpan.FromHours(120),
                SowingPeriod = TimeSpan.FromHours(48),
                GrowingPeriod = TimeSpan.FromHours(360),
                HarvestingPeriod = TimeSpan.FromHours(72),
                RestoringPeriod = TimeSpan.FromHours(360)
            };

            field.Grazing = field.Ready;
            CalcFertilizing(field);

            _context.Fields.Add(field);
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
