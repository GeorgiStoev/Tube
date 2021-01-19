using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tube.Services
{
    public interface ICloudinaryService
    {
        string UploadPicture(IFormFile pictureFile, string fileName);

        string UploadVideo(IFormFile pictureFile, string fileName);
    }
}
