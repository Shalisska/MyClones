using Domain.Entities.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts.Fields
{
    public interface IFieldsStageService
    {
        (FieldsStage stage, bool needUpdate) GetCurrentStage(List<FieldsStage> stages);
        FieldsStage GetNextStage(List<FieldsStage> stages, AgriculturalStageEnum currentStageId);
        //Dictionary<AgriculturalStageEnum, FieldsStage> CalculateStagesFromCurrent(Dictionary<AgriculturalStageEnum, FieldsStage> stages, FieldsStage currentStage);

        Field AddStagesToField(Field field);
    }
}
