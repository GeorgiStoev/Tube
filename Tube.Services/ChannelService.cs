using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tube.Common;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public class ChannelService : IChannelService
    {
        private readonly TubeDbContext context;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public ChannelService(TubeDbContext context, IUserService userService, IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
        }

        public ChannelDTO Create(string name, string description, string channelPicUrl, string tubeUserName)
        {
            if (channelPicUrl == null)
            {
                channelPicUrl = GlobalConstants.dafaultChannelPicUrl;
            }

            var user = this.userService.GetDomainUserFromDbByUserName(tubeUserName);

            var channel = new Channel
            {
                Name = name,
                Description = description,
                ChannelPicUrl = channelPicUrl,
                TubeUserId = user.Id,
                TubeUser = user
            };

            this.context.Channels.Add(channel);
            this.context.SaveChanges();

            return this.mapper.Map<ChannelDTO>(channel);
        }
        
        public ChannelDTO GetChannelById(string id)
        {
            var channel = this.context.Channels.AsNoTracking().SingleOrDefault(c => c.Id == id);

            return this.mapper.Map<ChannelDTO>(channel);
        }

        public ChannelDTO GetChannelByTubeUserName(string tubeUserName)
        {
            var channel = this.context.Channels.AsNoTracking().SingleOrDefault(c => c.TubeUser.UserName == tubeUserName);

            return this.mapper.Map<ChannelDTO>(channel);
        }

        public List<TubeUserChannelDTO> GetSubscribersByChannelId(string channelId)
        {
            var usersChannels = this.context.TubeUserChannels.AsNoTracking().Where(userChannel => userChannel.ChannelId == channelId).ToList();

            foreach (var userChannel in usersChannels)
            {
                userChannel.TubeUser = this.userService.GetDomainUserById(userChannel.TubeUserId);
                userChannel.Channel = this.GetDomainChannelByUserName(userChannel.TubeUser.UserName);
            }

            return this.mapper.Map<List<TubeUserChannelDTO>>(usersChannels);
        }

        public void Subscribe(string id ,string tubeUserName)
        {
            var user = this.userService.GetDomainUserFromDbByUserName(tubeUserName);
            var channel = this.GetDomainChannelById(id);

            var userChannel = new TubeUserChannel
            {
                TubeUserId = user.Id,
                TubeUser = user,
                ChannelId = channel.Id,
                Channel = channel
            };

            bool isSubscribed = this.IsSubscribed(userChannel);
            if (!isSubscribed)
            {
                this.context.TubeUserChannels.Add(userChannel);
                context.SaveChanges();
            }
        }

        public bool IsYours(string id, string tubeUserName)
        {
            var user = this.userService.GetDomainUserFromDbByUserName(tubeUserName);
            var channel = this.GetDomainChannelById(id);

            if (channel.TubeUserId == user.Id)
            {
                return true;
            }

            return false;
        }

        private bool IsSubscribed(TubeUserChannel userChannel)
        {
            var userChannels = this.context.TubeUserChannels.AsNoTracking().ToList();

            foreach (var uc in userChannels)
            {
                if (uc.ChannelId == userChannel.ChannelId && uc.TubeUserId == userChannel.TubeUserId)
                {
                    return true;
                }
            }

            return false;
        }

        public Channel GetDomainChannelByUserName(string username)
        {
            var channel = this.context.Channels.FirstOrDefault(u => u.TubeUser.UserName == username);

            return channel;
        }

        public Channel GetDomainChannelById(string id)
        {
            var channel = this.context.Channels.FirstOrDefault(u => u.Id == id);

            return channel;
        }

    }
}
