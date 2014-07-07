using System.Data.Entity.ModelConfiguration;

namespace OpenEcnu.Data.Models.Mapping
{
    public class AppInfoMap : EntityTypeConfiguration<AppInfo>
    {
        public AppInfoMap()
        {
            // Primary Key
            HasKey(t => t.appkey);

            // Properties
            Property(t => t.appsecret)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            Property(t => t.owner)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.redirecturi)
                .IsRequired();

            Property(t => t.name)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            ToTable("AppInfo");
            Property(t => t.appkey).HasColumnName("appkey");
            Property(t => t.appsecret).HasColumnName("appsecret");
            Property(t => t.owner).HasColumnName("owner");
            Property(t => t.redirecturi).HasColumnName("redirecturi");
            Property(t => t.name).HasColumnName("name");
            Property(t => t.description).HasColumnName("description");
        }
    }
}
