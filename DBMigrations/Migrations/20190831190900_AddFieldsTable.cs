using FluentMigrator;

namespace DBMigrations.Migrations
{
    [Migration(20190831190900)]
    public class AddFieldsTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Fields")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Culture").AsString()
                .WithColumn("Ready").AsDateTime2()
                .WithColumn("Grazing").AsDateTime2()
                .WithColumn("Fertilizing").AsDateTime2()
                .WithColumn("Sowing").AsDateTime2()
                .WithColumn("Growing").AsDateTime2()
                .WithColumn("Harvesting").AsDateTime2()
                .WithColumn("Restoring").AsDateTime2()
                .WithColumn("GrazingPeriod").AsTime()
                .WithColumn("FertilizingPeriod").AsTime()
                .WithColumn("SowingPeriod").AsTime()
                .WithColumn("GrowingPeriod").AsTime()
                .WithColumn("HarvestingPeriod").AsTime()
                .WithColumn("RestoringPeriod").AsTime();
        }
    }
}
