using Adaro.Centralize.EntityFrameworkCore;

namespace Adaro.Centralize.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly CentralizeDbContext _context;

        public InitialHostDbBuilder(CentralizeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
