using System;
using System.Collections.Generic;
using System.Text;

namespace XWidget.ShoppingCart {
    /// <summary>
    /// 訂單項目
    /// </summary>
    public interface IOrderItem<TIdentifier> : IOrderItem {
        /// <summary>
        /// 唯一識別號
        /// </summary>
        TIdentifier Id { get; set; }
    }
}
