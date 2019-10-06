using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF.Entities
{
    public class AgriculturalStage
    {
        public AgriculturalStageEnum Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public bool NeedConfirm { get; set; }
    }

    public enum AgriculturalStageEnum
    {
        Ready = 0,
        Grazing = 1,
        Fertilizing = 2,
        Sowing = 3,
        Growing = 4,
        Harvesting = 5,
        Restoring = 6
    }

    public class AgriculturalStageData
    {
        public AgriculturalStageData()
        {
            FillData();
        }

        public List<AgriculturalStage> AgriculturalStages { get; private set; }

        private void FillData()
        {
            var stages = new List<AgriculturalStage>
            {
                new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Ready,
                    Name=AgriculturalStageEnum.Ready.ToString(),
                    Duration=TimeSpan.FromHours(0),
                    NeedConfirm=false
                },new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Grazing,
                    Name=AgriculturalStageEnum.Grazing.ToString(),
                    Duration=TimeSpan.FromHours(48),
                    NeedConfirm=true
                },new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Fertilizing,
                    Name=AgriculturalStageEnum.Fertilizing.ToString(),
                    Duration=TimeSpan.FromHours(120),
                    NeedConfirm=true
                },new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Sowing,
                    Name=AgriculturalStageEnum.Sowing.ToString(),
                    Duration=TimeSpan.FromHours(48),
                    NeedConfirm=true
                },new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Growing,
                    Name=AgriculturalStageEnum.Growing.ToString(),
                    Duration=TimeSpan.FromHours(360),
                    NeedConfirm=false
                },new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Harvesting,
                    Name=AgriculturalStageEnum.Harvesting.ToString(),
                    Duration=TimeSpan.FromHours(72),
                    NeedConfirm=true
                },
                new AgriculturalStage()
                {
                    Id=AgriculturalStageEnum.Restoring,
                    Name=AgriculturalStageEnum.Restoring.ToString(),
                    Duration=TimeSpan.FromHours(360),
                    NeedConfirm=false
                }
            };

            AgriculturalStages = stages;
        }
    }
}
