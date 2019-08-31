using System;
using System.Collections.Generic;
using System.Text;
using FluentMigrator;

namespace DBMigrations.Migrations
{
    [Migration(20190831190900)]
    public class AddFieldsTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Fields")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Culture").AsString()
                .WithColumn("Ready").AsDateTime()
                .WithColumn("Grazing").AsDateTime()
                .WithColumn("Fertilizing").AsDateTime()
                .WithColumn("Sowing").AsDateTime()
                .WithColumn("Growing").AsDateTime()
                .WithColumn("Harvesting").AsDateTime()
                .WithColumn("Restoring").AsDateTime();
        }
    }
}
