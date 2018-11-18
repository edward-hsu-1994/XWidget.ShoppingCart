using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.Cashier {
    public class CouponOrderItem<TId> : OrderItem<TId> {
        public Type CouponType { get; internal set; }
    }
}
