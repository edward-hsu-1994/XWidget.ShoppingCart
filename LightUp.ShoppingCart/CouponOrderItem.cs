using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    public class CouponOrderItem : IOrderItem {
        public ICoupon Coupon { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Count { get; set; }
        public decimal Amount => Price * Count;
    }
}
