using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartShelf
{
	public partial class AddSmartShelfPage : ContentPage
	{
		public AddSmartShelfPage ()
		{
			InitializeComponent ();
		}
		private async void OnRegisterShelfClicked(Object sender, EventArgs e)
		{
			try
			{
				string url = "http://smartshelf.mybluemix.net/main/shelf";

				string postData = "{ \"id\": \"" + txtShelfID.Text + "\", \"name\": \"" + txtDescription.Text + "\" }";
				await SmartShelfService.DoAsyncPut(url, postData);

				HttpClient client = new HttpClient();
				await SmartShelfService.Authenticate("demouser", "123456", client);
				var masterPage = (MasterPage)App.MasterDetail.Master;
				masterPage.SetMyShelves();

				App.MasterDetail.Detail = new NavigationPage(new DashboardPage());


			}
			catch (Exception ex)
			{
				var exst = ex.Message;
				ShelfMessage.Text = "Shelf could not be added at this time";
			}
		}
	}
}
