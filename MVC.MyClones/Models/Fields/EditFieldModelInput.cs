using Data.EF.Entities;
using Domain.Entities.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.MyClones.Models.Fields
{
    public class EditFieldModelInput
    {
        public Guid Id { get; set; }
        public int CultureId { get; set; }
        public AgriculturalStageEnum CurrentStage { get; set; }
        public DateTime StartDate { get; set; }
    }
}
