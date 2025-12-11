using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyNews.Models;

public partial class MyNewsContext : DbContext
{
    public MyNewsContext()
    {
    }

    public MyNewsContext(DbContextOptions<MyNewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCategory> TblCategory { get; set; }

    public virtual DbSet<TblComment> TblComment { get; set; }

    public virtual DbSet<TblKeyword> TblKeyword { get; set; }

    public virtual DbSet<TblNews> TblNews { get; set; }

    public virtual DbSet<TblRole> TblRole { get; set; }

    public virtual DbSet<TblUser> TblUser { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=News;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblComment>(entity =>
        {
            entity.Property(e => e.DateSubmited).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.News).WithMany(p => p.TblComment).HasConstraintName("FK_TblComment_TblNews");
        });

        modelBuilder.Entity<TblNews>(entity =>
        {
            entity.Property(e => e.DatePublished).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.TblNews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblNews_TblCategory");

            entity.HasMany(d => d.Keyword).WithMany(p => p.News)
                .UsingEntity<Dictionary<string, object>>(
                    "TblNewsKeywordRel",
                    r => r.HasOne<TblKeyword>().WithMany()
                        .HasForeignKey("KeywordId")
                        .HasConstraintName("FK_TblNewsKeywordRel_TblKeyword"),
                    l => l.HasOne<TblNews>().WithMany()
                        .HasForeignKey("NewsId")
                        .HasConstraintName("FK_TblNewsKeywordRel_TblNews"),
                    j =>
                    {
                        j.HasKey("NewsId", "KeywordId");
                        j.IndexerProperty<int>("NewsId").HasColumnName("NewsID");
                        j.IndexerProperty<int>("KeywordId").HasColumnName("KeywordID");
                    });
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasOne(d => d.Role).WithMany(p => p.TblUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUser_TblRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
