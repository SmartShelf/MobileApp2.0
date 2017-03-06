using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class ScaleItemContentView : Frame
	{
		public ScaleItemContentView (ScaleItem scaleItem)
		{
            this.BindingContext = scaleItem;
            this.Item = scaleItem;
			InitializeComponent ();

            var pie = new Cross.Pie.Forms.CrossPie();
            pie.Title = string.Empty;
            pie.HeightRequest = 150;
            pie.WidthRequest = 150;
            pie.IsPercentVisible = false;
            pie.IsTitleOnTop = false;
            pie.IsValueVisible = false;
            pie.StartAngle = -90;
            pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = scaleItem.CurrentWeight, Color = Color.Green});
            pie.Add(new Cross.Pie.Forms.PieItem { Title = string.Empty, Value = (scaleItem.StartingWeight - scaleItem.CurrentWeight), Color = Color.Gray });
            pie.Update();

            pieGraphContentView.Content = pie;

            var infoImage = new Image();
            infoImage.Source = "info_circle_18_36.png";

            AbsoluteLayout.SetLayoutBounds(infoImage, new Rectangle(1f, 0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(infoImage, AbsoluteLayoutFlags.PositionProportional);
            topAbsoluteLayout.Children.Add(infoImage);

            var shoppingCartImage = new Image();
            shoppingCartImage.Source = "shopping_cart_plus_36.png";

            AbsoluteLayout.SetLayoutBounds(shoppingCartImage, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(shoppingCartImage, AbsoluteLayoutFlags.PositionProportional);
            bottomAbsoluteLayout.Children.Add(shoppingCartImage);
        }

        public ScaleItem Item { get; set; }
	}
}
