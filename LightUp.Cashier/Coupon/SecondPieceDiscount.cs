using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier.Coupon {
    /// <summary>
    /// 第N件M折
    /// </summary>
    public class SecondPieceDiscount : ICoupon {
        public object Id { get; set; }
        public bool AllowReuse { get; set; }
        public List<object> DiscountItems { get; set; }
        public uint OffPercent { get; set; }
        public uint? Count { get; set; }

        public bool Vaild(Order order) {
            if (Count == 0) return false;

            //檢驗是否已經重複使用
            if (!AllowReuse &&
                order.Items.Any(x =>
                    x is CouponOrderItem coupon &&
                    coupon.CouponType == typeof(SecondPieceDiscount))) {
                return false;
            }

            foreach (var item in order.Items.Where(x => !(x is CouponOrderItem) && DiscountItems.Contains(x.Id))) {
                //找尋還沒有被用來折扣用完的項目
                if (item.Count - item.CouponUsed.Values.Sum(x => x) > 1) {
                    return true;
                }
            }

            return false;
        }

        public void Use(Order order) {
            foreach (var item in order.Items.Where(x => !(x is CouponOrderItem) && DiscountItems.Contains(x.Id)).ToArray()) {
                var coupon = new CouponOrderItem() {
                    CouponType = typeof(SecondPieceDiscount),
                    Count = 0,
                    Name = $"{item.Name}第二件{new string((100 - OffPercent).ToString().TakeWhile(x => x != '0').ToArray())}折",
                    UnitPrice = item.UnitPrice * (OffPercent / 100.0m) * -1m
                };
                //找尋還沒有被用來折扣用完的項目

                var count = item.Count - item.CouponUsed.Values.Sum(x => x);
                if (count < 2) {
                    continue;
                }

                if (Count.HasValue) {
                    coupon.Count = Math.Min(Count.Value, (uint)Math.Floor(((double)count) / 2));
                } else {
                    coupon.Count = (uint)Math.Floor(((double)count) / 2);
                }
                if (!item.CouponUsed.ContainsKey(typeof(SecondPieceDiscount))) {
                    item.CouponUsed[typeof(SecondPieceDiscount)] = 0;
                }
                item.CouponUsed[typeof(SecondPieceDiscount)] += coupon.Count * 2;

                order._items.Add(coupon);

                if (Count.HasValue) {
                    Count -= coupon.Count;
                }
            }

        }
    }
}
