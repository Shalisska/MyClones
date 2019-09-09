using Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IFieldService
    {
        IEnumerable<Field> GetFields();

        void AddNewField();
    }
}
