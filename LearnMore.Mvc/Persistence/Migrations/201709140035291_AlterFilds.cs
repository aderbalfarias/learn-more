using System.Data.Entity.Migrations;

namespace LearnMore.Mvc.Persistence.Migrations
{
    public partial class AlterFilds : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Attendances", name: "IX_Event_Id", newName: "IX_EventId");
            DropPrimaryKey("dbo.Attendances");
            //RenameColumn(table: "dbo.Attendances", name: "Event_Id", newName: "EventId");
            AddPrimaryKey("dbo.Attendances", new[] { "EventId", "AttendeeId" });
            DropIndex(table: "dbo.Attendances", name: "IX_EventId");
            DropForeignKey("dbo.Attendances", name: "FK_dbo.Attendances_dbo.Events_Event_Id");
            DropColumn("dbo.Attendances", "Event_Id");
            AddForeignKey("dbo.Attendances", "EventId", "dbo.Events", "Id", cascadeDelete: false);
        }

        public override void Down()
        {
            AddColumn("dbo.Attendances", "EventId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Attendances");
            AddPrimaryKey("dbo.Attendances", new[] { "EventId", "AttendeeId" });
            RenameIndex(table: "dbo.Attendances", name: "IX_EventId", newName: "IX_Event_Id");
            RenameColumn(table: "dbo.Attendances", name: "EventId", newName: "Event_Id");
        }
    }
}
