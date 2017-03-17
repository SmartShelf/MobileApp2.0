using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartShelf.Entities;

using Xamarin.Forms;
using System.Net.Http.Headers;
using ZXing.Mobile;

namespace SmartShelf
{
	public partial class EditScalePage : ContentPage
	{
        private ScaleItem scaleItem;
		Picker picker;
		HttpClient client;
		List<Product> products;

		public EditScalePage ()
		{
			
			//scaleItem.ShelfName + 
			InitializeComponent ();
			btnUpdate.Clicked += SaveShelf;
			//scaleName.Text = title;
			client = new HttpClient();
			btnScan.Clicked += btnScanClick;
			LoadStatic();

		}
		private async Task LoadStatic()
		{
			
			picker = new Picker();

			await LoadProducts();
			foreach (var p in products)
			{
				picker.Items.Add(p.name);

			}
			staticLayout.Children.Add(picker);
		}
		private async Task LoadProducts()
		{
			try
			{
				var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/main/products", string.Empty));
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					products = JsonConvert.DeserializeObject<List<Product>>(content);





				}
			}
			catch (Exception ex)
			{

			}
		}
		public async void btnScanClick(object obj, EventArgs e)
		{

			var scanner = new ZXing.Mobile.MobileBarcodeScanner();
			var result = await scanner.Scan();

			if (result != null)
				Console.WriteLine("Scanned Barcode: " + result.Text);
		}
		public async void SaveShelf(Object sender, EventArgs e)
		{
			try
			{
				using (var client = new HttpClient())
				{

					StringContent strcontent = new StringContent("");

					HttpResponseMessage response = await client.PostAsync(new Uri("http://smartshelf.mybluemix.net/main/product/register/shelf/" + scaleItem.ShelfId + "/scale/" + scaleItem.ScaleName + "/product/" + (picker.SelectedIndex + 1).ToString()), strcontent);

					if (response.IsSuccessStatusCode)
					{
						int productID = picker.SelectedIndex + 1;
						var product = products.Find(p => p.id == productID.ToString());
						if (product != null)
						{
							lblProduct.Text = product.name;
							double temp = 0;
							if (double.TryParse(product.weight, out temp))
							{
								scaleItem.StartingWeight = temp;
							}

							scaleItem.Name = product.name;
							scaleItem.url = product.url;
							var masterPage = (MasterPage)App.MasterDetail.Master;
							masterPage.SetMyShelves();

						}



					}
				}
				return;
			}
			catch (Exception ex)
			{
				return;
			}
		}
        public ScaleItem ScaleItem
        {
            get { return scaleItem; }
            set { scaleItem = value; this.BindingContext = scaleItem; }
        }
    }
}
