using System;
using System.Net;
using MonkeyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(MonkeyServicePage))]
namespace MonkeyService
{
    public partial class MonkeyServicePage : ContentPage
    {
		private const string weburi = "https://raw.githubusercontent.com/jamesmontemagno/MonkeysApp-AppIndexing/master/MonkeysApp/monkeydata.json";

		public ListView _list { get; set; }

        public MonkeyServicePage()
        {
            InitializeComponent();
            GetList();
        }
        void GetList()
        {
            //MonkeyWebService

           // DependencyService.Get<IMonkeyService>().GetService(weburi);
            _list = new ListView();

            _list.ItemTemplate = new DataTemplate(typeof(ImageCell));
            _list.ItemTemplate.SetBinding(TextCell.TextProperty,"Name");
            _list.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Location");
            _list.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

            _list.ItemsSource = DependencyService.Get<IMonkeyService>().GetService(weburi);
            Content = _list;
        }

    }
}
