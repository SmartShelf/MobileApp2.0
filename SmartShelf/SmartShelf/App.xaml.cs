using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new SmartShelf.SignInPage();
            //MainPage = new SmartShelf.MainPage();

            try
            {
                //MainPage = new SmartShelf.MainPage();
                //MainPage = new NavigationPage(new SmartShelf.SignInPage(""));


                //var mainPage = new MainPage();
                //mainPage.Title = "Main";
                //mainPage.Master = new SSNavPage();
                //mainPage.Detail = new SignInPage();
                //mainPage.MasterBehavior = MasterBehavior.Split;


                //MasterDetail = mainPage;
                MainPage = new SignInPage();
            }
            catch (Exception e)
            {
                throw;
                //var message = e.Message + " : " + e.StackTrace;
                //var signInPage = new SmartShelf.SignInPage(message);
                //MainPage = new NavigationPage(signInPage);
            }
            
        }

        public static MasterDetailPage MasterDetail
        {
            get;
            set;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
