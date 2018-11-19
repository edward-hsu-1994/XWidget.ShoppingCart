using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightUp.Cashier {
    public class Order {
        /// <summary>
        /// 優惠券使用數量(Key為優惠券Id)
        /// </summary>
        public Dictionary<object, uint> CouponUsed { get; set; }
            = new Dictionary<object, uint>();

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
