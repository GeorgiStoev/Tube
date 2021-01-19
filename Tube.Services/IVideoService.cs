using System.Collections.Generic;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface IVideoService
    {
        Video GetDomainVideoById(string id);

        VideoDTO Add(string name, string category, string videoUrl, string tubeUserName);

        List<VideoDTO> GetVideosByChannelId(string id);

        VideoDTO View(VideoDTO video);

        VideoDTO Like(VideoDTO video, string username);

        void UnLike(VideoDTO video, string username);

        VideoDTO GetVideoById(string videoId);

        VideoDTO GetVideoByName(string videoName);

        List<VideoDTO> GetAllVideos();

        List<VideoDTO> GetSearchedVideos(string videoName);

        List<VideoDTO> GetMostViewedVideos();

        List<VideoDTO> GetMostLikedVideos();

        List<VideoDTO> GetMostCommendVideos();

        List<VideoDTO> GetVideosByCategory(string category);
    }
}
