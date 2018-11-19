using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.Cashier {
    /// <summary>
    /// 優惠券
    /// </summary>
    public interface ICoupon {
        object Id { get; set; }
        uint? Count { get; set; }
        bool Vaild(Order order);
        void Use(Order order);
    }
}
