using System.Data.Entity;
using Frame.Models;

namespace Frame.MetaData
{
    public class FrameContext : DbContext
    {
        public FrameContext() : base("MySqlConnection")
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<TopMenus> TopMenus { get; set; }
        public DbSet<LeftMenus> LeftMenus { get; set; }
        public DbSet<NavBarGroups> NavBarGroups { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<StaffRoleRelationships> StaffRoleRelationships { get; set; }
        public DbSet<LeftMenuPermissions> LeftMenuPermissions { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Dept> Dept { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<SysSetting> SysSetting { get; set; }
    }
}
