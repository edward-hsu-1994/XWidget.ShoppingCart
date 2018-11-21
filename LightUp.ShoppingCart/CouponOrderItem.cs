using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    /// <summary>
    /// 優惠券訂單項目
    /// </summary>
    public class CouponOrderItem : IOrderItem {
        /// <summary>
        /// 優惠券占用的訂單項目與數量
        /// </summary>
        public Dictionary<IOrderItem, uint> OccupyOrderItemCount { get; set; } = new Dictionary<IOrderItem, uint>();

        /// <summary>
        /// 優惠券
        /// </summary>
        public ICoupon Coupon { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// 折價總額
        /// </summary>
        public decimal Amount => Price * Count;
    }
}
