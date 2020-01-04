using Domain.Entities.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Contracts.Fields
{
    public interface IFieldsRepository
    {
        IEnumerable<Field> GetFields();
        Field GetField(Guid id);

        void AddFields(List<Field> fields);
        void UpdateField(Field field);
    }
}
