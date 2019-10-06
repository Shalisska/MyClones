using FluentMigrator;

namespace DBMigrations.Migrations
{
    [Migration(20191006112900)]
    public class UpdateFieldTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("Fields")
                .AlterColumn("Culture").AsString().Nullable()
                .AddColumn("HouseLocation").AsString()
                .AddColumn("CultureSeedPrice").AsDecimal(18, 4)
                .AddColumn("FertilizePrice").AsDecimal(18, 4)
                .AddColumn("HarvestTax").AsDecimal(18, 4);
        }
    }
}
