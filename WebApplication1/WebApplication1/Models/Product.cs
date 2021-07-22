using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    /// <summary>
    /// Книга.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Автор.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Дата издания.
        /// </summary>
        public string DatePublish { get; set; }

        /// <summary>
        /// ISBN код.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество книг на складе.
        /// </summary>
        public int Count { get; set; }
    }
}
