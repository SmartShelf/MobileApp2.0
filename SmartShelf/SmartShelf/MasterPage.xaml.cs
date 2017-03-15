using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class MasterPage : ContentPage
	{
		public MasterPage()
		{
			InitializeComponent();
            SetStaticMasterPageItems();
            SetMyShelves();
        }

        private void SetStaticMasterPageItems()
        {
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Dashboard",
                IconSource = "dashboard_36_72.png",
                TargetType = typeof(DashboardPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Add a SmartShelf",
                IconSource = "plus_circle_36_72.png",
                TargetType = typeof(AddSmartShelfPage)
            });
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Logout",
				IconSource = "plus_circle_36_72.png",
				TargetType = typeof(SignInPage)
			});

            staticListView.ItemsSource = masterPageItems;

            staticListView.ItemSelected += StaticListView_ItemSelected;
        }


        private void SetMyShelves()
        {
            var shelfItems = SmartShelfService.Shelves;

            var masterPageItems = new List<MasterPageItem>();

            foreach (var shelfItem in shelfItems)
            {
                masterPageItems.Add(new MasterPageItem
                {
                    Title = shelfItem.Name,
                    IconSource = string.Empty,
                    TargetType = typeof(EditSmartShelfPage),
                    Data = shelfItem
                });

                foreach (var scaleItem in shelfItem.Scales)
                {
                    masterPageItems.Add(new MasterPageItem
                    {
                        Title = string.Format("          {0} - {1}", scaleItem.Name, scaleItem.ScaleName),
                        IconSource = string.Empty,
                        TargetType = typeof(EditScalePage),
                        Data = scaleItem
                    });
                }
            }

            shelvesListView.ItemsSource = masterPageItems;

            shelvesListView.ItemSelected += ShelvesListView_ItemSelected;
        }

        private void ShelvesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
               
                if (page.GetType() == typeof(EditScalePage))
                {
                    ((EditScalePage)page).ScaleItem = (ScaleItem)item.Data;
                }
                else
                {
                    ((EditSmartShelfPage)page).ShelfItem = (ShelfItem)item.Data;
                }

				App.MasterDetail.Detail = new NavigationPage(page);
				shelvesListView.SelectedItem = null;
				App.MasterDetail.IsPresented = false;

            }
        }

        private void StaticListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
				if (item.TargetType == typeof(SignInPage))
				{
					App.Current.MainPage = new SignInPage();

				}
				else
				{
					App.MasterDetail.Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
					staticListView.SelectedItem = null;
					App.MasterDetail.IsPresented = false;
				}
            }
        }
    }
}
