using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.Cashier {
    /// <summary>
    /// 優惠券
    /// </summary>
    public interface ICoupon<TId> {
        bool Vaild(Order<TId> order);
        void Use(Order<TId> order);
    }
}
