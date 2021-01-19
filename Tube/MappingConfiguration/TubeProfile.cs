using AutoMapper;
using Tube.Data.DtoModels;
using Tube.Data.Models;
using Tube.Web.ViewModels.Channel;
using Tube.Web.ViewModels.Comment;
using Tube.Web.ViewModels.History;
using Tube.Web.ViewModels.Playlist;
using Tube.Web.ViewModels.Video;

namespace Tube.Web.MappingConfiguration
{
    public class TubeProfile : Profile
    {
        public TubeProfile()
        {
            this.CreateMap<Channel, ChannelDTO>();
            this.CreateMap<ChannelDTO, Channel>();
            this.CreateMap<Comment, CommentDTO>();
            this.CreateMap<CommentDTO, Comment>();
            this.CreateMap<History, HistoryDTO>();
            this.CreateMap<HistoryDTO, History>();
            this.CreateMap<Playlist, PlaylistDTO>();
            this.CreateMap<PlaylistDTO, Playlist>();
            this.CreateMap<TubeUser, TubeUserDTO>();
            this.CreateMap<TubeUserDTO, TubeUser>();
            this.CreateMap<TubeUserChannel, TubeUserChannelDTO>();
            this.CreateMap<TubeUserChannelDTO, TubeUserChannel>();
            this.CreateMap<Video, VideoDTO>();
            this.CreateMap<VideoDTO, Video>();
            this.CreateMap<VideoPlaylist, VideoPlaylistDTO>();
            this.CreateMap<VideoPlaylistDTO, VideoPlaylist>();
            this.CreateMap<Vote, VoteDTO>();
            this.CreateMap<VoteDTO, Vote>();

            this.CreateMap<VideoDTO, VideoViewModel>();
            this.CreateMap<TubeUserChannelDTO, TubeUserChannel>();
            this.CreateMap<ChannelDTO, ChannelViewModel>();
            this.CreateMap<HistoryDTO, HistoryViewModel>();
            this.CreateMap<VideoDTO, VideoViewModel>();
            this.CreateMap<PlaylistDTO, ChoosePlaylistViewModel>();
            this.CreateMap<TubeUserChannelDTO, SubscribersViewModel>();
            this.CreateMap<VideoDTO, WatchVideoViewModel>();
            this.CreateMap<VideoDTO, GuestWatchVideoViewModel>();
            this.CreateMap<CommentDTO, CommentViewModel>();

            this.CreateMap<Channel, ChannelViewModel>();
            this.CreateMap<TubeUserChannel, SubscribersViewModel>();

            this.CreateMap<VideoDTO, VideoViewModel>();
            this.CreateMap<VideoDTO, WatchVideoViewModel>()
                .ForMember(x => x.ChannelPicUrl, y => y.MapFrom(src => src.Channel.ChannelPicUrl))
                .ForMember(x => x.ChannelName, y => y.MapFrom(src => src.Channel.Name))
                .ForMember(x => x.Comments, y => y.MapFrom(src => src.Comments));

            this.CreateMap<VideoDTO, GuestWatchVideoViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.Name))
                .ForMember(x => x.Likes, y => y.MapFrom(src => src.Likes.Count))
                .ForMember(x => x.Comments, y => y.MapFrom(src => src.Comments.Count));

            this.CreateMap<Comment, CommentViewModel>();

            this.CreateMap<Playlist, ChoosePlaylistViewModel>();

            this.CreateMap<History, HistoryViewModel>();
        }
    }
}
