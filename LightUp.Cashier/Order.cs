using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier {
    public class Order {
        internal List<OrderItem> _items;
        public IReadOnlyList<OrderItem> Items => this._items.AsReadOnly();

        public decimal Amount {
            get {
                return Items.Sum(x => x.TotalPrice);
            }
        }

        public Order(IEnumerable<OrderItem> items) {
            this._items = items.ToList();
        }
    }
}
