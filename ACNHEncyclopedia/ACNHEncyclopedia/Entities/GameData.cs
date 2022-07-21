using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACNHEncyclopedia.Entities
{
    public class GameData
    {
        public List<Kind> Kinds { get; set; }
        public List<Item> Items { get; set; }
        public List<Craft> Crafts { get; set; }

        public GameData()
        {

        }
    }
}
