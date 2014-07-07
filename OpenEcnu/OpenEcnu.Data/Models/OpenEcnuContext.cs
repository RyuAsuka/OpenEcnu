using System.Data.Entity;
using OpenEcnu.Data.Models.Mapping;

namespace OpenEcnu.Data.Models
{
    /// <summary>
    /// 数据上下文类，实现程序与数据库的交互
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
        /// 映射AccessToken表
        /// </summary>
        public DbSet<AccessToken> AccessTokens { get; set; }

        /// <summary>
        /// 映射AppInfo表
        /// </summary>
        public DbSet<AppInfo> AppInfoes { get; set; }
        
        /// <summary>
        /// 映射Authorization表
        /// </summary>
        public DbSet<Authorization> Authorizations { get; set; }

        /// <summary>
        /// 映射User表
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 映射UsersDetail表
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
