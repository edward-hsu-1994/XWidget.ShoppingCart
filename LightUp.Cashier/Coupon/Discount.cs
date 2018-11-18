using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier.Coupon {
    /// <summary>
    /// 九折
    /// </summary>
    public class Discount<TId> : ICoupon<TId> {
        public bool AllowReuse { get; set; }
        public decimal Threshold { get; set; }
        public uint OffPercent { get; set; }

        public bool Vaild(Order<TId> order) {
            //檢驗是否已經重複使用
            if (!AllowReuse &&
                order.Items.Any(x =>
                    x is CouponOrderItem<TId> coupon &&
                    coupon.CouponType == typeof(Discount<TId>))) {
                return false;
            }

            if (order.Amount < Threshold) {
                return false;
            }

            return true;
        }

        public void Use(Order<TId> order) {
            order._items.Add(new CouponOrderItem<TId>() {
                CouponType = typeof(Discount<TId>),
                Count = 1,
                Name = $"滿{Threshold}元{new string((100 - OffPercent).ToString().TakeWhile(x => x != '0').ToArray())}折",
                UnitPrice = order.Amount * (OffPercent / 100m) * -1m
            });
        }
    }
}
