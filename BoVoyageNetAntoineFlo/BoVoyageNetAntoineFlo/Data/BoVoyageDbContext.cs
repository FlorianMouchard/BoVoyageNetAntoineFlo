using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BoVoyageNetAntoineFlo.Models;

namespace BoVoyageNetAntoineFlo.Data
{
    public class BoVoyageDbContext : DbContext
    {
        public BoVoyageDbContext() : base("BoVoyageAntoineFlo")
        {
        }

        public DbSet<AgenceVoyage> AgencesVoyages { get; set; }       

        public DbSet<AssuranceAnnulation> AssurancesAnnulations { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<DossierReservation> DossiersReservations { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Voyage> Voyages { get; set; }
    }
}