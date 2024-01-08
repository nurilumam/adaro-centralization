using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Adaro.Centralize.EntityFrameworkCore
{
    public static class CentralizeDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CentralizeDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CentralizeDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}