using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartShelf.Entities;

namespace SmartShelf
{
    public static class SmartShelfService
    {
		
        private static IList<ShelfItem> shelfItems = new List<ShelfItem>();

		private static SmartShelfDoc userDoc = new SmartShelfDoc();

		private static List<Product> products = new List<Product>();

		private static async Task LoadProducts(HttpClient client)
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
				return;
			}
		}
		public static async Task DoAsyncPut(string url, string postData)
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
        public static async Task<bool> Authenticate(string userName, string password, HttpClient client)
        {
			try
			{
				var uri = new Uri(string.Format("http://smartshelf.mybluemix.net/main/loginGetDoc?username={0}&password={1}", userName, password));
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					userDoc = JsonConvert.DeserializeObject<SmartShelfDoc>(content);

					string perc = "";
					long tempPerc = 0;

					ShelfItem tempShelf;
					ScaleItem tempScale;
					double tmpDouble;
					DateTime tmpDate;
					shelfItems.Clear();
					foreach (var shelf in userDoc.shelfs)
					{
						tempShelf = new ShelfItem();
						tempShelf.Id = shelf.id;
						tempShelf.Name = shelf.name;

						tempShelf.Scales = new List<ScaleItem>();

						foreach (var scale in shelf.scales)
						{
							tempScale = new ScaleItem();
							tempScale.ScaleId = scale.id.ToString();
							tempScale.Name = scale.id.ToString();
							tmpDouble = 0;
							if (double.TryParse(scale.weight, out tmpDouble))
							{
								tempScale.CurrentWeight = tmpDouble;
							}
							else
							{
								tempScale.CurrentWeight = 0;
							}
							tmpDate = DateTime.Now.AddDays(7);
							if (DateTime.TryParse(scale.estimatedDate, out tmpDate))
							{
								tempScale.EstimateRefillDate = tmpDate;
							}
							else
							{
								tempScale.EstimateRefillDate = DateTime.Now.AddDays(7);
							}

							if (DateTime.TryParse(scale.registerDate, out tmpDate))
							{
								tempScale.StartingDate = tmpDate;
							}
							else
							{
								tempScale.StartingDate = DateTime.Now;
							}

							double calcPerc = 0;
							double temp;
							await LoadProducts(client);
							if (!string.IsNullOrEmpty(scale.productId))
							{
								var product = products.Where(p => p.id == scale.productId).FirstOrDefault();
								if (product != null)
								{
									if (double.TryParse(product.weight, out temp))
									{
										tempScale.StartingWeight = temp;
									}
									if (temp != 0)
									{
										calcPerc = tempScale.CurrentWeight * 100 / temp;
										perc = string.Format("{0:0.00}", calcPerc) + "%";

									}
									tempScale.Name = product.name;
									tempScale.url = product.url;
								}
							}
							tempScale.ScaleName = scale.id.ToString();
							tempScale.ShelfName = tempShelf.Name;
							tempScale.ShelfId = tempShelf.Id;
							tempShelf.Scales.Add(tempScale);

						}
						shelfItems.Add(tempShelf);
					}

					return true;
				}
				else
				{
					
					return false;

				}
			}
			catch (Exception ex)
			{
				return false;
			}
        }

		public static IList<ShelfItem> Shelves { get { return shelfItems; } }

        private static IList<ShelfItem> GetShelfItems()
        {
            var shelfItems = new List<ShelfItem>();

            var shelfItem = new ShelfItem()
            {
                Name = "Pantry Top",
                Id = Guid.NewGuid().ToString()
            };

            shelfItem.Scales = new List<ScaleItem>();
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Coffee",
                CurrentWeight = 254.0,
                StartingWeight = 1000.0,
                StartingDate = DateTime.Parse("1/15/2016"),
                EstimateRefillDate = DateTime.Parse("3/4/2018"),
                ScaleName = "Scale 1",
                ShelfName = shelfItem.Name
            });
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Candy",
                CurrentWeight = 355.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 2",
                ShelfName = shelfItem.Name
            });
            shelfItems.Add(shelfItem);

            shelfItem = new ShelfItem()
            {
                Name = "Pantry Middle",
                Id = Guid.NewGuid().ToString()
            };

            shelfItem.Scales = new List<ScaleItem>();
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Popcorn",
                CurrentWeight = 591.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 1",
                ShelfName = shelfItem.Name
            });
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Quaker Oats",
                CurrentWeight = 254.0,
                StartingWeight = 1000.0,
                StartingDate = DateTime.Parse("1/15/2016"),
                EstimateRefillDate = DateTime.Parse("3/4/2018"),
                ScaleName = "Scale 2",
                ShelfName = shelfItem.Name
            });
            shelfItems.Add(shelfItem);

            shelfItem = new ShelfItem()
            {
                Name = "Pantry Bottom",
                Id = Guid.NewGuid().ToString()
            };

            shelfItem.Scales = new List<ScaleItem>();
			shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Tang",
                CurrentWeight = 355.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 1",
                ShelfName = shelfItem.Name
            });
			shelfItem.Scales.Add(new ScaleItem
			{
				Name = "Bread",
				CurrentWeight = 355.0,
				StartingWeight = 700.0,
				StartingDate = DateTime.Parse("1/15/2017"),
				EstimateRefillDate = DateTime.Parse("3/4/2017"),
				ScaleName = "Scale 2",
				ShelfName = shelfItem.Name
			});
            shelfItems.Add(shelfItem);

            return shelfItems;
        }

        public static IList<ScaleItem> GetScaleItemsForDashboard(ScaleOrderby orderBy)
        {
            var scaleItems = new List<ScaleItem>();
           
            foreach (var shelfItem in shelfItems)
            {
                scaleItems.AddRange(shelfItem.Scales);
            }

            ////TODO - add order by

            return scaleItems;
        }
    }
}
