using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartShelf.Entities;
using Xamarin.Forms;

namespace SmartShelf
{
	public partial class DashboardPage : ContentPage
	{
		HttpClient client;
		CancellationTokenSource cts;
		public DashboardPage ()
		{
			InitializeComponent ();
            SetScaleItems();
			//btnWatson.Clicked += GetPredictions;
			//var seconds = TimeSpan.FromSeconds(10);
			//client = new HttpClient();
			//Device.StartTimer(seconds, () =>
			//	{
					
			//			try
			//			{
			//				ReloadShelfs();

			//				//lblWeight.Text = lblWeight.Text + ".";
			//				// call your method to check for notifications here
			//				//LoadScales();
			//				// Returning true means you want to repeat this timer
			//				return true;

			//			}
			//			catch (Exception ex)
			//			{
			//				return false;
			//			}
					
			//	});
		}
	//private async void ReloadShelfs()
	//{
	//	var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/main/shelfs"));
	//	try
	//	{
	//		var response = await client.GetAsync(uri);
	//		if (response.IsSuccessStatusCode)
	//			{
	//				var content = await response.Content.ReadAsStringAsync();
	//				var shelfs = JsonConvert.DeserializeObject<List<Shelf>>(content);
	//				var masterPage = (MasterPage)App.MasterDetail.Master;
	//				SmartShelfService.Shelves = shelfs;
	//				masterPage.SetMyShelves();
	//		}
	//	}
	//	catch (Exception ex)
	//	{
	//	}
	//}
		private void GetPredictions(object sender, EventArgs e)
		{
			
		}
        private void SetScaleItems()
        {
            var scaleItems = SmartShelfService.GetScaleItemsForDashboard(ScaleOrderby.PercentageRemaining);
            foreach (var scaleItem in scaleItems)
            {
                stackLayout.Children.Add(new ScaleItemContentView(scaleItem));
            }
        }
	}
}
