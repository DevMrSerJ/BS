using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceBooks.Models
{
    /// <summary>
    /// Заказ.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string NameUser { get; set; }

        /// <summary>
        /// Книги в заказе.
        /// </summary>
        public Product[] Products { get; set; }

        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public Guid GuidOrder { get; set; }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Количество книг.
        /// </summary>
        public int Count { get; set; }
    }
}