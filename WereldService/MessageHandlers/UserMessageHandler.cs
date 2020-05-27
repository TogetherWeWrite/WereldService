using MessageBroker;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WereldService.MqMessages;
using WereldService.Entities;
using WereldService.Repositories;

namespace WereldService.MessageHandlers
{
    public class UserMessageHandler : IMessageHandler<RegisterMessage>
    {
        private readonly IUserRepository _userRepository;
        public UserMessageHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public Task HandleMessageAsync(string messageType, RegisterMessage sendable)
        {
            var user = new User { Id = sendable.id, Name = sendable.username, WorldFollowed = new List<Guid>() };
            _userRepository.Create(user);
            return Task.CompletedTask;
        }

        public Task HandleMessageAsync(string messageType, byte[] obj)
        {
            return HandleMessageAsync(messageType, JsonSerializer.Deserialize<RegisterMessage>((ReadOnlySpan<byte>)obj, (JsonSerializerOptions)null));
        }
    }
}
