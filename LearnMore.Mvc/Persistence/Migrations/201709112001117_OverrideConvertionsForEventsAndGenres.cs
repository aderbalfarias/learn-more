using System.Data.Entity.Migrations;

namespace LearnMore.Mvc.Persistence.Migrations
{
    public partial class OverrideConvertionsForEventsAndGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Events", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            DropIndex("dbo.Events", new[] { "Owner_Id" });
            AlterColumn("dbo.Events", "Venue", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Events", "Genre_Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Events", "Owner_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Events", "Genre_Id");
            CreateIndex("dbo.Events", "Owner_Id");
            AddForeignKey("dbo.Events", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Owner_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Events", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Events", new[] { "Owner_Id" });
            DropIndex("dbo.Events", new[] { "Genre_Id" });
            AlterColumn("dbo.Genres", "Name", c => c.String());
            AlterColumn("dbo.Events", "Owner_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Events", "Genre_Id", c => c.Byte());
            AlterColumn("dbo.Events", "Venue", c => c.String());
            CreateIndex("dbo.Events", "Owner_Id");
            CreateIndex("dbo.Events", "Genre_Id");
            AddForeignKey("dbo.Events", "Owner_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Events", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
