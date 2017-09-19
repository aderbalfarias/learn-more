using System.Data.Entity.Migrations;

namespace LearnMore.Mvc.Persistence.Migrations
{
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres (Id, Name) values (1, 'Art')");
            Sql("insert into Genres (Id, Name) values (2, 'Sport')");
            Sql("insert into Genres (Id, Name) values (3, 'Technology')");
            Sql("insert into Genres (Id, Name) values (4, 'Culture')");
            Sql("insert into Genres (Id, Name) values (5, 'Science')");
        }

        public override void Down()
        {
            Sql("delete from Genres whete Id in (1, 2, 3, 4, 5)");
        }
    }
}
