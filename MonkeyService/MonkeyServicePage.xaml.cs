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


            var move = new Button { 
                Text = "Insert monkey",
                BackgroundColor = Color.AliceBlue,
				HeightRequest = height,
				WidthRequest = width,
            };

            move.Clicked += (sender, e) => {
                Navigation.PushAsync(new MonkeyPage()); // PushAsync(new NavigationPage(new MonkeyPage()));
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
                    move
                }

            };

        }

    }
}
