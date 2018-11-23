using LightUp.ShoppingCart.Coupons;
using LightUp.ShoppingCart.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LightUp.ShoppingCart.Test {
    public class QuantituDiscountCouponTest {
        [Fact(DisplayName ="第二件半價，有限次數")]
        public void SecondHalfPrice_Limit() {
            var TargetProductId = Guid.NewGuid();

            // 每兩件折價50
            var userHasCoupons = new ICoupon[] {
                new QuantituDiscountCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    Count = 2,
                    DiscountQuantity = 2,
                    DiscountPrice = 100m * 0.5m,
                    Targets = new List<Guid>(new Guid[]{ TargetProductId}),
                    Name = "衛生紙第二件半價"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id =TargetProductId,
                                Count = 15,
                                Price = 100,
                                Name = "衛生紙(12包裝)"
                            }
                        }
                    )
            };

            var answer = (100 + 50) * 2 + 100 * (15 - 4);

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }


        [Fact(DisplayName = "第二件半價，無限次數")]
        public void SecondHalfPrice_Unlimit() {
            var TargetProductId = Guid.NewGuid();

            // 每兩件折價50
            var userHasCoupons = new ICoupon[] {
                new QuantituDiscountCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    DiscountQuantity = 2,
                    DiscountPrice = 100m * 0.5m,
                    Targets = new List<Guid>(new Guid[]{ TargetProductId}),
                    Name = "衛生紙第二件半價"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id =TargetProductId,
                                Count = 15,
                                Price = 100,
                                Name = "衛生紙(12包裝)"
                            }
                        }
                    )
            };

            var answer = (100 + 50) * 7 + 100;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }
    }
}
