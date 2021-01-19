using System.ComponentModel.DataAnnotations;

namespace Tube.Web.InputModels.Video
{
    public class SearchVideoInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
