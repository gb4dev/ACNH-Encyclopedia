using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACNHEncyclopedia.Entities
{
    public class Craft
    {
        public int Id { get; set; }
        public int ItemIdToCraft { get; set; }
        public Dictionary<int, int> ItemsNeeded { get; set; } // key = itemId value = nb needed

        public Item ItemToCraft { get; set; }
        public List<RessourceCraft> ItemsForCraft { get; set; }
    }
}
