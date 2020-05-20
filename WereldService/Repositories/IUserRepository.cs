using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Repositories
{
    public interface IUserRepository
    {
        
        Task<User> Get(Guid id);
        Task<User> Get(string Name);
        Task<User> Create(User world);
        Task Update(Guid id, User update);
        Task remove(Guid id);
    }
}
