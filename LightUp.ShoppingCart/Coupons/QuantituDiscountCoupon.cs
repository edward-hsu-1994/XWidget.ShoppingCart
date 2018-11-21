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
        : OrderItemCouponBase<TCouponIdentifier, TOrderItemIdentifier> {
        /// <summary>
        /// 折價金額
        /// </summary>
        public virtual decimal DiscountPrice { get; set; }

        /// <summary>
        /// 目標折價數量
        /// </summary>
        public virtual uint DiscountQuantity { get; set; }

        /// <summary>
        /// 檢驗此訂單是否可使用這個優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否可使用</returns>
        public override bool IsAvailable(IOrder order) {
            if (!base.IsAvailable(order)) {
                return false;
            }

            foreach (var item in order.Items) {
                if (IsAvailable(item)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 檢驗優惠券是否適用於指定訂單項目
        /// </summary>
        /// <param name="order">訂單項目</param>
        /// <returns>是否適用</returns>
        public override bool IsAvailable(IOrderItem orderItem) {
            if (!base.IsAvailable(orderItem)) {
                return false;
            }

            return orderItem.Count >= DiscountQuantity;
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order"></param>
        public override void Use(IOrder order) {
            throw new NotImplementedException();
        }
    }
}
