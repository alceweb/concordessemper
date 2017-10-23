namespace ConcordesSemper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quesiti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quesitis",
                c => new
                    {
                        Quesito_Id = c.Int(nullable: false, identity: true),
                        Prof = c.String(),
                        Valore = c.Int(nullable: false),
                        Domanda_1 = c.String(nullable: false),
                        Risposta_1 = c.String(nullable: false),
                        Domanda_2 = c.String(nullable: false),
                        Risposta_2 = c.String(nullable: false),
                        Domanda_3 = c.String(nullable: false),
                        Risposta_3 = c.String(nullable: false),
                        Domanda_4 = c.String(nullable: false),
                        Risposta_4 = c.String(nullable: false),
                        Domanda_5 = c.String(nullable: false),
                        Risposta_5 = c.String(nullable: false),
                        ParolaChiave = c.String(),
                        Pubblica = c.Boolean(nullable: false),
                        DataI = c.DateTime(nullable: false),
                        DataF = c.DateTime(nullable: false),
                        Casa_Id = c.Int(nullable: false),
                        Cognome = c.String(),
                        NomeAlunno = c.String(),
                    })
                .PrimaryKey(t => t.Quesito_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quesitis");
        }
    }
}
