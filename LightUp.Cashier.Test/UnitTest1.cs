using LightUp.Cashier.Coupon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace LightUp.Cashier.Test {
    public class UnitTest1 {
        [Fact(DisplayName = "滿1000九折+第二件六折")]
        public void Test1() {
            var cashier = new DefaultCashier();
            // 第二件6折
            cashier.CouponList.Add(new SecondPieceDiscount() {
                OffPercent = 40,
                DiscountItems = new List<object>() { 1 }
            });

            // 滿1000元9折
            cashier.CouponList.Add(new Discount() {
                OffPercent = 10,
                Threshold = 1000
            });

            var order = new Order(new OrderItem[]  {
                new OrderItem() {
                    Id = 0,
                    UnitPrice = 150,
                    Count = 17,
                    Name = "衛生紙"
                },
                new OrderItem() {
                    Id = 1,
                    UnitPrice = 100,
                    Count = 5,
                    Name = "筆記本"
                }
            });
            cashier.Checkout(order);
            string data = "";
            foreach (var item in order.Items) {
                data += "\r\n" + ($"{item.Name}\t單價:{item.UnitPrice}\t數量:{item.Count}\t金額:{item.TotalPrice}");
            }

            Assert.Equal((150m * 17m + (100 + 60) * 2 + 100) * 0.9m, order.Amount);
        }

        [Fact(DisplayName = "滿1000九折+第二件六折1次")]
        public void Test2() {
            var cashier = new DefaultCashier();
            // 第二件6折
            cashier.CouponList.Add(new SecondPieceDiscount() {
                OffPercent = 40,
                DiscountItems = new List<object>() { 1 },
                Count = 1
            });

            // 滿1000元9折
            cashier.CouponList.Add(new Discount() {
                OffPercent = 10,
                Threshold = 1000
            });

            var order = new Order(new OrderItem[]  {
                new OrderItem() {
                    Id = 0,
                    UnitPrice = 150,
                    Count = 17,
                    Name = "衛生紙"
                },
                new OrderItem() {
                    Id = 1,
                    UnitPrice = 100,
                    Count = 5,
                    Name = "筆記本"
                }
            });
            cashier.Checkout(order);
            string data = "";
            foreach (var item in order.Items) {
                data += "\r\n" + ($"{item.Name}\t單價:{item.UnitPrice}\t數量:{item.Count}\t金額:{item.TotalPrice}");
            }

            Assert.Equal((150m * 17m + 60 + 400) * 0.9m, order.Amount);
        }
    }
}
