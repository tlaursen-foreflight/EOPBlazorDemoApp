using System;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class DatabaseContext : DbContext
    {
        //Todo probably not the way to do this
        private static string _username;
        private static string _password;

        public static void SetCredentials(string username, string password)
        {
            _password = password;
            _username = username;
        }
        
        
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //todo move this to app settings
            var connectionString = $"Server=dispatch-rds-qa.acqa.foreflight.com;Port=5432;Database=eopmaster;User Id={_username};Password={_password};";
            optionsBuilder.UseNpgsql(connectionString);
        }

        

        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<AnalysisRun> AnalysisRuns { get; set; }
        public virtual DbSet<EngineOutProcedure> EngineOutProcedures { get; set; }
        public virtual DbSet<JeppesenProcedure> JeppesenProcedures { get; set; }
        public virtual DbSet<ManualNotam> ManualNotams { get; set; }
        public virtual DbSet<Runway> Runways { get; set; }
        public virtual DbSet<StraightOut> StraightOuts { get; set; }
        public virtual DbSet<StraightOutAnalysisRun> StraightOutAnalysisRuns { get; set; }
        public virtual DbSet<UnparsedNotam> UnparsedNotams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("Airport");

                entity.Property(e => e.Fir).HasColumnName("FIR");

                entity.Property(e => e.Iata).HasColumnName("IATA");

                entity.Property(e => e.Icao).HasColumnName("ICAO");
            });

            modelBuilder.Entity<AnalysisRun>(entity =>
            {
                entity.ToTable("AnalysisRun");

                entity.HasIndex(e => e.ProcedureId, "IX_AnalysisRun_ProcedureId");

                entity.Property(e => e.AnalysedOn).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.NavDbdataSet)
                    .IsRequired()
                    .HasColumnName("NavDBDataSet");

                entity.Property(e => e.ObstacleDbdataSet)
                    .IsRequired()
                    .HasColumnName("ObstacleDBDataSet");

                entity.Property(e => e.Results).IsRequired();

                entity.HasOne(d => d.Procedure)
                    .WithMany(p => p.AnalysisRuns)
                    .HasForeignKey(d => d.ProcedureId);
            });

            modelBuilder.Entity<EngineOutProcedure>(entity =>
            {
                entity.HasKey(e => e.ProcedureId);

                entity.ToTable("EngineOutProcedure");

                entity.HasIndex(e => e.RunwayId, "IX_EngineOutProcedure_RunwayId");

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Eopclass)
                    .IsRequired()
                    .HasColumnName("EOPClass");

                entity.Property(e => e.LastEditedOn).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Runway)
                    .WithMany(p => p.EngineOutProcedures)
                    .HasForeignKey(d => d.RunwayId);
            });

            modelBuilder.Entity<JeppesenProcedure>(entity =>
            {
                entity.HasKey(e => e.ProcedureId);

                entity.ToTable("JeppesenProcedure");

                entity.HasIndex(e => e.RunwayId, "IX_JeppesenProcedure_RunwayId");

                entity.HasOne(d => d.Runway)
                    .WithMany(p => p.JeppesenProcedures)
                    .HasForeignKey(d => d.RunwayId);
            });

            modelBuilder.Entity<ManualNotam>(entity =>
            {
                entity.HasKey(e => e.NotamId);

                entity.ToTable("ManualNotam");

                entity.HasIndex(e => e.UnparsedNotamId, "IX_ManualNotam_UnparsedNotamId")
                    .IsUnique();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.HasOne(d => d.UnparsedNotam)
                    .WithOne(p => p.ManualNotam)
                    .HasForeignKey<ManualNotam>(d => d.UnparsedNotamId);
            });

            modelBuilder.Entity<Runway>(entity =>
            {
                entity.ToTable("Runway");

                entity.HasIndex(e => e.AirportId, "IX_Runway_AirportId");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.Runways)
                    .HasForeignKey(d => d.AirportId);
            });

            modelBuilder.Entity<StraightOut>(entity =>
            {
                entity.ToTable("StraightOut");

                entity.HasIndex(e => e.RunwayId, "IX_StraightOut_RunwayId");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Runway)
                    .WithMany(p => p.StraightOuts)
                    .HasForeignKey(d => d.RunwayId);
            });

            modelBuilder.Entity<StraightOutAnalysisRun>(entity =>
            {
                entity.ToTable("StraightOutAnalysisRun");

                entity.HasIndex(e => e.StraightOutId, "IX_StraightOutAnalysisRun_StraightOutId");

                entity.Property(e => e.NavDbdataSet)
                    .IsRequired()
                    .HasColumnName("NavDBDataSet");

                entity.Property(e => e.Results).IsRequired();

                entity.HasOne(d => d.StraightOut)
                    .WithMany(p => p.StraightOutAnalysisRuns)
                    .HasForeignKey(d => d.StraightOutId);
            });

            modelBuilder.Entity<UnparsedNotam>(entity =>
            {
                entity.ToTable("UnparsedNotam");

                entity.Property(e => e.RawNotamText).IsRequired();
            });

        }

    }
}