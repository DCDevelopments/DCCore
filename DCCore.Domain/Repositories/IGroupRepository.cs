using DCCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCCore.Domain.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Group FindByName(string groupname);
        Task<Group> FindByNameAsync(string groupname);
        Task<Group> FindByNameAsync(CancellationToken cancellationToken, string groupname);
    }
}
