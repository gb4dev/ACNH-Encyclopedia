using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ACNHEncyclopedia.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public int SellPrice { get { return Price / 4; } }
        public string KindExtended { get; set; }
        public int KindId { get; set; }
        public Kind Kind { get; set; }
        public string TranslateId { get { return $"{Kind?.Name}_{Id}"; } }
        public string TranslatedName { get; set; }

        public Item()
        {

        }


        // App Models
        public ImageSource Image { get; set; }
    }
}
