using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;

namespace WereldService.Publishers
{
    public interface IWorldPublisher
    {
        public Task PublishNewWorld(World world);
        public Task PublishUpdateWorld(Guid id, string newTitle);
        public Task DeleteWorldWorld(Guid id);
    }
}
