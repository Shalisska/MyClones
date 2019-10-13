using Domain.Entities.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces.Fields
{
    public interface IFieldsStageService
    {
        (FieldsStage stage, bool needUpdate) GetCurrentStage(List<FieldsStage> stages);
        FieldsStage GetNextStage(List<FieldsStage> stages, AgriculturalStageEnum currentStageId);
        List<FieldsStage> CalculateStagesFromCurrent(List<FieldsStage> stages, FieldsStage currentStage);
        List<FieldsStage> CalculateStagesFromCurrent(FieldsStage currentStage);
    }
}
