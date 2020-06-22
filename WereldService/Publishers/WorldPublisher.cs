using MessageBroker;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.Entities;
using WereldService.Settings;

namespace WereldService.Publishers
{
    public class WorldPublisher : IWorldPublisher
    {
        private readonly IMessageQueuePublisher _messageQueuePublisher;
        private readonly MessageQueueSettings _messageQueueSettings;
        public WorldPublisher(IMessageQueuePublisher messageQueuePublisher, IOptions<MessageQueueSettings> messageQueueSettings)
        {
            this._messageQueuePublisher = messageQueuePublisher;
            this._messageQueueSettings = messageQueueSettings.Value;
        }
        public async Task PublishNewWorld(World world)
        {
            await _messageQueuePublisher.PublishMessageAsync(_messageQueueSettings.Exchange, "cell-service", "new-world", new { Id = world.Id, Title = world.Title });
            await _messageQueuePublisher.PublishMessageAsync(_messageQueueSettings.Exchange, "verhaal-service", "new-world", new { Id = world.Id, Title = world.Title });
        }

        public async Task PublishUpdateWorld(Guid id, string newTitle)
        {
            await _messageQueuePublisher.PublishMessageAsync(_messageQueueSettings.Exchange, "cell-service", "update-world", new { Id = id, NewTitle = newTitle });
            await _messageQueuePublisher.PublishMessageAsync(_messageQueueSettings.Exchange, "verhaal-service", "update-world", new { Id = id, NewTitle = newTitle });
        }

        public async Task DeleteWorldWorld(Guid id)
        {
            await _messageQueuePublisher.PublishMessageAsync(_messageQueueSettings.Exchange, "cell-service", "delete-world", new { Id = id });
            await _messageQueuePublisher.PublishMessageAsync(_messageQueueSettings.Exchange, "verhaal-service", "delete-world", new { Id = id });


        }
    }
}
