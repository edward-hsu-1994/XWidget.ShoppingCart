using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.Cashier {
    public class CouponOrderItem : OrderItem {
        public Type CouponType { get; internal set; }
    }
}
