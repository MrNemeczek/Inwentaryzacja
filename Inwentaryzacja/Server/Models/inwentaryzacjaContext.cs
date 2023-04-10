using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Inwentaryzacja.Shared.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Inwentaryzacja.Server.Models
{
    public partial class inwentaryzacjaContext : DbContext
    {
        public inwentaryzacjaContext()
        {
        }

        public inwentaryzacjaContext(DbContextOptions<inwentaryzacjaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<App> Apps { get; set; } = null!;
        public virtual DbSet<Appkomp> Appkomps { get; set; } = null!;
        public virtual DbSet<Error> Errors { get; set; } = null!;
        public virtual DbSet<Faktury> Faktury { get; set; } = null!;
        public virtual DbSet<Komp> Komps { get; set; } = null!;
        public virtual DbSet<Licencje> Licencje { get; set; } = null!;
        public virtual DbSet<NumeryInwentaryzacyjne> NumeryInwentaryzacyjne { get; set; } = null!;
        public virtual DbSet<NumeryInwentaryzacyjneNew> NumeryInwentaryzacyjneNew { get; set; } = null!;
        public virtual DbSet<Spolki> Spolki { get; set; } = null!;
        public virtual DbSet<TypFaktury> TypFaktury { get; set; } = null!;
        public virtual DbSet<UzyciaLicencji> UzyciaLicencji { get; set; } = null!;
        public virtual DbSet<Oddzialy> Oddzialy { get; set; } = null!;
        public virtual DbSet<Sprzet> Sprzet { get; set; } = null!;
        public virtual DbSet<StanSprzet> StanSprzet { get; set; } = null!;
        public virtual DbSet<TypSprzet> TypSprzet { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseCollation("utf8mb4_general_ci")
            //    .HasCharSet("utf8mb4");
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUsers)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.IdUsers)
                    .HasColumnName("id_users");

                entity.Property(e => e.Lastname)
                    .HasColumnType("text")
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Mail)
                    .HasColumnType("text")
                    .HasColumnName("mail");

                entity.Property(e => e.LastLogged)
                    .HasColumnName("last_logged");
                    //.HasColumnType("datetime")

                entity.Property(e => e.UserName)
                    .HasMaxLength(45)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<App>(entity =>
            {
                entity.HasKey(e => e.IdApp)
                    .HasName("PRIMARY");

                entity.ToTable("app");

                entity.Property(e => e.IdApp)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_app");

                entity.Property(e => e.Blacklist)
                    .HasColumnType("int(11)")
                    .HasColumnName("blacklist");

                entity.Property(e => e.NazwaApp)
                    .HasColumnType("text")
                    .HasColumnName("nazwa_app");

                entity.Property(e => e.Wersja)
                    .HasColumnType("text")
                    .HasColumnName("wersja");
            });

            modelBuilder.Entity<Appkomp>(entity =>
            {
                entity.HasKey(e => e.IdAppkomp)
                    .HasName("PRIMARY");

                entity.ToTable("appkomp");

                entity.HasIndex(e => e.IdApp, "id_app");

                entity.HasIndex(e => e.IdKomp, "id_komp");

                //ignoruj pomocnicze wlasciwosci
                entity.Ignore(e => e.kompNazwaDomena);
                entity.Ignore(e => e.nazwaApp);
                entity.Ignore(e => e.oddzialNazwa);
                entity.Ignore(e => e.ip);
                entity.Ignore(e => e.blacklist);

                entity.Property(e => e.IdAppkomp)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_appkomp");

                entity.Property(e => e.IdApp)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_app");

                entity.Property(e => e.IdKomp)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_komp");

                entity.HasOne(d => d.IdAppNavigation)
                    .WithMany(p => p.Appkomps)
                    .HasForeignKey(d => d.IdApp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appkomp_ibfk_1");

                entity.HasOne(d => d.IdKompNavigation)
                    .WithMany(p => p.Appkomps)
                    .HasForeignKey(d => d.IdKomp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appkomp_ibfk_2");
            });

            modelBuilder.Entity<Error>(entity =>
            {
                entity.ToTable("errors");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ErrorText)
                    .HasColumnType("text")
                    .HasColumnName("error_text");

                entity.Property(e => e.Time)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("time")
                    .HasDefaultValueSql("current_timestamp()");
            });

            modelBuilder.Entity<Faktury>(entity =>
            {
                entity.HasKey(e => e.IdFaktura)
                    .HasName("PRIMARY");

                entity.ToTable("faktury");

                //ignoruj pomocnicze wlasciwosci
                entity.Ignore(e => e.nazwaSpolka);
                entity.Ignore(e => e.nazwaTypFaktury);

                entity.HasIndex(e => e.IdSpolka, "faktury_ibfk_1");

                entity.HasIndex(e => e.IdTypFaktura, "faktury_ibfk_2");

                entity.Property(e => e.IdFaktura)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_faktura");

                entity.Property(e => e.DataFaktura).HasColumnName("data_faktura");

                entity.Property(e => e.IdSpolka)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_spolka");

                entity.Property(e => e.IdTypFaktura)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_typ_faktura");

                entity.Property(e => e.KwotaFaktura).HasColumnName("kwota_faktura");

                entity.Property(e => e.NazwaFaktura)
                    .HasColumnType("text")
                    .HasColumnName("nazwa_faktura");

                entity.Property(e => e.PlikNazwa)
                    .HasColumnType("text")
                    .HasColumnName("plik_nazwa");

                entity.Property(e => e.PlikFaktura).HasColumnName("plik_faktura");

                entity.HasOne(d => d.IdSpolkaNavigation)
                    .WithMany(p => p.Faktury)
                    .HasForeignKey(d => d.IdSpolka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("faktury_ibfk_1");

                entity.HasOne(d => d.IdTypFakturaNavigation)
                    .WithMany(p => p.Fakturies)
                    .HasForeignKey(d => d.IdTypFaktura)
                    .HasConstraintName("faktury_ibfk_2");
            });

            modelBuilder.Entity<StanSprzet>(entity =>
            {
                entity.HasKey(e => e.IdStan)
                    .HasName("PRIMARY");

                entity.ToTable("stan_sprzet");

                entity.Property(e => e.IdStan).HasColumnName("id_stan");

                entity.Property(e => e.Stan)
                    .HasMaxLength(45)
                    .HasColumnName("stan");
            });

            modelBuilder.Entity<TypSprzet>(entity =>
            {
                entity.HasKey(e => e.IdTypSprzet)
                    .HasName("PRIMARY");

                entity.ToTable("typ_sprzet");

                entity.Property(e => e.IdTypSprzet).HasColumnName("id_typ_sprzet");

                entity.Property(e => e.Typ)
                    .HasMaxLength(45)
                    .HasColumnName("typ");
            });

            modelBuilder.Entity<Sprzet>(entity =>
            {
                entity.HasKey(e => e.IdSprzet)
                    .HasName("PRIMARY");

                entity.ToTable("sprzet");

                //ignoruj pomocnicze wlasciwosci modelu
                entity.Ignore(e => e.nazwaFaktura);
                entity.Ignore(e => e.oddzialNazwa);
                entity.Ignore(e => e.typ);
                entity.Ignore(e => e.stan);
                entity.Ignore(e => e.numer);
                entity.Ignore(e => e.numerNew);
                entity.Ignore(e => e.nazwaSpolka);

                entity.HasIndex(e => e.IdFaktura, "id_faktura_idx");

                entity.HasIndex(e => e.IdOddzialy, "id_oddzialy_idx");

                entity.HasIndex(e => e.IdSpolka, "id_spolka_idx");

                entity.HasIndex(e => e.IdStan, "id_stan_idx");

                entity.HasIndex(e => e.IdTypSprzet, "id_typ_sprzet_idx");

                entity.Property(e => e.IdSprzet).HasColumnName("id_sprzet");

                entity.Property(e => e.IdFaktura).HasColumnName("id_faktura");

                entity.Property(e => e.IdOddzialy).HasColumnName("id_oddzialy");

                entity.Property(e => e.IdSpolka).HasColumnName("id_spolka");

                entity.Property(e => e.IdStan).HasColumnName("id_stan");

                entity.Property(e => e.IdTypSprzet).HasColumnName("id_typ_sprzet");

                entity.Property(e => e.IdNrInwentaryzacyjny).HasColumnName("id_nr_inwentaryzacyjny");

                entity.Property(e => e.IdNrInwentaryzacyjnyNew).HasColumnName("id_nr_inwentaryzacyjny_new");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.Model)
                    .HasColumnType("text")
                    .HasColumnName("model");

                entity.Property(e => e.Marka)
                    .HasColumnName("marka");

                entity.Property(e => e.NazwaSprzet)
                    .HasColumnType("text")
                    .HasColumnName("nazwa_sprzet");

                entity.Property(e => e.NumerSeryjny)
                    .HasMaxLength(45)
                    .HasColumnName("numer_seryjny");

                entity.HasOne(d => d.IdFakturaNavigation)
                    .WithMany(p => p.Sprzet)
                    .HasForeignKey(d => d.IdFaktura)
                    .HasConstraintName("id_faktura");

                entity.HasOne(d => d.IdOddzialyNavigation)
                    .WithMany(p => p.Sprzet)
                    .HasForeignKey(d => d.IdOddzialy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_oddzialy");

                entity.HasOne(d => d.IdSpolkaNavigation)
                    .WithMany(p => p.Sprzets)
                    .HasForeignKey(d => d.IdSpolka)
                    .HasConstraintName("id_spolka");

                entity.HasOne(d => d.IdStanNavigation)
                    .WithMany(p => p.Sprzet)
                    .HasForeignKey(d => d.IdStan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_stan");

                entity.HasOne(d => d.IdTypSprzetNavigation)
                    .WithMany(p => p.Sprzet)
                    .HasForeignKey(d => d.IdTypSprzet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_typ_sprzet");

                entity.HasOne(d => d.IdNrInwentaryzacyjnyNavigation)
                    .WithMany(p => p.Sprzety)
                    .HasForeignKey(d => d.IdNrInwentaryzacyjny)
                    .HasConstraintName("id_nr_inwentaryzacyjny");

                entity.HasOne(d => d.IdNrInwentaryzacyjnyNewNavigation)
                    .WithMany(p => p.Sprzety)
                    .HasForeignKey(d => d.IdNrInwentaryzacyjnyNew)
                    .HasConstraintName("id_nr_inwentaryzacyjny_new");
            });

            modelBuilder.Entity<Oddzialy>(entity =>
            {
                entity.HasKey(e => e.IdOddzialy)
                    .HasName("PRIMARY");

                entity.ToTable("oddzialy");

                entity.Property(e => e.IdOddzialy).HasColumnName("id_oddzialy");

                entity.Property(e => e.OddzialNazwa)
                    .HasMaxLength(45)
                    .HasColumnName("oddzial_nazwa");
            });

            modelBuilder.Entity<Komp>(entity =>
            {
                entity.HasKey(e => e.IdKomp)
                    .HasName("PRIMARY");

                entity.ToTable("komp");

                //ignoruj wlasciwosci w modelu
                entity.Ignore(e => e.nazwaSprzet);
                entity.Ignore(e => e.oddzialNazwa);

                entity.HasIndex(e => e.IdSprzet, "id_sprzet_idx");

                entity.Property(e => e.IdKomp).HasColumnName("id_komp");

                entity.Property(e => e.IdSprzet).HasColumnName("id_sprzet");

                entity.Property(e => e.Ip).HasColumnType("text").HasColumnName("ip");

                entity.Property(e => e.KompNazwaDomena).HasColumnType("text").HasColumnName("komp_nazwaDomena");

                entity.Property(e => e.ZrzutCzas).HasColumnName("zrzut_czas");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IdOddzial).HasColumnName("id_oddzial");
                entity.HasIndex(e => e.IdOddzial, "komp_ibfk_4_idx");

                entity.HasOne(d => d.IdSprzetNavigation)
                    .WithMany(p => p.Komps)
                    .HasForeignKey(d => d.IdSprzet)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("komp_ibfk_2");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Komps)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("komp_ibfk_3");

                entity.HasOne(d => d.IdOddzialNavigation)
                    .WithMany(p => p.Komps)
                    .HasForeignKey(d => d.IdOddzial)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("komp_ibfk_4");
            });

            modelBuilder.Entity<Licencje>(entity =>
            {
                entity.HasKey(e => e.IdLic)
                    .HasName("PRIMARY");

                entity.ToTable("licencje");

                //ignoruj pomocnicze zmienne
                entity.Ignore(e => e.nazwaFaktura);

                entity.HasIndex(e => e.IdFaktura, "id_faktura");

                entity.Property(e => e.IdLic)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_lic");

                entity.Property(e => e.DataWygLic).HasColumnName("data_wyg_lic");

                entity.Property(e => e.IdFaktura)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_faktura");

                entity.Property(e => e.Klucz)
                    .HasColumnType("text")
                    .HasColumnName("klucz");

                entity.Property(e => e.LiczbaLicencji)
                    .HasColumnType("int(11)")
                    .HasColumnName("liczba_licencji");

                entity.Property(e => e.Nazwa)
                    .HasColumnType("text")
                    .HasColumnName("nazwa");

                entity.HasOne(d => d.IdFakturaNavigation)
                    .WithMany(p => p.Licencjes)                    
                    .HasForeignKey(d => d.IdFaktura)
                    .HasConstraintName("licencje_ibfk_1");

            });

            modelBuilder.Entity<NumeryInwentaryzacyjne>(entity =>
            {
                entity.HasKey(e => e.IdNrInwentaryzacyjny)
                    .HasName("PRIMARY");

                entity.ToTable("numery_inwentaryzacyjne");

                entity.Property(e => e.IdNrInwentaryzacyjny).HasColumnName("id_nr_inwentaryzacyjny");

                entity.Property(e => e.Numer)
                    .HasColumnType("text")
                    .HasColumnName("numer");

            });

            modelBuilder.Entity<NumeryInwentaryzacyjneNew>(entity =>
            {
                entity.HasKey(e => e.IdNrInwentaryzacyjny)
                    .HasName("PRIMARY");

                entity.ToTable("numery_inw_new_system");

                //ignorowane wlasciwosci pomocnicze
                entity.Ignore(e => e.nazwaSpolka);

                entity.Property(e => e.IdNrInwentaryzacyjny).HasColumnName("id");

                entity.Property(e => e.NumerNew)
                    .HasColumnType("text")
                    .HasColumnName("numer");

                entity.Property(e => e.IdSpolka)
                    .HasColumnName("id_spolka");

                entity.HasOne(d => d.IdSpolkaNavigation)
                    .WithMany(p => p.NumeryInwentaryzacyjneNews)
                    .HasForeignKey(d => d.IdSpolka)
                    .HasConstraintName("id_spolka");

            });

            modelBuilder.Entity<Spolki>(entity =>
            {
                entity.HasKey(e => e.IdSpolka)
                    .HasName("PRIMARY");

                entity.ToTable("spolki");

                entity.Property(e => e.IdSpolka)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_spolka");

                entity.Property(e => e.NazwaSpolka)
                    .HasColumnType("text")
                    .HasColumnName("nazwa_spolka");

                entity.Property(e => e.NIP)
                    .HasColumnType("text")
                    .HasColumnName("NIP");

                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol");
            });

            modelBuilder.Entity<TypFaktury>(entity =>
            {
                entity.HasKey(e => e.IdTypFaktury)
                    .HasName("PRIMARY");

                entity.ToTable("typ_faktury");

                entity.Property(e => e.IdTypFaktury)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_typ_faktury");

                entity.Property(e => e.NazwaTypFaktury)
                    .HasColumnType("text")
                    .HasColumnName("nazwa_typ_faktury");
            });

            modelBuilder.Entity<UzyciaLicencji>(entity =>
            {
                entity.HasKey(e => e.IdUzyciaLicencji)
                    .HasName("PRIMARY");

                entity.ToTable("uzycia_licencji");

                entity.HasIndex(e => e.IdKomp, "id_komp");

                entity.HasIndex(e => e.IdLic, "id_lic");

                //ignoruj pomocnicze wlasciwosci
                entity.Ignore(e => e.kompNazwaDomena);

                entity.Property(e => e.IdUzyciaLicencji)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_uzycia_licencji");

                entity.Property(e => e.IdKomp)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_komp");

                entity.Property(e => e.IdLic)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_lic");

                entity.HasOne(d => d.IdKompNavigation)
                    .WithMany(p => p.UzyciaLicencji)
                    .HasForeignKey(d => d.IdKomp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("uzycia_licencji_ibfk_2");

                entity.HasOne(d => d.IdLicNavigation)
                    .WithMany(p => p.UzyciaLicencji)
                    .HasForeignKey(d => d.IdLic)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("uzycia_licencji_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
