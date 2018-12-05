namespace blankspaces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IdPersona", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IdPersona");
        }
    }
}
