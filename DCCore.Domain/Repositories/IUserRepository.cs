using DCCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCCore.Domain.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        User FindByUserName(string username);
        Task<User> FindByUserNameAsync(string username);
        User FindByEmail(string email);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);       
    }
}
