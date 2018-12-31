using System.Threading.Tasks;
using IdentityServer4.EFCore.Server.Models;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityServer4.EFCore.Server.Data
{
    public class TestDbContext : DbContext, IConfigurationDbContext, IPersistedGrantDbContext
    {
        #region IdentityServer4 Entities
        public DbSet<Client> Clients { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        #endregion

        #region Other Entities
        public DbSet<User> Users { get; set; }
        #endregion


        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }


        /// <summary>
        /// <see cref=""/>
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //BoolToZeroOneConverter





            //大部分网上的例子，都是使用

            modelBuilder.ConfigurePersistedGrantContext(OperationalStoreOptionsBuilder.Build());

            var options = ConfigurationStoreOptionsBuilder.Build();
            modelBuilder.ConfigureClientContext(options);
            modelBuilder.ConfigureResourcesContext(options);
        }
    }
}
