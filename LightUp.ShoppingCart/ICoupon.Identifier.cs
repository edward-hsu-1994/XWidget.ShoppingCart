using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    /// <summary>
    /// 優惠券介面
    /// </summary>
    public interface ICoupon<TIdentifier> : ICoupon {
        /// <summary>
        /// 唯一識別號
        /// </summary>
        TIdentifier Id { get; set; }
    }
}
