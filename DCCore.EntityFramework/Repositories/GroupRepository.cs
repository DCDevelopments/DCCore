using DCCore.Domain.Entities;
using DCCore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCCore.EntityFramework.Repositories
{
    internal class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Group FindByName(string groupname)
        {
            return Set.FirstOrDefault(x => x.Name == groupname);
        }

        public Task<Group> FindByNameAsync(string groupname)
        {
            return Set.FirstOrDefaultAsync(x => x.Name == groupname);
        }

        public Task<Group> FindByNameAsync(CancellationToken cancellationToken, string groupname)
        {
            return Set.FirstOrDefaultAsync(x => x.Name == groupname, cancellationToken);
        }
    }
}
