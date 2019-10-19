using Data.EF.Entities;
using Domain.Entities.Fields;
using Services.Contracts.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Implementations.Fields
{
    public class FieldsStageService : IFieldsStageService
    {
        AgriculturalStageData _agriculturalStageData = new AgriculturalStageData();

        public List<FieldsStage> CalculateStagesFromCurrent(List<FieldsStage> stages, FieldsStage currentStage)
        {
            currentStage.Duration = _agriculturalStageData.AgriculturalStages.FirstOrDefault(s => s.Id == currentStage.Id).Duration;

            var nextStageDate = currentStage.StartDate + currentStage.Duration;

            foreach (var stage in stages)
            {
                if (stage.Id == currentStage.Id)
                {
                    stage.StartDate = currentStage.StartDate;
                }
                else if (stage.Id > currentStage.Id)
                {
                    stage.StartDate = nextStageDate;
                    nextStageDate = stage.StartDate + stage.Duration;

                    if (stage.Id == AgriculturalStageEnum.Restoring && currentStage.Id > AgriculturalStageEnum.Grazing)
                    {
                        stages[0].StartDate = nextStageDate;
                    }
                }
            }

            return stages;
        }

        public List<FieldsStage> CalculateStagesFromCurrent(FieldsStage currentStage)
        {
            var stages = new List<FieldsStage>();

            var stagesInfo = _agriculturalStageData.AgriculturalStages.Where(s => s.Id > currentStage.Id);

            currentStage.Duration = _agriculturalStageData.AgriculturalStages.FirstOrDefault(s => s.Id == currentStage.Id).Duration;
            stages.Add(currentStage);
            var nextStageDate = currentStage.StartDate + currentStage.Duration;

            foreach(var stage in stagesInfo)
            {
                var newStage = new FieldsStage
                {
                    Id = stage.Id,
                    StartDate = nextStageDate,
                    Duration = stage.Duration,
                    IsCurrent = false
                };
                stages.Add(newStage);
                nextStageDate = newStage.StartDate + newStage.Duration;
            }

            return stages;
        }

        public (FieldsStage stage, bool needUpdate) GetCurrentStage(List<FieldsStage> stages)
        {
            throw new NotImplementedException();
        }

        public FieldsStage GetNextStage(List<FieldsStage> stages, AgriculturalStageEnum currentStageId)
        {
            throw new NotImplementedException();
        }
    }
}
