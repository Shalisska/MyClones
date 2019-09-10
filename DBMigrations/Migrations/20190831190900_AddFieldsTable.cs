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
                .WithColumn("GrazingPeriod").AsInt64()
                .WithColumn("FertilizingPeriod").AsInt64()
                .WithColumn("SowingPeriod").AsInt64()
                .WithColumn("GrowingPeriod").AsInt64()
                .WithColumn("HarvestingPeriod").AsInt64()
                .WithColumn("RestoringPeriod").AsInt64();
        }
    }
}
