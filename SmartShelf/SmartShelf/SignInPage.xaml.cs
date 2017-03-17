using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class SignInPage : ContentPage
	{
		HttpClient client;

		public SignInPage ()
		{
			client = new HttpClient();
            this.BindingContext = this;

			InitializeComponent ();

            var indicator = new ActivityIndicator()
            {
                Color = Color.Black
            };
            indicator.SetBinding(VisualElement.IsVisibleProperty, new Binding("IsBusy", BindingMode.OneWay, source: this));
            indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsBusy", BindingMode.OneWay, source: this));
            AbsoluteLayout.SetLayoutFlags(indicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(indicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absoluteLayout.Children.Add(indicator);
        }

        private async void submit_button_Clicked(object sender, EventArgs e)
        {
            try
            {
                this.IsBusy = true;
                var username = this.username.Text;
                var password = this.password.Text;
				var isAuth = await SmartShelfService.Authenticate(username, password, client);
                if (isAuth) {
                    var mainPage = new MainPage();
                    mainPage.Title = "Main";
                    mainPage.Master = new MasterPage();
                    mainPage.MasterBehavior = MasterBehavior.Default;

                    App.MasterDetail = mainPage;
					App.MasterDetail.Detail = new NavigationPage(new DashboardPage());

                    Application.Current.MainPage = mainPage;
                }
                else
                {					
                    //LoginMessage.Text = "Login unsuccessful. Please try again.";
                    await DisplayAlert("", "Login unsuccessful.Please try again.", "Close");
                }
            }
            catch (Exception ex)
            {
				
                var x = this.FindByName<Label>("messageLabel");
                x.Text = string.Format("{0} : {1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                this.IsBusy = false;
            }
        }
		private async void register_button_Clicked(object sender, EventArgs e)
		{
			try
			{
				
		        //LoginMessage.Text = "Registration unavailable at the moment... Work in progress";
                await DisplayAlert("", "Registration unavailable at the moment... Work in progress", "Close");

            }
			catch (Exception ex)
			{
				var x = this.FindByName<Label>("messageLabel");
				x.Text = string.Format("{0} : {1}", ex.Message, ex.StackTrace);
			}
		}
    }
}
