using System;
using System.Collections.Generic;
using System.Text;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface IUserService
    {
        TubeUser GetDomainUserFromDbByUserName(string username);

        TubeUser GetDomainUserById(string id);

        TubeUserDTO GetTubeUserByUsername(string username);

        List<TubeUserChannelDTO> GetSubscriptionsByUserName(string tubeUserName);

        void RemoveSubscription(string channelId, string username);

        TubeUserDTO GetTubeUserById(string id);
    }
}
