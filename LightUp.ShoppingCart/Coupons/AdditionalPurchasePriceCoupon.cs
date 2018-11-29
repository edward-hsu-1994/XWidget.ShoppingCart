using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.ShoppingCart.Coupons {
    /// <summary>
    /// 滿額加購折價券
    /// </summary>
    /// <typeparam name="TCouponIdentifier">優惠券唯一識別號</typeparam>
    /// <typeparam name="TOrderItemIdentifier">訂單項目識別號類型</typeparam>
    public class AdditionalPurchasePriceCoupon<TCouponIdentifier, TOrderItemIdentifier>
        : OrderItemCouponBase<TCouponIdentifier, TOrderItemIdentifier> {
        /// <summary>
        /// 目標價格
        /// </summary>
        public decimal TargetAmount { get; set; }

        /// <summary>
        /// 折價金額
        /// </summary>
        public virtual decimal DiscountPrice { get; set; }

        /// <summary>
        /// 檢驗此訂單是否可使用這個優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否可使用</returns>
        public override bool IsAvailable(IOrder order) {
            // 合併項目
            Cashier.MergeOrderItem<TOrderItemIdentifier>(order);

            if (!base.IsAvailable(order) || order.TotalAmount < TargetAmount) {
                return false;
            }

            foreach (var item in order.Items.Where(x => x is IOrderItem<TOrderItemIdentifier> _item && Targets.Contains(_item.Id))) {
                if (IsAvailable(order, item)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 檢驗優惠券是否適用於指定訂單項目
        /// </summary>
        /// <param name="order">訂單</param>
        /// <param name="orderItem">訂單項目</param>
        /// <returns>是否適用</returns>
        private protected override bool IsAvailable(IOrder order, IOrderItem orderItem) {
            if (!base.IsAvailable(order, orderItem) &&
                // 檢驗該訂單扣除目前要折價項目的金額是否有滿足折價目標
                order.TotalAmount - orderItem.Amount < TargetAmount) {
                return false;
            }

            var occupyOrderItem = Cashier.GetTotalOccupyOrderItemCount(order);

            if (occupyOrderItem.ContainsKey(orderItem)) {
                // 占用數量
                var occupyOrderItemTotal = occupyOrderItem[orderItem].Sum(x => x.Value);

                // 剩餘未占用項目是否還夠用
                return orderItem.Count - occupyOrderItemTotal > 0;
            }

            return orderItem.Count > 0;
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        public override void Use(IOrder order) {
            // 合併項目
            Cashier.MergeOrderItem<TOrderItemIdentifier>(order);

            foreach (var orderItem in order.Items.ToArray()) {
                if (IsAvailable(order, orderItem) &&
                    orderItem is IOrderItem<TOrderItemIdentifier> item) {

                    var totalOccupy = Cashier.GetTotalOccupyOrderItemCount(order);

                    var thisItemUsedCouponCount
                        = totalOccupy.ContainsKey(item) ?
                        totalOccupy[item].Sum(x => x.Value) :
                        0;

                    var count = item.Count - (uint)thisItemUsedCouponCount;

                    if (Count.HasValue) {
                        count = Math.Min(Count.Value, count);
                    }

                    var couponItem = new CouponOrderItem() {
                        Coupon = this,
                        Name = Name,
                        Price = -DiscountPrice,
                        Count = count
                    };

                    couponItem.OccupyOrderItemCount[item] = count;

                    if (Count.HasValue) {
                        Count -= couponItem.Count;
                    }

                    order.Items.Add(couponItem);
                }
            }
        }
    }
}
