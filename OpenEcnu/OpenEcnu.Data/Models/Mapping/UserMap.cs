using System.Data.Entity.ModelConfiguration;

namespace OpenEcnu.Data.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.userid);

            // Properties
            Property(t => t.userid)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.passwd)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Users");
            Property(t => t.userid).HasColumnName("userid");
            Property(t => t.passwd).HasColumnName("passwd");
        }
    }
}
