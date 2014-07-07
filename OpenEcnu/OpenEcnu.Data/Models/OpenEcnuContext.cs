using System.Data.Entity;
using OpenEcnu.Data.Models.Mapping;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// �����������࣬ʵ�ֳ��������ݿ�Ľ���
    /// </summary>
    public class OpenEcnuContext : DbContext
    {
        static OpenEcnuContext()
        {
            Database.SetInitializer<OpenEcnuContext>(null);
        }

        public OpenEcnuContext()
            : base("Name=OpenEcnuContext")
        {
        }

        /// <summary>
        /// ӳ��AccessToken��
        /// </summary>
        public DbSet<AccessToken> AccessTokens { get; set; }

        /// <summary>
        /// ӳ��AppInfo��
        /// </summary>
        public DbSet<AppInfo> AppInfoes { get; set; }
        
        /// <summary>
        /// ӳ��Authorization��
        /// </summary>
        public DbSet<Authorization> Authorizations { get; set; }

        /// <summary>
        /// ӳ��User��
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// ӳ��UsersDetail��
        /// </summary>
        public DbSet<UsersDetail> UsersDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessTokenMap());
            modelBuilder.Configurations.Add(new AppInfoMap());
            modelBuilder.Configurations.Add(new AuthorizationMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UsersDetailMap());
        }
    }
}
