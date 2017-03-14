using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class EditSmartShelfPage : ContentPage
	{
        private ShelfItem shelfItem;

        public EditSmartShelfPage ()
		{
			InitializeComponent ();
		}

        public ShelfItem ShelfItem
        {
            get { return shelfItem; }
            set { shelfItem = value; this.BindingContext = shelfItem; }
        }
    }
}
