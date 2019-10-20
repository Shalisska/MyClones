using Domain.Entities.Fields;
using System;
using System.Linq;

namespace MVC.MyClones.ViewModels.Fields
{
    public class FieldViewModel
    {
        public FieldViewModel(Field field)
        {
            var currentStage = field.FieldsStages.Values.FirstOrDefault(s => s.IsCurrent);

            Id = field.Id;
            Culture = field.Culture;
            HouseLocation = field.HouseLocation;
            CurrentStage = currentStage.Id.ToString();
            NextStage = (currentStage.Id == AgriculturalStageEnum.Restoring ? AgriculturalStageEnum.Ready : currentStage.Id + 1).ToString();
            CurrentStageStartDate = currentStage.StartDate;
            CurrentStageDuration = currentStage.Duration;
            CurrentStageEndDate = currentStage.StartDate + currentStage.Duration;
        }

        public Guid Id { get; set; }
        public string Culture { get; set; }
        public string HouseLocation { get; set; }
        public string CurrentStage { get; set; }
        public string NextStage { get; set; }
        public DateTime CurrentStageStartDate { get; set; }
        public TimeSpan CurrentStageDuration { get; set; }
        public DateTime CurrentStageEndDate { get; set; }
    }
}
