using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tube.Services;
using Tube.Web.InputModels.Channel;
using Tube.Web.ViewModels.Channel;
using Tube.Web.ViewModels.Video;

namespace Tube.Web.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly IChannelService channelService;
        private readonly IMapper mapper;
        private readonly IVideoService videoService; 
        private readonly ICloudinaryService cloudinaryService;

        public ChannelsController(IChannelService channelService, IMapper mapper,
            IVideoService videoService, ICloudinaryService cloudinaryService)
        {
            this.channelService = channelService;
            this.mapper = mapper;
            this.videoService = videoService;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateChannelInputModel createChannelInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Create));
            }

            string pictureUrl = this.cloudinaryService
                .UploadPicture(createChannelInputModel.ChannelPicUrl, createChannelInputModel.Name);

            var username = this.User.Identity.Name;
            this.channelService.Create(createChannelInputModel.Name, createChannelInputModel.Description, pictureUrl, username);

            return this.RedirectToAction(nameof(My));
        }

        [Authorize]
        public IActionResult Videos(string id)
        {
            var videos = this.videoService .GetVideosByChannelId(id);

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return View("~/Views/Videos/Videos.cshtml", videosViewModel);
        }

        [Authorize]
        public IActionResult Subscribers(string id)
        {
            var subscribers = this.channelService.GetSubscribersByChannelId(id);

            var subscribersViewModel = this.mapper.Map<List<SubscribersViewModel>>(subscribers);

            return this.View(subscribersViewModel);
        }

        [Authorize]
        public IActionResult My()
        {
            var channel = this.channelService.GetChannelByTubeUserName(this.User.Identity.Name);

            if (channel == null)
            {
                return this.RedirectToAction(nameof(Create));
            }

            var channelViewModel = mapper.Map<ChannelViewModel>(channel);

            return View(channelViewModel);
        }

        [Authorize]
        public IActionResult Other(string id)
        {
            var username = this.User.Identity.Name;
            var isYours = this.channelService.IsYours(id, username);

            if (isYours)
            {
                return this.RedirectToAction(nameof(My));
            }

            var channel = this.channelService.GetChannelById(id);

            var channelViewModel = mapper.Map<ChannelViewModel>(channel);

            return this.View(channelViewModel);
        }

        [Authorize]
        public IActionResult Subscribe(string id)
        {
            string username = this.User.Identity.Name;
            this.channelService.Subscribe(id, username);

            return this.RedirectToAction("Subscriptions", "Users");
        }
    }
}