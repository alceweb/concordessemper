namespace ConcordesSemper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incarichi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Casa_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Incarico", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Incarico");
            DropColumn("dbo.AspNetUsers", "Casa_Id");
        }
    }
}
