using System;
using System.Net;
using MonkeyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(MonkeyServicePage))]
namespace MonkeyService
{
    public partial class MonkeyServicePage : ContentPage
    {
		//Json to Consume data from
		private const string weburi = "https://raw.githubusercontent.com/jamesmontemagno/MonkeysApp-AppIndexing/master/MonkeysApp/monkeydata.json";

		public ListView _list { get; set; }

        public MonkeyServicePage()
        {
            InitializeComponent();
            //Calling the List Method For Initial Page
            GetList();
        }
        void GetList()
        {
            _list = new ListView();

            _list.ItemTemplate = new DataTemplate(typeof(ImageCell));

            //Property binding 
            _list.ItemTemplate.SetBinding(TextCell.TextProperty,"Name");
            _list.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Location");
            _list.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

            //Listing the data retrieve from the Service
            _list.ItemsSource = DependencyService.Get<IMonkeyService>().GetService(weburi);


            //Adding content to the page
            Content = _list;
        }

    }
}
