using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    /// <summary>
    /// 總額折價券
    /// </summary>
    public class TotalAmountCoupon<TIdentifier> : TotalAmountCoupon, ICoupon<TIdentifier> {

        /// <summary>
        /// 唯一識別號
        /// </summary>
        public virtual TIdentifier Id { get; set; }
    }
}
