namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Genres (Name) Values ('Blue')");
            Sql("Insert into Genres (Name) Values ('Rock')");
            Sql("Insert into Genres (Name) Values ('Pop')");
            Sql("Insert into Genres (Name) Values ('YoMusic')");
        }
        
        public override void Down()
        {
            Sql("Delete from Genres");
        }
    }
}
