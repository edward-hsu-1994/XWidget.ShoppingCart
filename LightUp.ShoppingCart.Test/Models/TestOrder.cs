using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LightUp.ShoppingCart.Test.Models {
    class TestOrder : IOrder {
        public IList<IOrderItem> Items { get; set; }

        public decimal TotalAmount {
            get {
                return Items.Sum(x => x.Amount);
            }
        }
    }
}
