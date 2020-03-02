using Data.EF.Entities;
using Domain.Entities.Common;
using Domain.Entities.Fields;
using Services.Contracts.Fields;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementations.Fields
{
    public class FieldsStageService : IFieldsStageService
    {
        AgriculturalStageData _agriculturalStageData = new AgriculturalStageData();

        public StateMachine<AgriculturalStageEnum, ManufactoringStageEvent> GetFieldStagesFSM(Field field)
        {
            var currentStage = field.Stages.FirstOrDefault(s => s.State == ManufactoringStageState.Processing)?.Id ??
                                AgriculturalStageEnum.Grazing;

            var machine = new StateMachine<AgriculturalStageEnum, ManufactoringStageEvent>(currentStage);

            var sourceStage = currentStage;

            machine.Configure(AgriculturalStageEnum.Grazing)
                .OnEntryFrom(ManufactoringStageEvent.EndStage, t => ExecuteOnStartStage(t.Destination, field))
                .OnExit(t => ExecuteOnExitFromStage(t.Source, t.Trigger, field))
                .PermitIf(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Fertilizing, () => IsStagePending(AgriculturalStageEnum.Fertilizing, field))
                .PermitIf(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Sowing, () => !IsStagePending(AgriculturalStageEnum.Fertilizing, field))
                .PermitIf(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Fertilizing, () => IsStagePending(AgriculturalStageEnum.Fertilizing, field))
                .PermitIf(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Sowing, () => !IsStagePending(AgriculturalStageEnum.Fertilizing, field));

            machine.Configure(AgriculturalStageEnum.Fertilizing)
                .OnEntryFrom(ManufactoringStageEvent.EndStage, t => ExecuteOnStartStage(t.Destination, field))
                .OnExit(t => ExecuteOnExitFromStage(t.Source, t.Trigger, field))
                .PermitIf(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Grazing, () => IsStagePending(AgriculturalStageEnum.Grazing, field))
                .PermitIf(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Sowing, () => !IsStagePending(AgriculturalStageEnum.Grazing, field))
                .PermitIf(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Grazing, () => !IsStagePending(AgriculturalStageEnum.Fertilizing, field))
                .PermitIf(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Sowing, () => IsStagePending(AgriculturalStageEnum.Fertilizing, field));

            machine.Configure(AgriculturalStageEnum.Sowing)
                .OnEntryFrom(ManufactoringStageEvent.EndStage, t => ExecuteOnStartStage(t.Destination, field))
                .OnExit(t => ExecuteOnExitFromStage(t.Source, t.Trigger, field))
                .Permit(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Growing)
                .Permit(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Growing);

            machine.Configure(AgriculturalStageEnum.Growing)
                .OnEntryFrom(ManufactoringStageEvent.EndStage, t => ExecuteOnStartStage(t.Destination, field))
                .OnExit(t => ExecuteOnExitFromStage(t.Source, t.Trigger, field))
                .Permit(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Harvesting)
                .Permit(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Harvesting);

            machine.Configure(AgriculturalStageEnum.Harvesting)
                .OnEntryFrom(ManufactoringStageEvent.EndStage, t => ExecuteOnStartStage(t.Destination, field))
                .OnExit(t => ExecuteOnExitFromStage(t.Source, t.Trigger, field))
                .Permit(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Restoring)
                .Permit(ManufactoringStageEvent.NextStage, AgriculturalStageEnum.Restoring);

            machine.Configure(AgriculturalStageEnum.Restoring)
                .OnEntryFrom(ManufactoringStageEvent.EndStage, t => ExecuteOnStartStage(t.Destination, field))
                .OnExit(t => ExecuteOnExitFromStage(t.Source, t.Trigger, field))
                .Permit(ManufactoringStageEvent.EndStage, AgriculturalStageEnum.Grazing);

            return machine;
        }

        private bool IsStagePending(AgriculturalStageEnum targetState, Field field)
        {
            var targetStage = field.Stages.FirstOrDefault(s => s.Id == targetState);

            return targetStage.State == ManufactoringStageState.Pending;
        }

        private void ExecuteOnStartStage(AgriculturalStageEnum state, Field field)
        {
            var stage = field.Stages.FirstOrDefault(s => s.Id == state);
            var innerMachine = InnerFSM(stage);
            innerMachine.Fire(ManufactoringStageEvent.StartProcess);
        }

        private void ExecuteOnExitFromStage(AgriculturalStageEnum state, ManufactoringStageEvent trigger, Field field)
        {
            if (trigger == ManufactoringStageEvent.EndStage)
                ExecuteOnEndStage(state, field);
        }

        private void ExecuteOnEndStage(AgriculturalStageEnum state, Field field)
        {
            var stage = field.Stages.FirstOrDefault(s => s.Id == state);
            var innerMachine = InnerFSM(stage);
            innerMachine.Fire(ManufactoringStageEvent.EndStage);
        }

        private void UpdateStages(StateMachine<AgriculturalStageEnum, ManufactoringStageEvent> machine, Field field, DateTime date)
        {
            var currentStage = GetCurrentStage(machine, field);

            if (currentStage.EndDate <= date)
            {
                EndStage(machine);
                UpdateStages(machine, field, date);
            }

            var startDate = currentStage.EndDate;
            var nextStage = GetNextStage(machine, field);

            while (nextStage != null)
            {
                nextStage.StartDate = startDate;
                nextStage.EndDate = startDate + nextStage.Duration;

                startDate = nextStage.EndDate;
                nextStage = GetNextStage(machine, field);
            }
        }

        private void CalculateDatesForNewFieldStages(StateMachine<AgriculturalStageEnum, ManufactoringStageEvent> machine, Field field, DateTime startDate)
        {
            var currentStage = GetCurrentStage(machine, field);

            currentStage.StartDate = startDate;
            currentStage.EndDate = currentStage.StartDate + currentStage.Duration;

            var nextStartDate = currentStage.EndDate;
            var nextStage = GetNextStage(machine, field);

            while (nextStage != null)
            {
                nextStage.StartDate = nextStartDate;
                nextStage.EndDate = nextStartDate + nextStage.Duration;

                nextStartDate = nextStage.EndDate;
                nextStage = GetNextStage(machine, field);
            }
        }

        private FieldsStage GetNextStage(StateMachine<AgriculturalStageEnum, ManufactoringStageEvent> machine, Field field)
        {
            if (!machine.CanFire(ManufactoringStageEvent.NextStage))
                return null;

            machine.Fire(ManufactoringStageEvent.NextStage);
            var state = machine.State;

            return field.Stages.FirstOrDefault(s => s.Id == state);
        }

        private FieldsStage GetCurrentStage(StateMachine<AgriculturalStageEnum, ManufactoringStageEvent> machine, Field field)
        {
            var state = machine.State;
            return field.Stages.FirstOrDefault(s => s.Id == state);
        }

        private void EndStage(StateMachine<AgriculturalStageEnum, ManufactoringStageEvent> machine)
        {
            machine.Fire(ManufactoringStageEvent.EndStage);
        }

        private StateMachine<ManufactoringStageState, ManufactoringStageEvent> InnerFSM(FieldsStage stage)
        {
            var machine = new StateMachine<ManufactoringStageState, ManufactoringStageEvent>(() => stage.State, s => s = stage.State);

            machine.Configure(ManufactoringStageState.Pending)
                .Permit(ManufactoringStageEvent.StartProcess, ManufactoringStageState.Processing);

            machine.Configure(ManufactoringStageState.Processing)
                .Permit(ManufactoringStageEvent.EndStage, ManufactoringStageState.Ended);

            machine.Configure(ManufactoringStageState.Ended);

            return machine;
        }



        //public Dictionary<AgriculturalStageEnum, FieldsStage> CalculateStagesFromCurrent(Dictionary<AgriculturalStageEnum, FieldsStage> stages, FieldsStage currentStage)
        //{
        //    currentStage.Duration = _agriculturalStageData.AgriculturalStages.FirstOrDefault(s => s.Id == currentStage.Id).Duration;

        //    var nextStageDate = currentStage.StartDate + currentStage.Duration;

        //    foreach (var stage in stages)
        //    {
        //        if (stage.Key == currentStage.Id)
        //        {
        //            stage.Value.StartDate = currentStage.StartDate;
        //        }
        //        else if (stage.Key > currentStage.Id)
        //        {
        //            stage.Value.StartDate = nextStageDate;
        //            nextStageDate = stage.Value.StartDate + stage.Value.Duration;

        //            if (stage.Key == AgriculturalStageEnum.Restoring && currentStage.Id > AgriculturalStageEnum.Grazing)
        //            {
        //                stages[AgriculturalStageEnum.Ready].StartDate = nextStageDate;
        //            }
        //        }
        //    }

        //    return stages;
        //}

        public Field AddStagesToField(Field field)
        {
            field.Stages = GetDefaultFieldStages();

            var machine = GetFieldStagesFSM(field);

            CalculateDatesForNewFieldStages(machine, field, DateTime.Now);

            return field;
        }

        private List<FieldsStage> GetDefaultFieldStages()
        {
            var stagesInfo = _agriculturalStageData.AgriculturalStages;
            var stages = new List<FieldsStage>();

            foreach (var stageInfo in stagesInfo)
            {
                stages.Add(new FieldsStage
                {
                    Id = stageInfo.Id,
                    Duration = stageInfo.Duration
                });
            }

            return stages;
        }

        public (FieldsStage stage, bool needUpdate) GetCurrentStage(List<FieldsStage> stages)
        {
            var currentStage = stages.FirstOrDefault(s => s.State == ManufactoringStageState.Processing);
            return (currentStage, currentStage?.StartDate + currentStage?.Duration < DateTime.Now);
        }

        public FieldsStage GetNextStage(List<FieldsStage> stages, AgriculturalStageEnum currentStageId)
        {
            throw new NotImplementedException();
        }
    }
}
