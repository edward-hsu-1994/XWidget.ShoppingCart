using System;
using System.Collections.Generic;
using System.Text;

namespace XWidget.ShoppingCart {
    /// <summary>
    /// 訂單
    /// </summary>
    public interface IOrder {
        /// <summary>
        /// 訂單項目
        /// </summary>
        IList<IOrderItem> Items { get; }

        /// <summary>
        /// 總金額
        /// </summary>
        decimal TotalAmount { get; }
    }
}
