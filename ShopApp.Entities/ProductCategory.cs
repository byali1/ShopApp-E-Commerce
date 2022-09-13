using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
    public class ProductCategory
    {
        //Burada id'ler birleştirilecek.

        /* junction tablo

         many to many relationshipleri dbmse aktarirken aradaki
        bu coklu iliskiyi aktarabilmek icin 2 one to many
        relationshipin ortasinda* yer alan ve bagli oldugu 
        tablolarin primary keylerini foreign key olarak 
        alinarak olusturulan tablo.

        */

        
        public int CategoryId { get; set; }
        public Category Category { get; set; } //Id'si 3 olan category'nin bilgisi için
        public Product Product { get; set; }
        public int ProductId { get; set; }

        
    }
}
