using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier {
    public class Order<TId> {
        internal List<OrderItem<TId>> _items;
        public IReadOnlyList<OrderItem<TId>> Items => this._items.AsReadOnly();

        public decimal Amount {
            get {
                return Items.Sum(x => x.TotalPrice);
            }
        }

        public Order(IEnumerable<OrderItem<TId>> items) {
            this._items = items.ToList();
        }
    }
}
