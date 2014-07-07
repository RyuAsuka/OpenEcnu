using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OpenEcnu.Data.Models.Mapping
{
    public class AccessTokenMap : EntityTypeConfiguration<AccessToken>
    {
        public AccessTokenMap()
        {
            // Primary Key
            HasKey(t => new { t.appkey, t.userid });

            // Properties
            Property(t => t.appkey)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.userid)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.accesstoken1)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("AccessToken");
            Property(t => t.appkey).HasColumnName("appkey");
            Property(t => t.userid).HasColumnName("userid");
            Property(t => t.accesstoken1).HasColumnName("accesstoken");
            Property(t => t.createtime).HasColumnName("createtime");
            Property(t => t.expire).HasColumnName("expire");

            // Relationships
            HasRequired(t => t.AppInfo)
                .WithMany(t => t.AccessTokens)
                .HasForeignKey(d => d.appkey);
            HasRequired(t => t.User)
                .WithMany(t => t.AccessTokens)
                .HasForeignKey(d => d.userid);

        }
    }
}
