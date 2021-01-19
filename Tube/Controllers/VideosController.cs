using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tube.Services;
using Tube.Web.InputModels.Video;
using Tube.Web.ViewModels.Video;

namespace Tube.Web.Controllers
{
    public class VideosController : Controller
    {
        private readonly IVideoService videoService;
        private readonly IMapper mapper;
        private readonly IChannelService channelService;
        private readonly IVoteService voteService;
        private readonly IHistoryService historyService;
        private readonly IUserService userService;
        private readonly ICloudinaryService cloudinaryService;

        public VideosController(IVideoService videoService, IMapper mapper, IChannelService channelService,
            IVoteService voteService, IHistoryService historyService, IUserService userService, ICloudinaryService cloudinaryService)
        {
            this.videoService = videoService;
            this.mapper = mapper;
            this.channelService = channelService;
            this.voteService = voteService;
            this.historyService = historyService;
            this.userService = userService;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CreateVideoInputModel createVideoInputModel)
        {
            if (!ModelState.IsValid)
            {
                this.RedirectToAction(nameof(Add));
            }

            var videoUrl = this.cloudinaryService
                               .UploadVideo(createVideoInputModel.VideoUrl, createVideoInputModel.Name);

            var username = this.User.Identity.Name;
            this.videoService.Add(createVideoInputModel.Name, createVideoInputModel.Category, videoUrl, username);
            var channel = this.channelService.GetChannelByTubeUserName(username);

            return this.RedirectToAction("Videos", "Channels", new { id = channel.Id });
        }

        [Authorize]
        public IActionResult Watch(string id)
        {
            var video = this.videoService.GetVideoById(id);

            video = this.videoService.View(video);

            var watchVideoViewModel = this.mapper.Map<WatchVideoViewModel>(video);

            var tubeUserName = this.User.Identity.Name; 
            this.historyService.Create(tubeUserName, video.Name);

            return this.View(watchVideoViewModel);
        }

        public IActionResult GuestWatch(string id)
        {
            var video = this.videoService.GetVideoById(id);

            this.videoService.View(video);

            var guestWatchVideoViewModel = this.mapper.Map<GuestWatchVideoViewModel>(video);

            return this.View(guestWatchVideoViewModel);
        }

        public IActionResult Search()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Search(SearchVideoInputModel searchVideoInputModel)
        {
            if (!ModelState.IsValid)
            {
                this.RedirectToAction(nameof(Search));
            }

            var searchVideosName = searchVideoInputModel.Name;

            return this.RedirectToAction("Index", "Home", new { searchVideosName });
        }

        [Authorize]
        public IActionResult Like(string id)
        {
            var video = this.videoService.GetVideoById(id);
            var username = this.User.Identity.Name;
            var user = this.userService.GetTubeUserByUsername(username);

            video = this.videoService.Like(video, username);
            this.historyService.DeleteLastHistory(user.Id);

            return this.RedirectToAction("Watch", new { video.Id });
        }

        [Authorize]
        public IActionResult Unlike(string id)
        {
            var video = this.videoService.GetVideoById(id);
            var username = this.User.Identity.Name;
            var user = this.userService.GetTubeUserByUsername(username);

            this.voteService.UnVote(video, username);
            this.historyService.DeleteLastHistory(user.Id);

            return this.RedirectToAction("Watch", new { video.Id });
        }
    }
}