using System;
using System.IO;
using MonkeyService;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

[assembly: Dependency(typeof(MonkeyPage))]
namespace MonkeyService
{
    public class MonkeyPage : ContentPage
    {
        public MonkeyPage()
        {
            var upload = new Button
            {
                Text = "Upload Image",
            };

            var submit = new Button
            {
                Text = "Post",
                BackgroundColor = Color.LightGreen
            };

            var open = new Button
            {
                Text = "Open"
            };


            var img = new Image();
            open.Clicked += (sender, e) => {

				var file = CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
				{
					PhotoSize = PhotoSize.Medium
				});
            };


			upload.Clicked += async (sender, e) =>
			{

				upload.IsEnabled = false;
				Stream stream = await DependencyService.Get<IPickerImage>().GetImage();
				if (stream != null)
				{
					Image image = new Image
					{
						Source = ImageSource.FromStream(() => stream),
						BackgroundColor = Color.Gray
					};

					TapGestureRecognizer recognizer = new TapGestureRecognizer();
					recognizer.Tapped += (sender2, args) =>
					{
						//(MainPage as ContentPage).Content = stack;
						upload.IsEnabled = true;
					};
					image.GestureRecognizers.Add(recognizer);

					//(App as ContentPage).Content = image;
				}
				else
				{
					upload.IsEnabled = true;
				}
			};

            Content = new StackLayout
            {
                Children = {
                    new Label { 
                        Text = "Insert Monkey Info",
                        FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center,
                        
                    },

                    new Entry {
                        Placeholder = "Enter Name",
                    },

                    new Entry {
                        Placeholder = "Enter Location",
                    },

                    new Entry {
                        Placeholder = "Enter Population",
                    },

                    upload,
                    submit,
                    open

                }
            };
        }
    }
}

