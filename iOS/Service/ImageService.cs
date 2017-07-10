using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MonkeyService.iOS.Service
{
    public class ImageService : IPickerImage
    {
        public Task<Stream> GetImage()
        {
            var file = CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            });


            throw new NotImplementedException();
        }
    }
}
