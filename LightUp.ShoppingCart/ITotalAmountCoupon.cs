using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.ShoppingCart {
    /// <summary>
    /// 總額折價券
    /// </summary>
    public interface ITotalAmountCoupon : ICoupon {
        /// <summary>
        /// 是否可重複使用
        /// </summary>
        bool AllowReuse { get; set; }

        /// <summary>
        /// 標定價格
        /// </summary>
        decimal Threshold { get; set; }

        /// <summary>
        /// 折價比率(百分比)，如9折則填入10
        /// </summary>
        uint DiscountPercent { get; set; }
    }
}
