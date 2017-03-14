using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class EditScalePage : ContentPage
	{
        private ScaleItem scaleItem;

		public EditScalePage ()
		{
			InitializeComponent ();
		}

        public ScaleItem ScaleItem
        {
            get { return scaleItem; }
            set { scaleItem = value; this.BindingContext = scaleItem; }
        }
    }
}
