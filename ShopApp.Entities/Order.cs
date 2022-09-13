﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
    public class Order
    {
        // stripe
        // IYZICO

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }

        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
        public string ConversationId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }

   

    public enum EnumOrderState
    {
        Waiting = 0,
        Approved = 1,
        InCargo = 2,
        Completed = 3
    }

    public enum EnumPaymentTypes
    {
        CreditCart = 0,
        Eft = 1,
        CouponCode = 2,
        Free = 3
    }
}