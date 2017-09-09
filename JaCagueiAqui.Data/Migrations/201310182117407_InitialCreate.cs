namespace JaCagueiAqui.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Comentario = c.String(nullable: false),
                        Lat = c.Decimal(nullable: false, precision: 11, scale: 8),
                        Long = c.Decimal(nullable: false, precision: 11, scale: 8),
                        Gostei = c.Int(nullable: false),
                        NaoGostei = c.Int(nullable: false),
                        Cidade_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidades", t => t.Cidade_ID)
                .Index(t => t.Cidade_ID);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contatoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Mensagem = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                        Cidade_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidades", t => t.Cidade_ID)
                .Index(t => t.Cidade_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comentarios", new[] { "Cidade_ID" });
            DropIndex("dbo.Posts", new[] { "Cidade_ID" });
            DropForeignKey("dbo.Comentarios", "Cidade_ID", "dbo.Cidades");
            DropForeignKey("dbo.Posts", "Cidade_ID", "dbo.Cidades");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Contatoes");
            DropTable("dbo.Cidades");
            DropTable("dbo.Posts");
        }
    }
}
