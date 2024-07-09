namespace ObdLogApi.Models
{
    using Microsoft.EntityFrameworkCore;

    public partial class ObdDb : DbContext
    {
        public ObdDb()
        {
        }

        public ObdDb(DbContextOptions<ObdDb> options)
            : base(options)
        {
        }

        public virtual DbSet<ObdData> ObdData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=10.0.0.247;port=3306;user=root;password=[3wer4e];database=mike;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<ObdData>(entity =>
            {
                entity.HasKey(e => e.VinSerial);

                entity.ToTable("ObdData", "mike");

                entity.Property(e => e.VinSerial)
                    .HasColumnName("Vin_Serial")
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AbsLoadPercentage).HasColumnName("AbsLoad_percentage");

                entity.Property(e => e.CoolantTempCels).HasColumnType("int(11)");

                entity.Property(e => e.EngLoadPercentage).HasColumnName("EngLoad_percentage");

                entity.Property(e => e.Rpm).HasColumnType("int(11)");

                entity.Property(e => e.SpeedKmH)
                    .HasColumnName("Speed_km_h")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ThrottlePercentage).HasColumnName("Throttle_percentage");
            });
        }
    }
}
