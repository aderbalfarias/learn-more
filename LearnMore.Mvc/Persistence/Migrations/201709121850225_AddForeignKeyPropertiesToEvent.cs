using System.Data.Entity.Migrations;

namespace LearnMore.Mvc.Persistence.Migrations
{
    public partial class AddForeignKeyPropertiesToEvent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Genre_Id", newName: "GenreId");
            RenameColumn(table: "dbo.Events", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.Events", name: "IX_Owner_Id", newName: "IX_OwnerId");
            RenameIndex(table: "dbo.Events", name: "IX_Genre_Id", newName: "IX_GenreId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameIndex(table: "dbo.Events", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.Events", name: "OwnerId", newName: "Owner_Id");
            RenameColumn(table: "dbo.Events", name: "GenreId", newName: "Genre_Id");
        }
    }
}
