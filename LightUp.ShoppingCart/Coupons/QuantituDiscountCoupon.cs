using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart.Coupons {
    /// <summary>
    /// 數量折扣
    /// </summary>
    /// <typeparam name="TCouponIdentifier">優惠券唯一識別號</typeparam>
    /// <typeparam name="TOrderItemIdentifier">訂單項目識別號類型</typeparam>
    public class QuantituDiscountCoupon<TCouponIdentifier, TOrderItemIdentifier>
        : ICoupon<TCouponIdentifier> {
        /// <summary>
        /// 唯一識別號
        /// </summary>
        public virtual TCouponIdentifier Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 可用數量
        /// </summary>
        public virtual uint? Count { get; set; }

        /// <summary>
        /// 折價金額
        /// </summary>
        public virtual decimal DiscountPrice { get; set; }

        /// <summary>
        /// 目標折價數量
        /// </summary>
        public virtual uint Quantity { get; set; }

        /// <summary>
        /// 檢驗此訂單是否可使用這個優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否可使用</returns>
        public virtual bool IsAvailable(IOrder order) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order"></param>
        public virtual void Use(IOrder order) {
            throw new NotImplementedException();
        }
    }
}
