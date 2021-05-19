using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JewelryStore.Desktop.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Insertion> Insertions { get; set; }
        public virtual DbSet<Metal> Metals { get; set; }
        public virtual DbSet<Prodgroup> Prodgroups { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsSale> Productssales { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=root;database=jewerly_store_app");
                optionsBuilder.UseLazyLoadingProxies();
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Insertion>(entity =>
            {
                entity.ToTable("insertions");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.GemCategory)
                    .HasMaxLength(20)
                    .HasColumnName("gem_category")
                    .IsFixedLength(true);

                entity.Property(e => e.InsertColor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("insert_color")
                    .IsFixedLength(true);

                entity.Property(e => e.InsertName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("insert_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Metal>(entity =>
            {
                entity.ToTable("metals");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.MetalName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("metal_name")
                    .IsFixedLength(true);

                entity.Property(e => e.Sample).HasColumnName("sample");
            });

            modelBuilder.Entity<Prodgroup>(entity =>
            {
                entity.ToTable("prodgroups");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ProdGroupName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("prod_group_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.IdIns, "fk_insert");

                entity.HasIndex(e => e.IdMet, "fk_met");

                entity.HasIndex(e => e.IdProdGr, "fk_prod_group");

                entity.HasIndex(e => e.IdSupp, "fk_supp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArrivalDate)
                    .HasColumnType("date")
                    .HasColumnName("arrival_date");

                entity.Property(e => e.BarCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasColumnName("bar_code")
                    .IsFixedLength(true);

                entity.Property(e => e.ClearWeight).HasColumnName("clear_weight");

                entity.Property(e => e.Faceting)
                    .HasMaxLength(20)
                    .HasColumnName("faceting")
                    .IsFixedLength(true);

                entity.Property(e => e.IdIns).HasColumnName("id_ins");

                entity.Property(e => e.IdMet).HasColumnName("id_met");

                entity.Property(e => e.IdProdGr).HasColumnName("id_prod_gr");

                entity.Property(e => e.IdSupp).HasColumnName("id_supp");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.PriceForTheWork).HasColumnName("price_for_the_work");

                entity.Property(e => e.ProdItem)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("prod_item")
                    .IsFixedLength(true);

                entity.Property(e => e.ProdSize).HasColumnName("prod_size");

                entity.Property(e => e.ProdType)
                    .HasMaxLength(20)
                    .HasColumnName("prod_type")
                    .IsFixedLength(true);

                entity.Property(e => e.WeaveType)
                    .HasMaxLength(20)
                    .HasColumnName("weave_type")
                    .IsFixedLength(true);

                entity.Property(e => e.WeaveWay)
                    .HasMaxLength(20)
                    .HasColumnName("weave_way")
                    .IsFixedLength(true);

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.Property(e => e.IsSold).HasColumnName("is_sold");

                entity.HasOne(d => d.IdInsNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdIns)
                    .HasConstraintName("fk_insert");

                entity.HasOne(d => d.IdMetNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdMet)
                    .HasConstraintName("fk_met");

                entity.HasOne(d => d.IdProdGrNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdProdGr)
                    .HasConstraintName("fk_prod_group");

                entity.HasOne(d => d.IdSuppNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdSupp)
                    .HasConstraintName("fk_supp");
            });

            modelBuilder.Entity<ProductsSale>(entity =>
            {
                entity.ToTable("productssales");

                entity.HasIndex(e => e.IdProd, "fk_id_prod");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdProd).HasColumnName("id_prod");

                entity.Property(e => e.SaleDate)
                    .HasColumnType("date")
                    .HasColumnName("sale_date");

                entity.HasOne(d => d.IdProdNavigation)
                    .WithMany(p => p.Productssales)
                    .HasForeignKey(d => d.IdProd)
                    .HasConstraintName("fk_id_prod");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("suppliers");

                entity.HasIndex(e => e.Suplname, "suplname")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Suplname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("suplname")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
