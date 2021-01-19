using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tube.Common;
using Tube.Data.DtoModels;
using Tube.Data.Models;
using Tube.Services;
using Tube.Web.ViewModels.Video;

namespace Tube.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVideoService videoService;

        public HomeController(IMapper mapper, IVideoService videoService)
        {
            this.mapper = mapper;
            this.videoService = videoService;
        }

        public IActionResult Index(string searchVideosName)
        {
            var videos = new List<VideoDTO>();

            if (searchVideosName != null)
            {
                videos = this.videoService.GetSearchedVideos(searchVideosName);
            }
            else
            {
                videos = this.videoService.GetMostViewedVideos();
            }

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View(videosViewModel);
        }

        public IActionResult All()
        {
            var videos = this.videoService.GetAllVideos();

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult MostViewed()
        {
            var videos = this.videoService.GetMostViewedVideos();

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult MostLiked()
        {
            var videos = this.videoService.GetMostLikedVideos();

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult MostCommend()
        {
            var videos = this.videoService.GetMostCommendVideos();

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult MusicVideos()
        {
            var category = GlobalConstants.Music;
            var videos = this.videoService.GetVideosByCategory(category);

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult NewsVideos()
        {
            var category = GlobalConstants.News;
            var videos = this.videoService.GetVideosByCategory(category);

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult TvVideos()
        {
            var category = GlobalConstants.TV;
            var videos = this.videoService.GetVideosByCategory(category);

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }

        public IActionResult SportVideos()
        {
            var category = GlobalConstants.Sport;
            var videos = this.videoService.GetVideosByCategory(category);

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return this.View("Index", videosViewModel);
        }
    }
}