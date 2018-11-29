using System;
using System.Collections.Generic;
using System.Text;

namespace XWidget.ShoppingCart {
    /// <summary>
    /// 訂單項目
    /// </summary>
    public interface IOrderItem {
        /// <summary>
        /// 名稱
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        uint Count { get; set; }

        /// <summary>
        /// 總價
        /// </summary>
        decimal Amount { get; }
    }
}
