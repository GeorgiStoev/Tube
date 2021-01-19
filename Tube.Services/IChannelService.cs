using System.Collections.Generic;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface IChannelService
    {
        Channel GetDomainChannelByUserName(string username);

        Channel GetDomainChannelById(string id);

        ChannelDTO Create(string name, string description, string channelPicUrl, string tubeUserName);

        ChannelDTO GetChannelByTubeUserName(string tubeUserName);

        List<TubeUserChannelDTO> GetSubscribersByChannelId(string tubeUserName);

        ChannelDTO GetChannelById(string id);

        void Subscribe(string id, string tubeUserName);

        bool IsYours(string id, string tubeUserName);
    }
}
