using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tube.Services;
using Tube.Web.ViewModels.Channel;

namespace Tube.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IChannelService channelService;

        public UsersController(IMapper mapper, IUserService userService, IChannelService channelService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.channelService = channelService;
        }

        [Authorize]
        public IActionResult Subscriptions()
        {
            var username = this.User.Identity.Name;
            var subscriptions = this.userService.GetSubscriptionsByUserName(username);

            var subscriptionViewModels = this.mapper.Map<List<SubscribersViewModel>>(subscriptions);

            return this.View(subscriptionViewModels);
        }

        [Authorize]
        public IActionResult Remove(string id)
        {
            var username = this.User.Identity.Name;
            this.userService.RemoveSubscription(id, username);

            return this.RedirectToAction(nameof(Subscriptions));
        }
    }
}