﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal:EfCoreGenericRepository<Cart,ShopContext>,ICartDal
    {
        public override void Update(Cart entity)
        {
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }


        public Cart GetCartByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.Carts.Include(i => i.CartItems)
                    .ThenInclude(i => i.Product).FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteItemFromCart(int cartId, int productId)
        {
            using (var context = new ShopContext())
            {
                var sqlCommand = @"delete from CartItem where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlCommand(sqlCommand,cartId,productId);
            }
        }

        public void ClearCart(string cartId)
        {
            using (var context = new ShopContext())
            {
                var sqlCommand = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlCommand(sqlCommand, cartId);
            }
        }
    }
}
