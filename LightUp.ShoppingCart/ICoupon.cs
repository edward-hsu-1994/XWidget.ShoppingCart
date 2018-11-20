using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    /// <summary>
    /// 優惠券介面
    /// </summary>
    public interface ICoupon {
        /// <summary>
        /// 可用數量
        /// </summary>
        uint? Count { get; set; }

        /// <summary>
        /// 檢驗優惠券是否適用於指定訂單
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否適用</returns>
        bool IsAvailable(IOrder order);

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        void Use(IOrder order);
    }
}
