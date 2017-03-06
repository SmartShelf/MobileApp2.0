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
