using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
    public class Cart
    {
        public int  Id { get; set; }
        public string UserId = "9beeda6c-390c-4a51-a587-cf00690b48fa";
        //public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
