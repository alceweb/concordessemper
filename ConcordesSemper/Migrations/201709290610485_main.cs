namespace ConcordesSemper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class main : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Casa_Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Casa_Id);
            
            CreateTable(
                "dbo.Punteggis",
                c => new
                    {
                        Punteggio_Id = c.Int(nullable: false, identity: true),
                        Casa_Id = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Punti = c.Int(nullable: false),
                        Motivazione = c.String(),
                        Comportamento = c.Int(nullable: false),
                        Dimenticanze = c.Int(nullable: false),
                        Varie = c.Int(nullable: false),
                        GrandiG = c.Int(nullable: false),
                        OeP = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Punteggio_Id)
                .ForeignKey("dbo.Cases", t => t.Casa_Id, cascadeDelete: true)
                .Index(t => t.Casa_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Punteggis", "Casa_Id", "dbo.Cases");
            DropIndex("dbo.Punteggis", new[] { "Casa_Id" });
            DropTable("dbo.Punteggis");
            DropTable("dbo.Cases");
        }
    }
}
