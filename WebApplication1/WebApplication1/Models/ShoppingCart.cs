using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    /// <summary>
    /// Корзина.
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// Книги.
        /// </summary>
        public Product[] Products { get; set; }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public int Cost { get; set; }
    }
}
