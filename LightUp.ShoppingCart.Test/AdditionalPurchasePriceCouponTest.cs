using LightUp.ShoppingCart.Coupons;
using LightUp.ShoppingCart.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LightUp.ShoppingCart.Test {
    public class AdditionalPurchasePriceCouponTest {
        [Fact(DisplayName = "滿1000元加購啤酒便宜10元，有限數量")]
        public void AdditionalPurchasePriceCoupon_Limit() {
            var TargetProductId = Guid.NewGuid();

            // 每兩件折價50
            var userHasCoupons = new ICoupon[] {
                new AdditionalPurchasePriceCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    Count = 2,
                    TargetAmount = 1000,
                    DiscountPrice = 10,
                    Targets = new List<Guid>(new Guid[]{ TargetProductId }),
                    Name = "滿1000元加購啤酒便宜10元"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 15,
                                Price = 100,
                                Name = "衛生紙(12包裝)"
                            },
                            new TestOrderItem() {
                                Id = TargetProductId,
                                Count = 10,
                                Price = 50,
                                Name = "啤酒"
                            }
                        }
                    )
            };

            var answer = 100 * 15 + 50 * 10 - 10 * 2;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "滿1000元加購啤酒便宜10元，無限數量")]
        public void AdditionalPurchasePriceCoupon_Unlimit() {
            var TargetProductId = Guid.NewGuid();

            // 每兩件折價50
            var userHasCoupons = new ICoupon[] {
                new AdditionalPurchasePriceCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    TargetAmount = 1000,
                    DiscountPrice = 10,
                    Targets = new List<Guid>(new Guid[]{ TargetProductId }),
                    Name = "滿1000元加購啤酒便宜10元"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 15,
                                Price = 100,
                                Name = "衛生紙(12包裝)"
                            },
                            new TestOrderItem() {
                                Id = TargetProductId,
                                Count = 10,
                                Price = 50,
                                Name = "啤酒"
                            }
                        }
                    )
            };

            var answer = 100 * 15 + 50 * 10 - 10 * 10;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }
    }
}
