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
				var masterPage = (MasterPage)App.MasterDetail.Master;
				masterPage.SetMyShelves();
				var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/main/shelf", string.Empty));
				string url = "http://smartshelf.mybluemix.net/main/shelf";
				//client.ContentType = "application/json";

				string postBody = JsonConvert.SerializeObject(shelfItem);
				string postData = JsonConvert.SerializeObject(shelfItem);
				await DoAsyncPut(url, postData);
			}
			catch (Exception ex)
			{
			}
		}
		private async Task DoAsyncPut(string url, string postData)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "PUT";
			request.ContentType = "application/json";

			byte[] postBytes = Encoding.UTF8.GetBytes(postData);
			var content = new ByteArrayContent(postBytes);
			content.Headers.ContentLength = postBytes.Length;

			content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

			using (var client = new HttpClient())
			{
				await client.PutAsync(url, content);
			}


		}
    }
}
