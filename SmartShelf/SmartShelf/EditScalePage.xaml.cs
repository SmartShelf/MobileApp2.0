using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;

using Xamarin.Forms;

namespace SmartShelf
{
	public partial class EditScalePage : ContentPage
	{
        private ScaleItem scaleItem;

		public EditScalePage ()
		{
			InitializeComponent ();
			btnScan.Clicked += btnScanClick;
		}
		public async void btnScanClick(object obj, EventArgs e)
		{

			var scanner = new ZXing.Mobile.MobileBarcodeScanner();
			var result = await scanner.Scan();

			if (result != null)
				Console.WriteLine("Scanned Barcode: " + result.Text);
		}

        public ScaleItem ScaleItem
        {
            get { return scaleItem; }
            set { scaleItem = value; this.BindingContext = scaleItem; }
        }
    }
}
