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
		}

        private async void submit_button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var username = this.username.Text;
                var password = this.password.Text;
				var isAuth = await SmartShelfService.Authenticate(username, password, client);
                if (isAuth) {
                    var mainPage = new MainPage();
                    mainPage.Title = "Main";
                    mainPage.Master = new MasterPage();
                    //mainPage.Detail = new DashboardPage();
                    mainPage.MasterBehavior = MasterBehavior.Default;

                    App.MasterDetail = mainPage;
					App.MasterDetail.Detail = new NavigationPage(new DashboardPage());

                    Application.Current.MainPage = mainPage;
                } else
                {
                    LoginMessage.Text = "Login unsuccessful. Please try again.";

                }
            }
            catch (Exception ex)
            {
                var x = this.FindByName<Label>("messageLabel");
                x.Text = string.Format("{0} : {1}", ex.Message, ex.StackTrace);
            }
        }
		private async void register_button_Clicked(object sender, EventArgs e)
		{
			try
			{
				
					LoginMessage.Text = "Registration unavailable at the moment... Work in progress";


			}
			catch (Exception ex)
			{
				var x = this.FindByName<Label>("messageLabel");
				x.Text = string.Format("{0} : {1}", ex.Message, ex.StackTrace);
			}
		}
    }
}
