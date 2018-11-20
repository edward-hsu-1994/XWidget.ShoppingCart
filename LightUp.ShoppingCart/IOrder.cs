using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    public interface IOrder {
        IReadOnlyList<IOrderItem> Items { get; }

        decimal TotalAmount { get; }
    }
}
