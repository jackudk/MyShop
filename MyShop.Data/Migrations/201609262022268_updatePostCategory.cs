namespace MyShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePostCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "HomeFlag", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostCategories", "HomeFlag");
        }
    }
}
