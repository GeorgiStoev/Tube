using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Data.DtoModels
{
    public class HistoryDTO
    {
        public string Id { get; set; }

        public string TubeUserId { get; set; }
        public virtual TubeUserDTO TubeUser { get; set; }

        public string VideoName { get; set; }

        public DateTime Date { get; set; }
    }
}
