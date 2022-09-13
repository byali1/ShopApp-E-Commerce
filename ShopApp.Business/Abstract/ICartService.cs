﻿using System;
using System.Collections.Generic;
using System.Text;
using ShopApp.Entities;

namespace ShopApp.Business.Abstract
{
    public interface ICartService
    {
        void InitializeCart(string userId);

        Cart GetCartByUserId(string userId);

        void AddToCart(string userId, int productId, int quantity);
        void DeleteItemFromCart(string userId, int productId);
        void ClearCart(string cartId);

    }

}
