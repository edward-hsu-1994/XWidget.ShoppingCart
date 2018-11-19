using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier.Coupon {
    /// <summary>
    /// 九折
    /// </summary>
    public class Discount : ICoupon {
        public object Id { get; set; }
        public bool AllowReuse { get; set; }
        public decimal Threshold { get; set; }
        public uint OffPercent { get; set; }
        public uint? Count { get; set; }

        public bool Vaild(Order order) {
            if (Count == 0) return false;

            //檢驗是否已經重複使用
            if (!AllowReuse &&
                order.CouponUsed.ContainsKey(Id) &&
                order.CouponUsed[Id] > 0) {
                return false;
            }

            if (order.Amount < Threshold) {
                return false;
            }

            return true;
        }

        public void Use(Order order) {
            order._items.Add(new CouponOrderItem() {
                CouponType = typeof(Discount),
                Count = 1,
                Name = $"滿{Threshold}元{new string((100 - OffPercent).ToString().TakeWhile(x => x != '0').ToArray())}折",
                UnitPrice = order.Amount * (OffPercent / 100m) * -1m
            });
            if (order.CouponUsed.ContainsKey(Id)) {
                order.CouponUsed[Id]++;
            } else {
                order.CouponUsed[Id] = 1;
            }

            if (Count.HasValue) {
                Count--;
            }
        }
    }
}
