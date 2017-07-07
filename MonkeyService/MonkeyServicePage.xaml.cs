using System;
using System.IO;
using System.Net;
using MonkeyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(MonkeyServicePage))]
namespace MonkeyService
{
    public partial class MonkeyServicePage : ContentPage
    {
        //Json to Consume data from
       // private const string weburi = "https://raw.githubusercontent.com/jamesmontemagno/MonkeysApp-AppIndexing/master/MonkeysApp/monkeydata.json";
        private const string weburi = "https://api.myjson.com/bins/1czns7";
		public ListView _list { get; set; }

        public MonkeyServicePage()
        {
            InitializeComponent();
            //Calling the List Method For Initial Page
            GetList();


        }
        void GetList()
        {
            this.Title = "List of Monkeys";
            var width = 220;
            var height = 50;
            var label = new Label
            {
                Text = "List of Monkeys!",
                BackgroundColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.Center
            };

            _list = new ListView();

            _list.ItemTemplate = new DataTemplate(typeof(ImageCell));

            //Property binding 
            _list.ItemTemplate.SetBinding(TextCell.TextProperty,"Name");
            _list.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Location");
            _list.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

            //Listing the data retrieve from the Service
            _list.ItemsSource = DependencyService.Get<IMonkeyService>().GetService(weburi);

            //remove a list history of monkeys
			var removebutton = new Button
			{
				Text = "Remove Monkey",
				BackgroundColor = Color.LightCoral,
				HeightRequest = height,
				WidthRequest = width,
			};

            //Add back the list of monkeys
			var Addbutton = new Button
            {
				Text = "Add Monkey",
				BackgroundColor = Color.Lime,
				HeightRequest = height,
				WidthRequest = width,
			};

            var Upload = new Button
            {
                Text = "Upload"
            };
           
			//Event buttons for removing and adding 
            removebutton.Clicked += async delegate 
			{
                var answer = await DisplayAlert("Warning", "Are you sure?", "Yes", "Cancel");
                if(answer)
                {
                    _list.ItemsSource = string.Empty;
                }
                /*else{
                    _list.ItemsSource = DependencyService.Get<IMonkeyService>().GetService(weburi);
                }*/
                
				

			};
			Addbutton.Clicked += (sender, e) =>
			{
				_list.ItemsSource = DependencyService.Get<IMonkeyService>().GetService(weburi);
			};

            Upload.Clicked += async (sender, e) => {

                Upload.IsEnabled = false;
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
						Upload.IsEnabled = true;
					};
					image.GestureRecognizers.Add(recognizer);

                    //(App as ContentPage).Content = image;
				}
				else
				{
					Upload.IsEnabled = true;
				}
            };


            //Adding content to the page
            Content = new StackLayout
            {
                //
                Padding = 20,
                Children = {
                    new Label{
                        Text = "List Of Random Monkeys",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new ScrollView{
                        Content = _list
                    },
					 Addbutton,
					removebutton,
                    Upload
                }

            };

        }

    }
}
