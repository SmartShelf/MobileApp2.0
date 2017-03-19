using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartShelf.Entities;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;
using System.Threading;

namespace SmartShelf
{
	public partial class ScaleItemContentView : Frame
	{
		HttpClient client;
		CancellationTokenSource cts;
		CancellationTokenSource cts2;
		public bool startTimer = true;
		public bool showWatsonMessage = false;
		public ScaleItemContentView(ScaleItem scaleItem)
		{
			try
			{
				client = new HttpClient();
				this.BindingContext = scaleItem;
				this.Item = scaleItem;
				InitializeComponent();
				double percent = 0;
				if (scaleItem.CurrentWeight > 0 && scaleItem.StartingWeight > 0)
				{
					percent = 100 * (scaleItem.CurrentWeight / scaleItem.StartingWeight);
				}
				Color pieColor = Color.Green;
				if (percent < 15)
				{
					pieColor = Color.Red;
				}
				else if (percent < 50)
				{

					pieColor = Color.Yellow;

				}

				var pie = new Cross.Pie.Forms.CrossPie();
				pie.Title = string.Empty;
				pie.HeightRequest = 150;
				pie.WidthRequest = 150;
				pie.IsPercentVisible = false;
				pie.IsTitleOnTop = false;
				pie.IsValueVisible = false;
				pie.StartAngle = -90;
				pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = scaleItem.CurrentWeight, Color = pieColor });
				pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = (scaleItem.StartingWeight - scaleItem.CurrentWeight), Color = Color.Gray });
				pie.Update();

				pieGraphContentView.Content = pie;

				var infoImage = new Image();
				infoImage.Source = "info_circle_18_36.png";
				var tapGestureRecognizer1 = new TapGestureRecognizer();
				tapGestureRecognizer1.Tapped += (s, e) =>
				{
					startTimer = false;
					if (cts != null)
					{
						cts.Cancel();

					}
					var editScalePage = new EditScalePage();
					editScalePage.ScaleItem = scaleItem;

					App.MasterDetail.Detail = new NavigationPage(editScalePage);
				};
				infoImage.GestureRecognizers.Add(tapGestureRecognizer1);


				AbsoluteLayout.SetLayoutBounds(infoImage, new Rectangle(1f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
				AbsoluteLayout.SetLayoutFlags(infoImage, AbsoluteLayoutFlags.PositionProportional);
				topAbsoluteLayout.Children.Add(infoImage);

				// Watson predictive service
				var watsonImage = new Image();
				watsonImage.Source = "watson_36.png";
				var tapGestureRecognizerWatson = new TapGestureRecognizer();
				tapGestureRecognizerWatson.Tapped += (s, e) =>
				{

					RunWatsonService(scaleItem);
				};
				watsonImage.GestureRecognizers.Add(tapGestureRecognizerWatson);


				AbsoluteLayout.SetLayoutBounds(watsonImage, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
				watsonImage.Margin = new Thickness(0, 0, 105, 5);
				AbsoluteLayout.SetLayoutFlags(watsonImage, AbsoluteLayoutFlags.PositionProportional);
				topAbsoluteLayout.Children.Add(watsonImage);



				var shoppingCartImage = new Image();
				shoppingCartImage.Source = "shopping_cart_plus_36.png";
				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.Tapped += (s, e) =>
				{
					startTimer = false;
					if (cts != null)
					{
						cts.Cancel();

					}
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
					if (startTimer)
					{
						cts = new CancellationTokenSource();
						try
						{
							ReloadScale(scaleItem, scaleItem.ShelfId);

							//lblWeight.Text = lblWeight.Text + ".";
							// call your method to check for notifications here
							//LoadScales();
							// Returning true means you want to repeat this timer
							cts = null;
							return true;

						}
						catch (Exception ex)
						{
							return true;
						}
					}
					else
					{
						return false;
					}
				});
			}
			catch (Exception ex)
			{
				//do nothing
			}
		}
		public async void RunWatsonService(ScaleItem scaleItem)
		{
			var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/prediction/user/demouser/shelf/{0}/scale/{1}", scaleItem.ShelfId, scaleItem.ScaleId));
			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var prediction = JsonConvert.DeserializeObject<WatsonNotification>(content);
					if (prediction.showNotification)
					{
						lblPredication.TextColor = Color.Red;
						lblPredication.Text = "Product is almost out, click on cart to order more!";
					}
					else
					{
						
						lblPredication.TextColor = Color.Blue;
						lblPredication.Text = "Product supply is ok.";

					}
					var seconds = TimeSpan.FromSeconds(10);

					Device.StartTimer(seconds, () =>
					{
						
						lblPredication.Text = "";
						return false;
					});
				}
				else
				{
					
				}
			}

			catch (Exception ex)
			{
				// do nothing
				return;
			}

		}
		public async void ReloadScale(ScaleItem scaleItem, string shelfID)
		{
			var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/main/shelf/{0}", shelfID));
			try
			{
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
							this.BindingContext = scaleItem;
							this.Item = scaleItem;
							double tempDouble = 0;
							double weight1 = 0;

							if (double.TryParse(s.weight, out tempDouble))
								weight1 = tempDouble;
							double percent = 0;
							if (weight1 > 0 && scaleItem.StartingWeight > 0)
							{
								percent = 100 * (weight1 / scaleItem.StartingWeight);
							}
							Color pieColor = Color.Green;
							if (percent < 15)
							{
								pieColor = Color.Red;
							}
							else if (percent < 50)
							{

								pieColor = Color.Yellow;

							}
							lblWeight.Text = string.Format("Current Weight: {0}g", s.weight);
							var pie = new Cross.Pie.Forms.CrossPie();
							pie.Title = string.Empty;
							pie.HeightRequest = 150;
							pie.WidthRequest = 150;
							pie.IsPercentVisible = false;
							pie.IsTitleOnTop = false;
							pie.IsValueVisible = false;
							pie.StartAngle = -90;
							pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = weight1, Color = pieColor });
							pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = (scaleItem.StartingWeight - weight1), Color = Color.Gray });
							pie.Update();

							pieGraphContentView.Content = pie;

						}
					}
				}
			}

			catch (Exception ex)
			{
				// do nothing
				return;
			}

		}
	
        public ScaleItem Item { get; set; }
	}
}
