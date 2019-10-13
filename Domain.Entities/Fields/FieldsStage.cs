using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Fields
{
    public class FieldsStage
    {
        public AgriculturalStageEnum Id { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsCurrent { get; set; }
    }
}
