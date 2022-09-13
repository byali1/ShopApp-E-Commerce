using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //Bir category için birden fazla ürün olabilir.
        public List<ProductCategory> ProductCategories { get; set; }

    }
}
