namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "User_Id" });
            DropColumn("dbo.Gigs", "UserId");
            RenameColumn(table: "dbo.Gigs", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Gigs", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Gigs", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Gigs", "UserId");
            AddForeignKey("dbo.Gigs", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "UserId" });
            AlterColumn("dbo.Gigs", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Gigs", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Gigs", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Gigs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Gigs", "User_Id");
            AddForeignKey("dbo.Gigs", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
