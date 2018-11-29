using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XWidget.ShoppingCart.Coupons {
    /// <summary>
    /// 訂單項目優惠券基礎類別
    /// </summary>
    /// <typeparam name="TCouponIdentifier">優惠券唯一識別號</typeparam>
    /// <typeparam name="TOrderItemIdentifier">訂單項目識別號類型</typeparam>
    public abstract class OrderItemCouponBase<TCouponIdentifier, TOrderItemIdentifier>
        : ICoupon<TCouponIdentifier> {
        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 作用商品列表
        /// </summary>
        public virtual List<TOrderItemIdentifier> Targets { get; set; } = new List<TOrderItemIdentifier>();

        /// <summary>
        /// 唯一識別號
        /// </summary>
        public virtual TCouponIdentifier Id { get; set; }

        /// <summary>
        /// 可用數量
        /// </summary>
        public virtual uint? Count { get; set; }

        /// <summary>
        /// 是否為數量不受限制的
        /// </summary>
        public bool IsUnlimited {
            get {
                return !Count.HasValue;
            }
        }

        /// <summary>
        /// 檢驗優惠券是否適用於指定訂單
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否適用</returns>
        public virtual bool IsAvailable(IOrder order) {
            if (!IsUnlimited && Count == 0) {
                return false;
            }

            return order.Items.Any(x => IsAvailable(order, x));
        }

        /// <summary>
        /// 檢驗優惠券是否適用於指定訂單項目
        /// </summary>
        /// <param name="orderItem">訂單項目</param>
        /// <param name="order">訂單</param>
        /// <returns>是否適用</returns>
        private protected virtual bool IsAvailable(IOrder order,IOrderItem orderItem) {
            if (orderItem is IOrderItem<TOrderItemIdentifier> item) {
                return Targets.Contains(item.Id);
            }

            if(Count.HasValue && Count == 0) {
                return false;
            }

            return false;
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        public abstract void Use(IOrder order);
    }
}
