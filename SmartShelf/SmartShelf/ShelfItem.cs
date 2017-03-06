using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShelf
{
    public class ShelfItem
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public IList<ScaleItem> Scales { get; set; }
    }
}
