using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Models;

public partial class ResourceBookingDbContext : DbContext
{
    public ResourceBookingDbContext()
    {
    }

    public ResourceBookingDbContext(DbContextOptions<ResourceBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WISEMAN;Database=ResourceBookingDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC072CEF6464");

            entity.Property(e => e.BookedBy).HasMaxLength(100);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Purpose).HasMaxLength(200);
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Resource).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ResourceId)
                .HasConstraintName("FK_Bookings_Resources");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resource__3214EC0781970BF4");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
