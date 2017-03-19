using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartShelf
{
	public partial class EditSmartShelfPage : ContentPage
	{
        private ShelfItem shelfItem;

        public EditSmartShelfPage ()
		{
			InitializeComponent ();
			btnUpdateShelf.Clicked += updateShelf;
		}

        public ShelfItem ShelfItem
        {
            get { return shelfItem; }
            set { shelfItem = value; this.BindingContext = shelfItem; }
        }
		private async void updateShelf(object sender, EventArgs e)
		{
			try
			{
				shelfItem.Name = shelfName.Text;

				string url = "http://smartshelf.mybluemix.net/main/UpdateShelf?shelfId=" + shelfItem.Id + "&shelfName=" + shelfName.Text;
				////client.ContentType = "application/json";

				//string postBody = JsonConvert.SerializeObject(shelfItem);

				HttpClient client = new HttpClient();
				await client.PostAsync(new Uri(url), new StringContent(""));
				await SmartShelfService.Authenticate("demouser", "123456", client);
				var masterPage = (MasterPage)App.MasterDetail.Master;
				masterPage.SetMyShelves();
				App.MasterDetail.Detail = new NavigationPage(new DashboardPage());
			}
			catch (Exception ex)
			{
			}
		}

    }
}
