using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartShelf.Entities;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class ScaleItemContentView : Frame
	{
		HttpClient client;
		
		public ScaleItemContentView(ScaleItem scaleItem)
		{
			client = new HttpClient();
            this.BindingContext = scaleItem;
            this.Item = scaleItem;
			InitializeComponent();

            var pie = new Cross.Pie.Forms.CrossPie();
            pie.Title = string.Empty;
            pie.HeightRequest = 150;
            pie.WidthRequest = 150;
            pie.IsPercentVisible = false;
            pie.IsTitleOnTop = false;
            pie.IsValueVisible = false;
            pie.StartAngle = -90;
            pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = scaleItem.CurrentWeight, Color = Color.Green});
            pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = (scaleItem.StartingWeight - scaleItem.CurrentWeight), Color = Color.Gray });
            pie.Update();

            pieGraphContentView.Content = pie;

            var infoImage = new Image();
            infoImage.Source = "info_circle_18_36.png";
            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += (s, e) =>
            {
                var editScalePage = new EditScalePage();
                editScalePage.ScaleItem = scaleItem;
                App.MasterDetail.Detail = new NavigationPage(new EditScalePage());
            };
            infoImage.GestureRecognizers.Add(tapGestureRecognizer1);


            AbsoluteLayout.SetLayoutBounds(infoImage, new Rectangle(1f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(infoImage, AbsoluteLayoutFlags.PositionProportional);
            topAbsoluteLayout.Children.Add(infoImage);

            var shoppingCartImage = new Image();
            shoppingCartImage.Source = "shopping_cart_plus_36.png";
			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				Device.OpenUri(new Uri(scaleItem.url));
			};
			shoppingCartImage.GestureRecognizers.Add(tapGestureRecognizer);
            shoppingCartImage.Margin = new Thickness(0, 0, 5, 5);
            AbsoluteLayout.SetLayoutBounds(shoppingCartImage, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(shoppingCartImage, AbsoluteLayoutFlags.PositionProportional);
            topAbsoluteLayout.Children.Add(shoppingCartImage);
			var seconds = TimeSpan.FromSeconds(5);

			Device.StartTimer(seconds, () =>
			{
				try
				{
					 ReloadScale(scaleItem, scaleItem.ShelfId);

					//lblWeight.Text = lblWeight.Text + ".";
					// call your method to check for notifications here
					//LoadScales();
					// Returning true means you want to repeat this timer
					return true;
				}
				catch (Exception ex)
				{
					return true;
				}
			});
        }

		public async void ReloadScale(ScaleItem scaleItem, string shelfID)
		{
			var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/main/shelf/{0}", shelfID));
			var response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var shelf = JsonConvert.DeserializeObject<Shelf>(content);
				for (int i = 0; i < shelf.scales.Count; i++)
				{
					Scale s = shelf.scales[i];
					if (s.id.ToString() == scaleItem.ScaleId)
					{
						double tempDouble = 0;
						double weight1 = 0;

						if (double.TryParse(s.weight, out tempDouble))
							weight1 = tempDouble;
						
						lblWeight.Text = string.Format("Current Weight: {0}g", s.weight);
						var pie = new Cross.Pie.Forms.CrossPie();
						pie.Title = string.Empty;
						pie.HeightRequest = 150;
						pie.WidthRequest = 150;
						pie.IsPercentVisible = false;
						pie.IsTitleOnTop = false;
						pie.IsValueVisible = false;
						pie.StartAngle = -90;
						pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = weight1, Color = Color.Green });
						pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = (scaleItem.StartingWeight - weight1), Color = Color.Gray });
						pie.Update();

						pieGraphContentView.Content = pie;

					}
				}
			}
		}

        public ScaleItem Item { get; set; }
	}
}
