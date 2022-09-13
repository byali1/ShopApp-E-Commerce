using System;
using System.Collections.Generic;
using System.Text;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Abstract
{
    public interface IOrderDal:IRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
