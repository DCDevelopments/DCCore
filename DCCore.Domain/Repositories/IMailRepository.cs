using DCCore.Domain.Entities;
using DCCore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCCore.Domain.Repositories
{
   
    public interface IMailRepository : IRepository<Mail>
    {
        Mail FindByUserId(Guid userId);
        Task<Mail> FindByUserIdAsync(Guid userId);
        Task<Mail> FindByUserIdAsync(CancellationToken cancellationToken, Guid userId);       
    }
}
