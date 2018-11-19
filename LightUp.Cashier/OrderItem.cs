using System;
using System.Collections.Generic;
using System.Text;

namespace LightUp.Cashier {
    public class OrderItem {
        internal Dictionary<Type, uint> CouponUsed { get; set; }
            = new Dictionary<Type, uint>();

        /// <summary>
        /// 唯一識別號
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// 總價
        /// </summary>
        public decimal TotalPrice => UnitPrice * Count;

    }
}
