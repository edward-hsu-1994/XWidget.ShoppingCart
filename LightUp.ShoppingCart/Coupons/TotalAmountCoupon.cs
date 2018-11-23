using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart.Coupons {
    /// <summary>
    /// 總額折價券
    /// </summary>
    public class TotalAmountCoupon : ICoupon {
        /// <summary>
        /// 使用過的訂單
        /// </summary>
        protected List<IOrder> UsedOrder = new List<IOrder>();

        /// <summary>
        /// 名稱
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 是否可重複使用
        /// </summary>
        public virtual bool AllowReuse { get; set; }

        /// <summary>
        /// 標定價格
        /// </summary>
        public virtual decimal Threshold { get; set; }

        /// <summary>
        /// 折價比率(百分比)，如9折則填入10
        /// </summary>
        public virtual uint DiscountPercent { get; set; }

        /// <summary>
        /// 數量
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
        /// 檢驗指定訂單是否已經用過這個優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否已經使用過</returns>
        public virtual bool Used(IOrder order) {
            return UsedOrder.Contains(order);
        }

        /// <summary>
        /// 檢驗此訂單是否可使用這個優惠券
        /// </summary>
        /// <param name="order">訂單</param>
        /// <returns>是否可使用</returns>
        public virtual bool IsAvailable(IOrder order) {
            // 沒有優惠券
            if (!IsUnlimited && Count == 0) {
                return false;
            }

            // 低於金額
            if (order.TotalAmount < Threshold) {
                return false;
            }

            // 已經使用過
            if (Used(order) && !AllowReuse) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 使用優惠券
        /// </summary>
        /// <param name="order"></param>
        public virtual void Use(IOrder order) {
            if (!IsAvailable(order)) {
                return;
            }

            var couponItem = new CouponOrderItem() {
                Name = Name,
                Price = 0,
                Count = 1,
                Coupon = this
            };

            var amount = order.TotalAmount;

            if (AllowReuse) {
                var discount = (100-DiscountPercent) / 100m;

                // amount * discount ^ count = Threshold
                // discount ^ count = Threshold / amount;
                // log_(Threshold / amount) discount = count

                var count = Convert.ToUInt32(Math.Ceiling(
                    Math.Log(decimal.ToDouble(Threshold / amount), decimal.ToDouble(discount))
                ));

                if (Count.HasValue) {
                    count = Convert.ToUInt32(Math.Min(count, Count.Value));
                }

                couponItem.Price = amount - new decimal(Math.Pow(decimal.ToDouble(discount), count)) * amount;
                couponItem.Price *= -1;

                #region 使用並扣除數量
                if (Count.HasValue) {
                    Count -= count;
                }

                order.Items.Add(couponItem);
                UsedOrder.Add(order);
                #endregion
            } else {
                couponItem.Price = amount * DiscountPercent / 100m;
                couponItem.Price *= -1;

                #region 使用並扣除數量
                Count -= 1;
                order.Items.Add(couponItem);
                UsedOrder.Add(order);
                #endregion
            }
        }
    }
}
