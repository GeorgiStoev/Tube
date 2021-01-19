using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly TubeDbContext context;
        private readonly IUserService userService;
        private readonly IVideoService videoService;
        private readonly IMapper mapper;

        public PlaylistService(TubeDbContext context, IUserService userService, IVideoService videoService, IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.videoService = videoService;
            this.mapper = mapper;
        }

        public void AddVideoToPlaylist(string playlistId, string videoId)
        {
            var isAlreadyAdded = IsAdded(playlistId, videoId);
            if (isAlreadyAdded)
            {
                return;
            }

            var videoPlaylist = new VideoPlaylist
            {
                VideoId = videoId,
                PlaylistId = playlistId
            };

            this.context.VideoPlaylists.Add(videoPlaylist);
            this.context.SaveChanges();
        }

        public PlaylistDTO Create(string name, string tubeUserName)
        {
            var tubeUser = this.userService.GetDomainUserFromDbByUserName(tubeUserName);

            var playlist = new Playlist
            {
                Name = name,
                TubeUserId = tubeUser.Id,
                Videos = new List<VideoPlaylist>()
            };

            this.context.Playlists.Add(playlist);
            this.context.SaveChanges();

            var playlistDto = this.mapper.Map<PlaylistDTO>(playlist);
            return playlistDto;
        }

        public PlaylistDTO GetPlaylistById(string playlistId)
        {
            var playlist = this.context.Playlists.AsNoTracking().FirstOrDefault(p => p.Id == playlistId);

            var playlistDto = this.mapper.Map<PlaylistDTO>(playlist);
            return playlistDto;
        }

        public List<VideoDTO> GetPlaylistVideos(string playlistId)
        {
            var videoPlaylists = this.context
                                     .VideoPlaylists.AsNoTracking()
                                     .Where(videoPlaylist => videoPlaylist.PlaylistId == playlistId)
                                     .ToList();

            var videos = new List<VideoDTO>();
            foreach (var videoPlaylist in videoPlaylists)
            {
                var video = this.videoService.GetVideoById(videoPlaylist.VideoId);
                videos.Add(video);
            }

            return videos;
        }

        public List<PlaylistDTO> GetUserPlayListsByUsername(string tubeUserName)
        {
            var tubeUser = this.userService.GetTubeUserByUsername(tubeUserName);
            var playlists = this.context
                                .Playlists.AsNoTracking()
                                .Where(playlist => playlist.TubeUserId == tubeUser.Id)
                                .ToList();

            var playlistsDto = this.mapper.Map<List<PlaylistDTO>>(playlists);
            return playlistsDto;
        }

        private bool IsAdded(string playlistId, string videoId)
        {
            var videoPlaylist = this.GetVideoPlaylist(playlistId, videoId);

            if (videoPlaylist == null)
            {
                return false;
            }

            return true;
        }

        private VideoPlaylistDTO GetVideoPlaylist(string playlistId, string videoId)
        {
            var videoPlaylist = this.context
                                    .VideoPlaylists.AsNoTracking()
                                    .FirstOrDefault(vp => vp.PlaylistId == playlistId && vp.VideoId == videoId);

            var videoPlaylistDto = this.mapper.Map<VideoPlaylistDTO>(videoPlaylist);
            return videoPlaylistDto;
        }
    }
}
