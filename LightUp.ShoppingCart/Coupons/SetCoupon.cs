using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.ShoppingCart.Coupons {
    /// <summary>
    /// 套餐折價優惠券
    /// </summary>
    /// <typeparam name="TCouponIdentifier">優惠券唯一識別號</typeparam>
    /// <typeparam name="TOrderItemIdentifier">訂單項目識別號類型</typeparam>
    public class SetCoupon<TCouponIdentifier, TOrderItemIdentifier>
        : ICoupon<TCouponIdentifier> {
        /// <summary>
        /// 唯一識別號
        /// </summary>
        public TCouponIdentifier Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 可用數量
        /// </summary>
        public uint? Count { get; set; }

        /// <summary>
        /// 折價金額
        /// </summary>
        public virtual decimal DiscountPrice { get; set; }

        /// <summary>
        /// 商品組合
        /// </summary>
        public Dictionary<TOrderItemIdentifier, uint> Set { get; set; }

        /// <summary>
        /// 檢驗此訂單是否可使用這個優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否可使用</returns>
        public bool IsAvailable(IOrder order) {
            // 沒有優惠券
            if (Count.HasValue && Count == 0) {
                return false;
            }

            // 合併項目
            Cashier.MergeOrderItem<TOrderItemIdentifier>(order);

            var matchSetItems = order.Items
                .Where(x =>
                    x is IOrderItem<TOrderItemIdentifier> item &&
                    Set.ContainsKey(item.Id));

            var occupy = Cashier.GetTotalOccupyOrderItemCount(order);

            foreach (var setItem in Set) {
                var matchItem = matchSetItems
                    .Where(x =>
                        x is IOrderItem<TOrderItemIdentifier> item &&
                        item.Id.Equals(setItem.Key))
                    .Select(x => new {
                        Occupy = occupy.ContainsKey(x) ? occupy[x].Sum(y => y.Value) : 0,
                        Item = x
                    })
                    .SingleOrDefault();

                // 商品數量
                var count = matchItem.Item.Count;

                // 占用數量
                var occupyCount = matchItem.Occupy;

                // 未占用數量是否符合條件
                if (count - occupyCount < setItem.Value) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        public void Use(IOrder order) {
            // 合併項目
            Cashier.MergeOrderItem<TOrderItemIdentifier>(order);

            while (IsAvailable(order)) {
                // 適用於此優惠券項目
                var setItems = order.Items
                    .Where(x =>
                        x is IOrderItem<TOrderItemIdentifier> item &&
                        Set.Keys.Contains(item.Id)
                    )
                    .Cast<IOrderItem<TOrderItemIdentifier>>();

                // 查詢目前占用
                var occupy = Cashier.GetTotalOccupyOrderItemCount(order);

                // 取得目前已經使用的項目
                CouponOrderItem couponOrderItem
                    = order.Items.SingleOrDefault(x => x is CouponOrderItem item && item.Coupon == this)
                        as CouponOrderItem;

                // 如果沒有則加入
                if (couponOrderItem == null) {
                    couponOrderItem = new CouponOrderItem() {
                        Coupon = this,
                        Name = Name,
                        Price = -DiscountPrice
                    };

                    order.Items.Add(couponOrderItem);
                }

                // 累計
                couponOrderItem.Count++;

                // 更新占用項目
                foreach (var item in setItems) {
                    if (!couponOrderItem.OccupyOrderItemCount.ContainsKey(item)) {
                        couponOrderItem.OccupyOrderItemCount[item] = 0;
                    }
                    couponOrderItem.OccupyOrderItemCount[item] += Set[item.Id];
                }
            }
        }
    }
}
