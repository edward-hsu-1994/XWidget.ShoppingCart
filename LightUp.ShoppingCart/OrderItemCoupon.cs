using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    /// <summary>
    /// 訂單項目優惠券
    /// </summary>
    /// <typeparam name="TCouponIdentifier">優惠券唯一識別號</typeparam>
    /// <typeparam name="TOrderItemIdentifier">訂單項目識別號類型</typeparam>
    public class OrderItemCoupon<TCouponIdentifier, TOrderItemIdentifier>
        : ICoupon<TCouponIdentifier> {
        /// <summary>
        /// 作用商品列表
        /// </summary>
        public List<TOrderItemIdentifier> Targets { get; set; } = new List<TOrderItemIdentifier>();

        public TCouponIdentifier Id { get; set; }

        public uint? Count { get; set; }

        public bool IsAvailable(IOrder order) {
            throw new NotImplementedException();
        }

        public virtual void Use(IOrder order) {
            throw new NotImplementedException();
        }
    }
}
