using System;
using System.Collections.Generic;
using System.Text;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICartDal:IRepository<Cart>
    {
        Cart GetCartByUserId(string userId);
        void DeleteItemFromCart(int cartId, int productId);
        void ClearCart(string cartId);
    }
}
