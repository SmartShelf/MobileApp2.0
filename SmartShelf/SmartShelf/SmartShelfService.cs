using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShelf
{
    public static class SmartShelfService
    {
        private static IList<ShelfItem> shelfItems = new List<ShelfItem>();

        public static bool Authenticate(string userName, string password)
        {
            shelfItems = GetShelfItems();  ////TODO - replace call to real service
            return true;
        }

        public static IList<ShelfItem> Shelves { get { return shelfItems; } }
        

        private static IList<ShelfItem> GetShelfItems()
        {
            var shelfItems = new List<ShelfItem>();

            var shelfItem = new ShelfItem()
            {
                Name = "Pantry Top",
                Id = Guid.NewGuid().ToString()
            };

            shelfItem.Scales = new List<ScaleItem>();
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Coffee",
                CurrentWeight = 254.0,
                StartingWeight = 1000.0,
                StartingDate = DateTime.Parse("1/15/2016"),
                EstimateRefillDate = DateTime.Parse("3/4/2018"),
                ScaleName = "Scale 1",
                ShelfName = shelfItem.Name
            });
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Candy",
                CurrentWeight = 355.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 2",
                ShelfName = shelfItem.Name
            });
            shelfItems.Add(shelfItem);

            shelfItem = new ShelfItem()
            {
                Name = "Pantry Middle",
                Id = Guid.NewGuid().ToString()
            };

            shelfItem.Scales = new List<ScaleItem>();
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Popcorn",
                CurrentWeight = 591.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 1",
                ShelfName = shelfItem.Name
            });
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Quaker Oats",
                CurrentWeight = 254.0,
                StartingWeight = 1000.0,
                StartingDate = DateTime.Parse("1/15/2016"),
                EstimateRefillDate = DateTime.Parse("3/4/2018"),
                ScaleName = "Scale 2",
                ShelfName = shelfItem.Name
            });
            shelfItems.Add(shelfItem);

            shelfItem = new ShelfItem()
            {
                Name = "Pantry Bottom",
                Id = Guid.NewGuid().ToString()
            };

            shelfItem.Scales = new List<ScaleItem>();
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Tang",
                CurrentWeight = 355.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 1",
                ShelfName = shelfItem.Name
            });
            shelfItem.Scales.Add(new ScaleItem
            {
                Name = "Bread",
                CurrentWeight = 355.0,
                StartingWeight = 700.0,
                StartingDate = DateTime.Parse("1/15/2017"),
                EstimateRefillDate = DateTime.Parse("3/4/2017"),
                ScaleName = "Scale 2",
                ShelfName = shelfItem.Name
            });
            shelfItems.Add(shelfItem);

            return shelfItems;
        }

        public static IList<ScaleItem> GetScaleItemsForDashboard(ScaleOrderby orderBy)
        {
            var scaleItems = new List<ScaleItem>();
           
            foreach (var shelfItem in shelfItems)
            {
                scaleItems.AddRange(shelfItem.Scales);
            }

            ////TODO - add order by

            return scaleItems;
        }
    }
}
