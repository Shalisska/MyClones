using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.MyClones.Services
{
    public interface IFieldService
    {
        string GetFields();
    }

    public class FieldService : IFieldService
    {
        public string GetFields()
        {
            return "Fields";
        }
    }
}
