using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Services.Models
{
    public class HistoryServiceModel
    {
        public string Id { get; set; }

        public string TubeUserId { get; set; }
        public UserServiceModel TubeUser { get; set; }

        public string VideoName { get; set; }

        public DateTime Date { get; set; }
    }
}
