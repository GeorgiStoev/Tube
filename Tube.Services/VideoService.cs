using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tube.Data;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public class VideoService : IVideoService
    {
        private readonly TubeDbContext context;
        private readonly IChannelService channelService;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly IVoteService voteService;

        public VideoService(TubeDbContext context, IVoteService voteService, IChannelService channelService, ICommentService commentService, IMapper mapper)
        {
            this.context = context;
            this.voteService = voteService;
            this.channelService = channelService;
            this.commentService = commentService;
            this.mapper = mapper;
        }

        public VideoDTO Add(string name, string category, string videoUrl, string tubeUserName)
        {
            var channel = this.channelService.GetDomainChannelByUserName(tubeUserName);

            var video = new Video
            {
                Name = name,
                ChannelId = channel.Id,
                Channel = channel,
                Category = category,
                Comments = new List<Comment>(),
                Likes = new List<Vote>(),
                VideoUrl = videoUrl,
                Date = DateTime.UtcNow
            };

            context.Videos.Add(video); ;
            context.SaveChanges();

            return this.mapper.Map<VideoDTO>(video);
        }

        public List<VideoDTO> GetAllVideos()
        {
            var videos = this.context.Videos.ToList();

            var videosDto = this.mapper.Map<List<VideoDTO>>(videos);
            return videosDto;
        }

        public List<VideoDTO> GetMostCommendVideos()
        {
            var allVideos = this.GetAllVideos();
            var videosCount = allVideos.Count;

            var videos = new List<VideoDTO>();
            if (videosCount >= 12)
            {
                videos = allVideos.OrderByDescending(video => video.Comments.Count).Take(12).ToList();
            }
            else
            {
                videos = allVideos.OrderByDescending(video => video.Comments.Count).Take(videosCount).ToList();
            }

            return videos;
        }

        public List<VideoDTO> GetMostLikedVideos()
        {
            var allVideos = this.GetAllVideos();
            var videosCount = allVideos.Count;

            var videos = new List<VideoDTO>();
            if (videosCount >= 12)
            {
                videos = allVideos.OrderByDescending(video => video.Likes.Count).Take(12).ToList();
            }
            else
            {
                videos = allVideos.OrderByDescending(video => video.Likes.Count).Take(videosCount).ToList();
            }

            return videos;
        }

        public List<VideoDTO> GetMostViewedVideos()
        {
            var allVideos = this.GetAllVideos();
            var videosCount = allVideos.Count;

            var videos = new List<VideoDTO>();
            if (videosCount >= 12)
            {
                videos = allVideos.OrderByDescending(video => video.Views).Take(12).ToList();
            }
            else
            {
                videos = allVideos.OrderByDescending(video => video.Views).Take(videosCount).ToList();
            }

            return videos;
        }

        public List<VideoDTO> GetSearchedVideos(string videoName)
        {
            var videos = this.context.Videos.Where(video => video.Name.ToLower().Contains(videoName.ToLower())).ToList();

            var videosDto = this.mapper.Map<List<VideoDTO>>(videos);
            return videosDto;
        }

        public VideoDTO GetVideoById(string videoId)
        {
            var video = this.GetDomainVideoById(videoId);

            var videoDto = this.mapper.Map<VideoDTO>(video);
            return videoDto;
        }

        public VideoDTO GetVideoByName(string videoName)
        {
            var video = this.context.Videos.FirstOrDefault(v => v.Name == videoName);

            var videoDto = this.mapper.Map<VideoDTO>(video);
            return videoDto;
        }

        public List<VideoDTO> GetVideosByChannelId(string id)
        {
            var videos = context.Videos.Where(video => video.ChannelId == id).OrderByDescending(video => video.Date).ToList();

            var videosDto = this.mapper.Map<List<VideoDTO>>(videos);
            return videosDto;
        }

        public VideoDTO Like(VideoDTO video, string username)
        {
            var isVoted = this.voteService.IsVoted(video, username);
            var videoFromDB = this.GetDomainVideoById(video.Id);

            if (!isVoted)
            {
                var vote = this.voteService.Create(video, username);
                var voteFromDB = this.context.Votes.FirstOrDefault(v => v.Id == vote.Id);
                videoFromDB.Likes.Add(voteFromDB);
            }

            videoFromDB.Views--;

            this.context.Update(videoFromDB);
            this.context.SaveChanges();

            var videoDto = this.mapper.Map<VideoDTO>(video);
            return videoDto;
        }

        public void UnLike(VideoDTO video, string username)
        {
            var isVoted = this.voteService.IsVoted(video, username);

            if (isVoted)
            {
                this.voteService.UnVote(video, username);
            }
        }

        public VideoDTO View(VideoDTO video)
        {
            var videoFromDB = this.context.Videos.FirstOrDefault(v => v.Id == video.Id);

            videoFromDB.Views++;
            context.SaveChanges();

            var videoDto = this.mapper.Map<VideoDTO>(video);
            return videoDto;
        }

        public List<VideoDTO> GetVideosByCategory(string category)
        {
            var videos = this.GetAllVideos().Where(video => video.Category == category).ToList();

            return videos;
        }

        public Video GetDomainVideoById(string id)
        {
            var video = this.context.Videos.FirstOrDefault(v => v.Id == id);
            video.Channel = this.channelService.GetDomainChannelById(video.ChannelId);
            video.Comments = this.commentService.GetDomainVideoCommentsByVideoId(video.Id);
            video.Likes = this.voteService.GteDomainVideoVotesByVideoId(video.Id);

            return video;
        }
    }
}
