namespace ECommerce.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AccessCode", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Code", c => c.String());
            AddColumn("dbo.Users", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "VerificationCode", c => c.String());
            AddColumn("dbo.Users", "Mobile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Mobile");
            DropColumn("dbo.Users", "VerificationCode");
            DropColumn("dbo.Users", "IsVerified");
            DropColumn("dbo.Users", "ClientId");
            DropColumn("dbo.Users", "Code");
            DropColumn("dbo.Users", "AccessCode");
        }
    }
}
