using System;

using Xamarin.Forms;

namespace Cross.Pie.Forms.Sample
{
	public class SimplestPage : ContentPage
	{
		CrossPie Pie { get; set; }

		public SimplestPage ()
		{
			Content = Pie = new CrossPie();
			Pie.BackgroundColor = Color.White;
			AddItems ();
		}

		void AddItems ()
		{
			Pie.Add (new PieItem { Title="one", Value = 1.5});
			Pie.Add (new PieItem { Title="two",Value = 2});
			Pie.Add (new PieItem { Title="three",Value = 2.5});
			Pie.Add (new PieItem { Title="four",Value = 3.5});
			Pie.Update ();
		}
	}
}


