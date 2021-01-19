using System.Collections.Generic;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface IPlaylistService
    {
        PlaylistDTO Create(string name, string tubeUserName);

        List<PlaylistDTO> GetUserPlayListsByUsername(string tubeUserName);

        List<VideoDTO> GetPlaylistVideos(string playlistId);

        PlaylistDTO GetPlaylistById(string playlistId);

        void AddVideoToPlaylist(string playlistId, string videoId);
    }
}
