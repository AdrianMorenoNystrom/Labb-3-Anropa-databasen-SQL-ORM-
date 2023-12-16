using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LABB3.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BetygTabell> BetygTabells { get; set; }

    public virtual DbSet<KlassTabell> KlassTabells { get; set; }

    public virtual DbSet<KursTabell> KursTabells { get; set; }

    public virtual DbSet<Lärare> Lärares { get; set; }

    public virtual DbSet<PersonalTabell> PersonalTabells { get; set; }

    public virtual DbSet<StudentTabell> StudentTabells { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=LABB2 Skola;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BetygTabell>(entity =>
        {
            entity.HasKey(e => e.BetygIdPk).HasName("PK__BetygTab__E90ED048A0D661DE");

            entity.ToTable("BetygTabell");

            entity.Property(e => e.BetygIdPk)
                .ValueGeneratedNever()
                .HasColumnName("BetygIdPK");
            entity.Property(e => e.Betyg).HasMaxLength(5);
            entity.Property(e => e.BetygDatum).HasColumnType("date");
            entity.Property(e => e.KursIdFk).HasColumnName("KursIdFK");
            entity.Property(e => e.LärareIdFk).HasColumnName("LärareIdFK");
            entity.Property(e => e.StudentIdFk).HasColumnName("StudentIdFK");

            entity.HasOne(d => d.KursIdFkNavigation).WithMany(p => p.BetygTabells)
                .HasForeignKey(d => d.KursIdFk)
                .HasConstraintName("FK__BetygTabe__KursI__3A81B327");

            entity.HasOne(d => d.LärareIdFkNavigation).WithMany(p => p.BetygTabells)
                .HasForeignKey(d => d.LärareIdFk)
                .HasConstraintName("FK__BetygTabe__Lärar__3B75D760");

            entity.HasOne(d => d.StudentIdFkNavigation).WithMany(p => p.BetygTabells)
                .HasForeignKey(d => d.StudentIdFk)
                .HasConstraintName("FK__BetygTabe__Stude__398D8EEE");
        });

        modelBuilder.Entity<KlassTabell>(entity =>
        {
            entity.HasKey(e => e.KlassIdPk).HasName("PK__KlassTab__CF47A60D3444AD5C");

            entity.ToTable("KlassTabell");

            entity.Property(e => e.KlassIdPk)
                .ValueGeneratedNever()
                .HasColumnName("KlassIdPK");
            entity.Property(e => e.KlassNamn).HasMaxLength(50);
        });

        modelBuilder.Entity<KursTabell>(entity =>
        {
            entity.HasKey(e => e.KursIdPk).HasName("PK__KursTabe__BCCFFF3BCF61AFC4");

            entity.ToTable("KursTabell");

            entity.Property(e => e.KursIdPk)
                .ValueGeneratedNever()
                .HasColumnName("KursIdPK");
            entity.Property(e => e.KursNamn).HasMaxLength(50);
        });

        modelBuilder.Entity<Lärare>(entity =>
        {
            entity.HasKey(e => e.LärareIdPk);

            entity.ToTable("Lärare");

            entity.Property(e => e.LärareIdPk)
                .ValueGeneratedNever()
                .HasColumnName("LärareIdPK");
            entity.Property(e => e.Lärare1)
                .HasMaxLength(50)
                .HasColumnName("Lärare");
        });

        modelBuilder.Entity<PersonalTabell>(entity =>
        {
            entity.HasKey(e => e.PersonIdPk).HasName("PK__Personal__AA2FFB858259B802");

            entity.ToTable("PersonalTabell");

            entity.Property(e => e.PersonIdPk).HasColumnName("PersonIdPK");
            entity.Property(e => e.Befattning).HasMaxLength(50);
            entity.Property(e => e.EfterNamn).HasMaxLength(50);
            entity.Property(e => e.FörNamn).HasMaxLength(50);
            entity.Property(e => e.LärareIdFk).HasColumnName("LärareIdFK");
            entity.Property(e => e.Personnummer).HasMaxLength(20);

            entity.HasOne(d => d.LärareIdFkNavigation).WithMany(p => p.PersonalTabells)
                .HasForeignKey(d => d.LärareIdFk)
                .HasConstraintName("FK_PersonalTabell_Lärare");
        });

        modelBuilder.Entity<StudentTabell>(entity =>
        {
            entity.HasKey(e => e.StudentIdPk).HasName("PK__StudentT__32C52A798EE2D681");

            entity.ToTable("StudentTabell", tb => tb.HasTrigger("TR_Kön"));

            entity.Property(e => e.StudentIdPk)
                .ValueGeneratedNever()
                .HasColumnName("StudentIdPK");
            entity.Property(e => e.EfterNamn).HasMaxLength(50);
            entity.Property(e => e.FörNamn).HasMaxLength(50);
            entity.Property(e => e.KlassIdFk).HasColumnName("KlassIdFK");
            entity.Property(e => e.Kön)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Personnummer)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.KlassIdFkNavigation).WithMany(p => p.StudentTabells)
                .HasForeignKey(d => d.KlassIdFk)
                .HasConstraintName("FK__StudentTa__Klass__34C8D9D1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
