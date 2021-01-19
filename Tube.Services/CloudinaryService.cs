using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Tube.Common;

namespace Tube.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public string UploadPicture(IFormFile pictureFile, string fileName)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                pictureFile.CopyTo(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = GlobalConstants.tubeChannelImagesFolder,
                    File = new FileDescription(fileName, ms)
                };
                 
                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }

        public string UploadVideo(IFormFile videoFile, string fileName)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                videoFile.CopyTo(ms);
                destinationData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(destinationData))
            {
                VideoUploadParams uploadParams = new VideoUploadParams
                {
                    Folder = GlobalConstants.tubeVideosFolder,
                    File = new FileDescription(fileName, ms)
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
