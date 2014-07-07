using System.Data.Entity.ModelConfiguration;

namespace OpenEcnu.Data.Models.Mapping
{
    public class UsersDetailMap : EntityTypeConfiguration<UsersDetail>
    {
        public UsersDetailMap()
        {
            // Primary Key
            HasKey(t => t.userId);

            // Properties
            Property(t => t.userId)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Gender)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UsersDetail");
            Property(t => t.userId).HasColumnName("userId");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Gender).HasColumnName("Gender");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.Birthday).HasColumnName("Birthday");

            // Relationships
            HasRequired(t => t.User)
                .WithOptional(t => t.UsersDetail);

        }
    }
}
