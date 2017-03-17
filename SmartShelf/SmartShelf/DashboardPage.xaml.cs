using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
            SetScaleItems();

           // DoToolbar();
		}

        private void DoToolbar()
        {

            var toolbarItem = new ToolbarItem("Menu", "ellipsis_18_36.png", async () => {
                string result = await DisplayActionSheet("", "Cancel", null, "Logout");

                if (result == "Logout")
                {
                    App.Current.MainPage = new SignInPage();
                }
            });

            ToolbarItems.Add(toolbarItem);
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
