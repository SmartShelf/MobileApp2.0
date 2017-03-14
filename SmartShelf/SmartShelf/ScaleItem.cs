using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShelf
{
    public class ScaleItem
    {
        public string Name { get; set; }

        public double CurrentWeight { get; set; }

        public double StartingWeight { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime EstimateRefillDate { get; set; }

        public string ShelfName { get; set; }

        public string ScaleName { get; set; }

        public string ShelfId { get; set; }

        public string ScaleId { get; set; }

		public string url { get; set; }
    }
}
