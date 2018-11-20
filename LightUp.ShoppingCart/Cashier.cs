using System;
using System.Collections.Generic;
using System.Linq;

namespace LightUp.ShoppingCart {
    public class Cashier {
        public virtual void Checkout(IOrder order, IEnumerable<ICoupon> coupons) {
            IEnumerable<ICoupon> allowCoupons = null;
            while ((allowCoupons = coupons.Where(x => x.IsAvailable(order))).Count() > 0) {
                foreach (var coupon in allowCoupons) {
                    if (coupon.IsAvailable(order)) { // 依舊必須要在檢查一次
                        coupon.Use(order);
                    }
                }
            }
        }
    }
}
