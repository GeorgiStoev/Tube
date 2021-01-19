using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public class UserService : IUserService
    {
        private readonly TubeDbContext context;
        private readonly IMapper mapper;

        public UserService(TubeDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<TubeUserChannelDTO> GetSubscriptionsByUserName(string tubeUserName)
        {
            var user = this.GetTubeUserByUsername(tubeUserName);

            var usersChannels = this.context.TubeUserChannels.Where(userChannel => userChannel.TubeUserId == user.Id);

            foreach (var userChannel in usersChannels)
            {
                userChannel.Channel = this.context.Channels.FirstOrDefault(channel => channel.Id == userChannel.ChannelId);
            }

            var tubeUserChannelDto = this.mapper.Map<List<TubeUserChannelDTO>>(usersChannels);
            return tubeUserChannelDto;
        }

        public TubeUserDTO GetTubeUserById(string id)
        {
            var user = this.context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            user.Channel = this.context.Channels.AsNoTracking().FirstOrDefault(channel => channel.TubeUserId == user.Id);

            var tubeUserDto = this.mapper.Map<TubeUserDTO>(user);
            return tubeUserDto;
        }

        public TubeUserDTO GetTubeUserByUsername(string username)
        {
            var user = this.context.Users.AsNoTracking().FirstOrDefault(u => u.UserName == username);

            var tubeUserDto = this.mapper.Map<TubeUserDTO>(user);
            return tubeUserDto;
        }

        public TubeUser GetDomainUserById(string id)
        {
            var tubeUser = this.context.Users.FirstOrDefault(u => u.Id == id);

            return tubeUser;
        }

        public TubeUser GetDomainUserFromDbByUserName(string username)
        {
            var tubeUser = this.context.Users.FirstOrDefault(u => u.UserName == username);

            return tubeUser;
        }

        public void RemoveSubscription(string channelId, string username)
        {
            var userId = this.GetTubeUserByUsername(username).Id;
            var userChannel = this.context.TubeUserChannels.AsNoTracking().SingleOrDefault(uc => uc.ChannelId == channelId && uc.TubeUserId == userId);

            this.context.TubeUserChannels.Remove(userChannel);
            this.context.SaveChanges();
        }
    }
}
