namespace MyShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddASWColumnsToOrderAndSlide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Orders", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Slides", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Slides", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Slides", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.Slides", "UpdatedDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Status", c => c.String());
            DropColumn("dbo.Slides", "UpdatedDate");
            DropColumn("dbo.Slides", "UpdatedBy");
            DropColumn("dbo.Slides", "CreatedDate");
            DropColumn("dbo.Slides", "CreatedBy");
            DropColumn("dbo.Orders", "UpdatedDate");
            DropColumn("dbo.Orders", "UpdatedBy");
        }
    }
}
