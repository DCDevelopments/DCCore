using DCCore.Domain.Entities;
using DCCore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace DCCore.EntityFramework.Repositories
{
    internal class MailRepository : Repository<Mail>, IMailRepository
    {
        internal MailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Mail FindByUserId(Guid userId)
        {
            return Set.FirstOrDefault(x => x.UserId == userId);
        }

        public Task<Mail> FindByUserIdAsync(Guid userId)
        {
            return Set.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<Mail> FindByUserIdAsync(CancellationToken cancellationToken, Guid userId)
        {
            return Set.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }        
      
    }
}
