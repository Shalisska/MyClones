using Data.EF.Entities;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IFieldService
    {
        IEnumerable<Field> GetFields();

        void AddFields(int count, string location);
    }
}
