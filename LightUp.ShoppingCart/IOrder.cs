using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    public interface IOrder {
        IList<IOrderItem> Items { get; }

        decimal TotalAmount { get; }
    }
}
