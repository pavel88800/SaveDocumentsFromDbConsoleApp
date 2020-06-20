using Microsoft.EntityFrameworkCore;

namespace DocumentsFromDb.Models
{
    public partial class ContractDocumentContext : DbContext
    {
        public ContractDocumentContext()
        {
        }

        public ContractDocumentContext(DbContextOptions<ContractDocumentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractFolder> ContractFolder { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<FolderDocument> FolderDocument { get; set; }
        public virtual DbSet<Folders> Folders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=ContractDocument;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<ContractFolder>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.IdContract)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdFolders)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdContractNavigation)
                    .WithMany(p => p.ContractFolder)
                    .HasForeignKey(d => d.IdContract)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFolder_Contract");

                entity.HasOne(d => d.IdFoldersNavigation)
                    .WithMany(p => p.ContractFolder)
                    .HasForeignKey(d => d.IdFolders)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractFolder_Folders");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Extension)
                    .HasColumnName("extension")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<FolderDocument>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.IdContractFolders)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdDocument)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdContractFoldersNavigation)
                    .WithMany(p => p.FolderDocument)
                    .HasForeignKey(d => d.IdContractFolders)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FolderDocument_ContractFolder");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany(p => p.FolderDocument)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FolderDocument_Document");
            });

            modelBuilder.Entity<Folders>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
