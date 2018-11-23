using System;
using System.Collections.Generic;
using System.Linq;

namespace LightUp.ShoppingCart {
    public static class Cashier {
        public static void Checkout(IOrder order, IEnumerable<ICoupon> coupons) {
            IEnumerable<ICoupon> allowCoupons = null;

            while ((allowCoupons = coupons.Where(x => x.IsAvailable(order))).Count() > 0) {
                foreach (var coupon in allowCoupons) {
                    if (coupon.IsAvailable(order)) { // 依舊必須要在檢查一次
                        coupon.Use(order);
                    }
                }
            }
        }

        /// <summary>
        /// 取得優惠券占用項目列表
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>訂單項目占用列表</returns>
        public static Dictionary<IOrderItem, Dictionary<ICoupon, uint>> GetTotalOccupyOrderItemCount(IOrder order) {
            var result = new Dictionary<IOrderItem, Dictionary<ICoupon, uint>>();

            foreach (var orderItem in order.Items) {
                // 僅取Coupon項目
                if (orderItem is CouponOrderItem item) {
                    // 列出項目中標示占用的訂單項目數量
                    foreach (var occupy in item.OccupyOrderItemCount) {
                        if (!result.ContainsKey(occupy.Key)) {
                            result[occupy.Key] = new Dictionary<ICoupon, uint>();
                        }
                        Dictionary<ICoupon, uint> details = result[occupy.Key];

                        uint occupyCount = 0;
                        item.OccupyOrderItemCount?.TryGetValue(occupy.Key, out occupyCount);

                        // 加總
                        if (!details.ContainsKey(item.Coupon)) {
                            details[item.Coupon] = 0;
                        }
                        details[item.Coupon] += occupyCount;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 取得訂單中使用的折價券列表
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>折價券列表</returns>
        public static IEnumerable<ICoupon> GetUsedCompon(IOrder order) {
            return order.Items
                .Select(x => 
                    x is CouponOrderItem item ? item.Coupon : null
                )
                .Where(x => x != null);
        }
    }
}
