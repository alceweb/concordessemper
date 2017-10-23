namespace ConcordesSemper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class incarichi1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Incarichis",
                c => new
                    {
                        Incarico_Id = c.Int(nullable: false, identity: true),
                        Incarico = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Incarico_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Incarichis", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Incarichis", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Incarichis");
        }
    }
}
