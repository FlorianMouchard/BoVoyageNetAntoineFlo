namespace BoVoyageNetAntoineFlo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgenceVoyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AssuranceAnnulations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Civilite = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Continent = c.String(),
                        Pays = c.String(),
                        Region = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DossierReservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(nullable: false),
                        PrixTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EtatDossier = c.Int(nullable: false),
                        VoyageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Voyages", t => t.VoyageID, cascadeDelete: true)
                .Index(t => t.VoyageID);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateAller = c.DateTime(nullable: false),
                        DateRetour = c.DateTime(nullable: false),
                        PlacesDisponibles = c.Int(nullable: false),
                        TarifToutCompris = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DestinationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: true)
                .Index(t => t.DestinationID);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reduction = c.Single(nullable: false),
                        Civilite = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        Prenom = c.String(nullable: false),
                        Adresse = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DossierReservations", "VoyageID", "dbo.Voyages");
            DropForeignKey("dbo.Voyages", "DestinationID", "dbo.Destinations");
            DropIndex("dbo.Voyages", new[] { "DestinationID" });
            DropIndex("dbo.DossierReservations", new[] { "VoyageID" });
            DropTable("dbo.Participants");
            DropTable("dbo.Voyages");
            DropTable("dbo.DossierReservations");
            DropTable("dbo.Destinations");
            DropTable("dbo.Clients");
            DropTable("dbo.AssuranceAnnulations");
            DropTable("dbo.AgenceVoyages");
        }
    }
}
