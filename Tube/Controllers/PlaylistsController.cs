using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tube.Services;
using Tube.Web.InputModels.Playlist;
using Tube.Web.ViewModels.Playlist;
using Tube.Web.ViewModels.Video;

namespace Tube.Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistService playlistService;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IVideoService videoService;

        public PlaylistsController(IPlaylistService playlistService, IMapper mapper, IUserService userService, IVideoService videoService)
        {
            this.playlistService = playlistService;
            this.mapper = mapper;
            this.userService = userService;
            this.videoService = videoService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreatePlaylistInputModel createPlaylistInputModel)
        {
            if (!ModelState.IsValid)
            {
                this.RedirectToAction(nameof(Add));
            }

            var username = this.User.Identity.Name;
            this.userService.GetTubeUserByUsername(username);
            this.playlistService.Create(createPlaylistInputModel.Name, username);

            return RedirectToAction(nameof(Choose), new { videoId = string.Empty });
        }

        [Authorize]
        public IActionResult Add(string id)
        {
            var username = this.User.Identity.Name;
            var playlists = this.playlistService.GetUserPlayListsByUsername(username);

            if (playlists.Count != 0)
            {
                return this.RedirectToAction(nameof(Choose), new { videoId = id });
            }

            return this.RedirectToAction(nameof(Create));
        }

        [Authorize]
        public IActionResult Choose(string videoId)
        {
            var username = this.User.Identity.Name;
            var playlists = this.playlistService.GetUserPlayListsByUsername(username);

            var playlistsViewModel = this.mapper.Map<List<ChoosePlaylistViewModel>>(playlists);

            foreach (var pvm in playlistsViewModel)
            {
                pvm.VideoId = videoId;
            }

            return View(playlistsViewModel);
        }

        [Authorize]
        public IActionResult Videos(string id)
        {
            var data = id.Split();
            var playlistId = data[0].ToString();

            if (data[1].ToString() != string.Empty)
            {
                var videoId = data[1].ToString();
                this.playlistService.AddVideoToPlaylist(playlistId, videoId);
            }

            var videos = this.playlistService.GetPlaylistVideos(playlistId);

            var videosViewModel = this.mapper.Map<List<VideoViewModel>>(videos);

            return View("~/Views/Videos/Videos.cshtml", videosViewModel);
        }
    }
}