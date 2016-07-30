namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeGigsPropertyNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Gigs", name: "UserId", newName: "ArtistId");
            RenameIndex(table: "dbo.Gigs", name: "IX_UserId", newName: "IX_ArtistId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Gigs", name: "IX_ArtistId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Gigs", name: "ArtistId", newName: "UserId");
        }
    }
}
