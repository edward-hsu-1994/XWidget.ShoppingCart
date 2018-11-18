using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier.Coupon {
    /// <summary>
    /// 九折
    /// </summary>
    public class SecondPieceDiscount<TId> : ICoupon<TId> {
        public bool AllowReuse { get; set; }
        public List<TId> Items { get; set; }
        public uint OffPercent { get; set; }

        public bool Vaild(Order<TId> order) {
            //檢驗是否已經重複使用
            if (!AllowReuse &&
                order.Items.Any(x =>
                    x is CouponOrderItem<TId> coupon &&
                    coupon.CouponType == typeof(SecondPieceDiscount<TId>))) {
                return false;
            }

            foreach (var item in order.Items.Where(x => !(x is CouponOrderItem<TId>) && Items.Contains(x.Id))) {
                //找尋還沒有被用來折扣用完的項目
                if (item.Count - item.CouponUsed.Values.Sum(x => x) > 1) {
                    return true;
                }
            }

            return false;
        }

        public void Use(Order<TId> order) {
            foreach (var item in order.Items.Where(x => !(x is CouponOrderItem<TId>) && Items.Contains(x.Id)).ToArray()) {
                var coupon = new CouponOrderItem<TId>() {
                    CouponType = typeof(SecondPieceDiscount<TId>),
                    Count = 0,
                    Name = $"{item.Name}第二件{new string((100 - OffPercent).ToString().TakeWhile(x => x != '0').ToArray())}折",
                    UnitPrice = item.UnitPrice * (OffPercent / 100.0m) * -1m
                };
                //找尋還沒有被用來折扣用完的項目

                var count = item.Count - item.CouponUsed.Values.Sum(x => x);
                if (count < 2) {
                    continue;
                }

                coupon.Count = (uint)Math.Floor(((double)count) / 2);
                if (!item.CouponUsed.ContainsKey(typeof(SecondPieceDiscount<TId>))) {
                    item.CouponUsed[typeof(SecondPieceDiscount<TId>)] = 0;
                }
                item.CouponUsed[typeof(SecondPieceDiscount<TId>)] += coupon.Count * 2;

                order._items.Add(coupon);
            }

        }
    }
}
