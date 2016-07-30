namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToGigModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "Artist_Id" });
            RenameColumn(table: "dbo.Gigs", name: "Genre_Id", newName: "GenreId");
            RenameColumn(table: "dbo.Gigs", name: "Artist_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Gigs", name: "IX_Genre_Id", newName: "IX_GenreId");
            AddColumn("dbo.Gigs", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Gigs", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Gigs", "User_Id");
            AddForeignKey("dbo.Gigs", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "User_Id" });
            AlterColumn("dbo.Gigs", "User_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Gigs", "UserId");
            RenameIndex(table: "dbo.Gigs", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameColumn(table: "dbo.Gigs", name: "User_Id", newName: "Artist_Id");
            RenameColumn(table: "dbo.Gigs", name: "GenreId", newName: "Genre_Id");
            CreateIndex("dbo.Gigs", "Artist_Id");
            AddForeignKey("dbo.Gigs", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
