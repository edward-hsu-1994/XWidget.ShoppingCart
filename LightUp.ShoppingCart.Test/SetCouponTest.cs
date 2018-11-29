using LightUp.ShoppingCart.Coupons;
using LightUp.ShoppingCart.Test.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LightUp.ShoppingCart.Test {
    public class SetCouponTest {
        [Fact(DisplayName = "尿布+啤酒省10元，有限次數")]
        public void SetCouponTest_Limit() {
            var productIdA = Guid.NewGuid();
            var productIdB = Guid.NewGuid();

            // 尿布+啤酒省10元
            var userHasCoupons = new ICoupon[] {
                new SetCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    Count = 2,
                    DiscountPrice = 10,
                    Set = new Dictionary<Guid, uint>() {
                        [productIdA] = 1,
                        [productIdB] = 1
                    },
                    Name = "尿布+啤酒省10元"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = productIdA,
                                Count = 3,
                                Price = 500,
                                Name = "尿布"
                            },
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 8,
                                Price = 70,
                                Name = "衛生紙"
                            },
                            new TestOrderItem() {
                                Id = productIdB,
                                Count = 2,
                                Price = 100,
                                Name = "啤酒"
                            }
                        }
                    )
            };

            var answer = 500 * 3 + 100 * 2 + 70 * 8 - 10 * 2;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "尿布+啤酒省10元，無限次數")]
        public void SetCouponTest_Unlimit() {
            var productIdA = Guid.NewGuid();
            var productIdB = Guid.NewGuid();

            // 尿布+啤酒省10元
            var userHasCoupons = new ICoupon[] {
                new SetCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    DiscountPrice = 10,
                    Set = new Dictionary<Guid, uint>() {
                        [productIdA] = 1,
                        [productIdB] = 1
                    },
                    Name = "尿布+啤酒省10元"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = productIdA,
                                Count = 3,
                                Price = 500,
                                Name = "尿布"
                            },
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 8,
                                Price = 70,
                                Name = "衛生紙"
                            },
                            new TestOrderItem() {
                                Id = productIdB,
                                Count = 4,
                                Price = 100,
                                Name = "啤酒"
                            }
                        }
                    )
            };

            var answer = 500 * 3 + 100 * 4 + 70 * 8 - 10 * 3;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "尿布+兩罐啤酒省10元，不同數量，有限次數")]
        public void SetCouponTest_limit2() {
            var productIdA = Guid.NewGuid();
            var productIdB = Guid.NewGuid();

            // 尿布+啤酒省10元
            var userHasCoupons = new ICoupon[] {
                new SetCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    Count = 2,
                    DiscountPrice = 10,
                    Set = new Dictionary<Guid, uint>() {
                        [productIdA] = 1,
                        [productIdB] = 2
                    },
                    Name = "尿布+兩罐啤酒省10元"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = productIdA,
                                Count = 3,
                                Price = 500,
                                Name = "尿布"
                            },
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 8,
                                Price = 70,
                                Name = "衛生紙"
                            },
                            new TestOrderItem() {
                                Id = productIdB,
                                Count = 2,
                                Price = 100,
                                Name = "啤酒"
                            }
                        }
                    )
            };

            var answer = 500 * 3 + 100 * 2 + 70 * 8 - 10 * 1;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }

        [Fact(DisplayName = "尿布+兩罐啤酒省10元，不同數量，無限次數")]
        public void SetCouponTest_Unlimit2() {
            var productIdA = Guid.NewGuid();
            var productIdB = Guid.NewGuid();

            // 尿布+啤酒省10元
            var userHasCoupons = new ICoupon[] {
                new SetCoupon<Guid,Guid>() {
                    Id = Guid.NewGuid(),
                    DiscountPrice = 10,
                    Set = new Dictionary<Guid, uint>() {
                        [productIdA] = 1,
                        [productIdB] = 2
                    },
                    Name = "尿布+兩罐啤酒省10元"
                }
            };

            IOrder order = new TestOrder() {
                Items = new List<IOrderItem>(
                        new TestOrderItem[] {
                            new TestOrderItem() {
                                Id = productIdA,
                                Count = 3,
                                Price = 500,
                                Name = "尿布"
                            },
                            new TestOrderItem() {
                                Id = Guid.NewGuid(),
                                Count = 8,
                                Price = 70,
                                Name = "衛生紙"
                            },
                            new TestOrderItem() {
                                Id = productIdB,
                                Count = 4,
                                Price = 100,
                                Name = "啤酒"
                            }
                        }
                    )
            };

            var answer = 500 * 3 + 100 * 4 + 70 * 8 - 10 * 2;

            Cashier.Checkout(order, userHasCoupons);

            Assert.Equal(answer, order.TotalAmount);
        }
    }
}
