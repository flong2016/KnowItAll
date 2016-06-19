using Nop.Core.Domain.Directory;

namespace Nop.Data.Mapping.Directory
{

    //JXzfl
    public partial class CountyMap : NopEntityTypeConfiguration<County>
    {
        public CountyMap()
        {
            this.ToTable("County");
            this.HasKey(sp => sp.Id);
            this.Property(sp => sp.Name).IsRequired().HasMaxLength(100);
            this.Property(sp => sp.Abbreviation).HasMaxLength(100);


            this.HasRequired(sp => sp.City)
                .WithMany(c => c.Countys)
                .HasForeignKey(sp => sp.CityId);
        }
    }
}