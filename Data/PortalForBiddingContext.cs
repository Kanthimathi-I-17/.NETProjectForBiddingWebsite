using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PortalForBiddingDB.Models;

namespace PortalForBiddingDB.Data;

public partial class PortalForBiddingContext : DbContext
{
    public PortalForBiddingContext(DbContextOptions<PortalForBiddingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Consumer> Consumers { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Consumer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("consumer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BiddedPrice)
                .HasPrecision(5, 2)
                .HasColumnName("bidded_price");
            entity.Property(e => e.ConsumerMailId)
                .HasMaxLength(100)
                .HasColumnName("consumer_mail_id");
            entity.Property(e => e.ConsumerName)
                .HasMaxLength(100)
                .HasColumnName("consumer_name");
            entity.Property(e => e.FarmerId).HasColumnName("farmer_id");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            //entity.HasOne(c => c.Farmer)
            //      .WithMany(f => f.Consumers).HasForeignKey(c => c.FarmerId);
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("farmer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FarmerMailId)
                .HasMaxLength(100)
                .HasColumnName("farmer_mail_id");
            entity.Property(e => e.FarmerName)
                .HasMaxLength(100)
                .HasColumnName("farmer_name");
            entity.Property(e => e.ProductBasePrice)
                .HasPrecision(5, 2)
                .HasColumnName("product_base_price");
            entity.Property(e => e.ProductCategory)
                .HasMaxLength(100)
                .HasColumnName("product_category");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductQuantity)
                .HasPrecision(5, 2)
                .HasColumnName("product_quantity");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
