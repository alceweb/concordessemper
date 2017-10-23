namespace ConcordesSemper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alunni : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunnis",
                c => new
                    {
                        Alunno_Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cognome = c.String(),
                        Casa_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Alunno_Id)
                .ForeignKey("dbo.Cases", t => t.Casa_Id, cascadeDelete: true)
                .Index(t => t.Casa_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alunnis", "Casa_Id", "dbo.Cases");
            DropIndex("dbo.Alunnis", new[] { "Casa_Id" });
            DropTable("dbo.Alunnis");
        }
    }
}
