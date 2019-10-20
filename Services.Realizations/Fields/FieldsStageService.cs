using Data.EF.Entities;
using Domain.Entities.Fields;
using Services.Contracts.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations.Fields
{
    public class FieldsStageService : IFieldsStageService
    {
        AgriculturalStageData _agriculturalStageData = new AgriculturalStageData();

        public Dictionary<AgriculturalStageEnum, FieldsStage> CalculateStagesFromCurrent(Dictionary<AgriculturalStageEnum, FieldsStage> stages, FieldsStage currentStage)
        {
            currentStage.Duration = _agriculturalStageData.AgriculturalStages.FirstOrDefault(s => s.Id == currentStage.Id).Duration;

            var nextStageDate = currentStage.StartDate + currentStage.Duration;

            foreach (var stage in stages)
            {
                if (stage.Key == currentStage.Id)
                {
                    stage.Value.StartDate = currentStage.StartDate;
                }
                else if (stage.Key > currentStage.Id)
                {
                    stage.Value.StartDate = nextStageDate;
                    nextStageDate = stage.Value.StartDate + stage.Value.Duration;

                    if (stage.Key == AgriculturalStageEnum.Restoring && currentStage.Id > AgriculturalStageEnum.Grazing)
                    {
                        stages[AgriculturalStageEnum.Ready].StartDate = nextStageDate;
                    }
                }
            }

            return stages;
        }

        public Dictionary<AgriculturalStageEnum, FieldsStage> CalculateStagesFromCurrent(FieldsStage currentStage)
        {
            var stages = new Dictionary<AgriculturalStageEnum, FieldsStage>();

            var stagesInfo = _agriculturalStageData.AgriculturalStages.Where(s => s.Id > currentStage.Id);

            currentStage.Duration = _agriculturalStageData.AgriculturalStages.FirstOrDefault(s => s.Id == currentStage.Id).Duration;
            stages.Add(currentStage.Id, currentStage);
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
                stages.Add(stage.Id, newStage);
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
