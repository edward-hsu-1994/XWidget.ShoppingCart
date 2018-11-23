using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart.Test.Models {
    public class TestOrderItem : IOrderItem<Guid> {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Count { get; set; }

        public decimal Amount {
            get {
                return Price * Count;
            }
        }
    }
}
