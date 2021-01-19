using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tube.Data.Models
{
    public class History
    {
        public string Id { get; set; }

        public string TubeUserId { get; set; }
        public virtual TubeUser TubeUser { get; set; }

        public string VideoName { get; set; }

        public DateTime Date { get; set; }
    }
}
