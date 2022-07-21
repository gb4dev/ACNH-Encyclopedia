using System;
using System.Collections.Generic;
using System.Text;

namespace ACNHEncyclopedia.Entities
{
    public class Category
    {
        public static List<Category> Categories = new List<Category>()
        {
            new Category()
            {
                Name = "Mobilier",
                CategoryKinds = new List<string>()
                {
                    "Ftr", "Fish", "Insect"
                }
            },
            new Category()
            {
                Name = "Vetements",
                CategoryKinds = new List<string>()
                {
                    "Accessory", "Bag", "Bottoms", "Cap", "Halmet", "OnePiece", "Shoes", "Socks", "Tops"
                }
            }
        };

        /// <summary>
        /// Regroupe plusieur item kind pour former une catégorie
        /// </summary>
        public Category()
        {

        }
        public string Name { get; set; }
        public List<string> CategoryKinds { get; set; }
    }
}
