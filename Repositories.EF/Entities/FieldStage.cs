using Domain.Entities.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF.Entities
{
    public class FieldStage
    {
        public Guid FieldId { get; set; }
        public AgriculturalStageEnum StageId { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsCurrent { get; set; }
    }
}
