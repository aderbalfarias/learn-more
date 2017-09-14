namespace LearnMore.Mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterFilds : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Attendances", name: "Event_Id", newName: "EventId");
            RenameIndex(table: "dbo.Attendances", name: "IX_Event_Id", newName: "IX_EventId");
            DropPrimaryKey("dbo.Attendances");
            AddPrimaryKey("dbo.Attendances", new[] { "EventId", "AttendeeId" });
            DropColumn("dbo.Attendances", "GigId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "GigId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Attendances");
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            RenameIndex(table: "dbo.Attendances", name: "IX_EventId", newName: "IX_Event_Id");
            RenameColumn(table: "dbo.Attendances", name: "EventId", newName: "Event_Id");
        }
    }
}
