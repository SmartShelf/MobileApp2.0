using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class SignInPage : ContentPage
	{
		public SignInPage ()
		{
            this.BindingContext = this;
			InitializeComponent ();
		}

        private void submit_button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var username = this.username.Text;
                var password = this.password.Text;

                if (SmartShelfService.Authenticate(username, password)) {
                    var mainPage = new MainPage();
                    mainPage.Title = "Main";
                    mainPage.Master = new MasterPage();
                    mainPage.Detail = new DashboardPage();
                    mainPage.MasterBehavior = MasterBehavior.Default;

                    App.MasterDetail = mainPage;

                    Application.Current.MainPage = mainPage;
                } else
                {
                    ////TODO: handle failed authentication
                }
            }
            catch (Exception ex)
            {
                var x = this.FindByName<Label>("messageLabel");
                x.Text = string.Format("{0} : {1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
