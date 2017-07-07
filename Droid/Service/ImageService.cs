using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using MonkeyService.Droid.Service;
using Xamarin.Forms;

[assembly:  Dependency(typeof(ImageService))]
namespace MonkeyService.Droid.Service
{
    public class ImageService : IPickerImage
    {
        public ImageService()
        {
        }

        public Task<Stream> GetImage()
        {
            //Define the intent for getting image content
            Intent image = new Intent();
            image.SetType("image/*");
            image.SetAction(Intent.ActionGetContent);

            //Get the Mainactivity instance
            MainActivity mainactivity = Forms.Context as MainActivity;

            //start the picture picker
            mainactivity.StartActivityForResult(Intent.CreateChooser(image, ""), MainActivity.PickImageId);

			// Save the TaskCompletionSource object as a MainActivity property
			mainactivity.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

			// Return Task object
			return mainactivity.PickImageTaskCompletionSource.Task;
        }
    }
}
