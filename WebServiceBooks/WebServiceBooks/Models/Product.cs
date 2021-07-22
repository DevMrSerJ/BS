using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceBooks.Models
{
    /// <summary>
    /// Книга.
    /// </summary>
    public class Product: ICloneable
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
        /// Ссылка на изображение.
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество книг на складе.
        /// </summary>
        public int Count { get; set; }

        public object Clone()
        {
            return new Product { 
                Name = this.Name, 
                Author = this.Author,
                DatePublish = this.DatePublish,
                ISBN = this.ISBN,
                ImageURL = this.ImageURL,
                Price = this.Price,
                Count = this.Count
            };
        }
    }
}