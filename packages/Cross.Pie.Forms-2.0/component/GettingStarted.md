# Getting Started with Cross.Pie.Forms

#STEP 1 - Create and Set Properties of CrossPie

#STEP 2 - Create new PieItem object per segment and set properties 
## Title - is Name of each pie segment
## Value - is Value of each segment

#STEP 3 - Refresh(Redraw) Pie chart using 'Update' method.

#See Sample Code of a page with a pie chart.

    using Xamarin.Forms;
    using Cross.Pie.Forms
    namespace Sample
    {
        public class SimplestPage : ContentPage
        {
            CrossPie Pie { get; set; }

            public SimplestPage ()
            {
                Content = Pie = new CrossPie();
                AddItems ();
            }
    
            void AddItems ()
            {
                Pie.Add (new PieItem { Title="one",  Value = 1.5});
                Pie.Add (new PieItem { Title="two",  Value = 2});
                Pie.Add (new PieItem { Title="three",Value = 2.5});
                Pie.Update ();
            }
        }
    }

#STEP 4 - On Android Project
  1. Add NuGet Package 'NControl'
  2. On MainActivity.OnCreate Method
  3. Add 'NControlViewRenderer.Init ();' before 'LoadApplication (new App ());'

#See sample code

    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using NControl.Droid;
    using Cross.Pie.Forms;

    namespace Sample.Droid
    {
        [Activity (Label = "Sample.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
        public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
        {
            protected override void OnCreate (Bundle bundle)
            {
                base.OnCreate (bundle);
                global::Xamarin.Forms.Forms.Init (this, bundle);

                NControlViewRenderer.Init ();   //Add this line <-------------
    
                LoadApplication (new App ());
            }
        }
    }

#STEP 5 - On iOS Project
  1. Add NuGet Package 'NControl'
  2. On AppDelegate.FinishedLaunching Method
  3. Add 'NControlViewRenderer.Init ();' before 'LoadApplication (new App ());'

#See sample code

    using Cross.Pie.Forms.Sample;
    using Foundation;
    using UIKit;
    using NControl.iOS;
    
    namespace Sample.iOS
    {
        [Register ("AppDelegate")]
        public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
        {
            public override bool FinishedLaunching (UIApplication app, NSDictionary options)
            {
                global::Xamarin.Forms.Forms.Init ();
                
                NControlViewRenderer.Init (); //Add this line <----------
    
                LoadApplication (new App ());
                
                return base.FinishedLaunching (app, options);
            }
        }
    }

#SUCCESS	
