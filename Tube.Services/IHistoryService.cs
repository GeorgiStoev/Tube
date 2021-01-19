using System.Collections.Generic;
using Tube.Data.DtoModels;
using Tube.Data.Models;

namespace Tube.Services
{
    public interface IHistoryService
    {
        void Create(string tubeUserName, string videoName);

        List<HistoryDTO> GetTubeUserHistoriesById(string tubeUserId);

        void DeleteAllHistory(string tubeUserId);

        void DeleteLastHistory(string tubeUserId);
    }
}
