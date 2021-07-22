using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private const string ProductsPath = @"D:\BookStore\WebApplication1\WebApplication1\Data\products.json";
        private const string ShoppingCartPath = @"D:\BookStore\WebApplication1\WebApplication1\Data\shoppingCart.json";
        private const string OrderPath = @"D:\BookStore\WebApplication1\WebApplication1\Data\products.json";

        // Get api/product
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            string json = System.IO.File.ReadAllText(ProductsPath);

            return json;
            //return JsonConvert.DeserializeObject<Models.Product[]>(json).ToList();
        }

        // GET api/product/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Дважды красавчик!";
        }


        // POST api/users
        /// <summary>
        /// Добавление книги в корзину.
        /// </summary>
        /// <param name="product">Книга.</param>
        /// <returns>Успешно ли прошло добавление.</returns>
        [HttpPost]
        public ActionResult<Models.ShoppingCart> Post([FromBody]Models.Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var json = System.IO.File.ReadAllText(ShoppingCartPath);

            var lastBooks = new List<Models.Product>();

            if (json != "")
            {
                lastBooks = JsonConvert.DeserializeObject<Models.Product[]>(json).ToList();
            }

            lastBooks.Add(product);
            var newBooks = lastBooks.ToArray();

            json = JsonConvert.SerializeObject(newBooks);

            System.IO.File.WriteAllText(ShoppingCartPath, json);

            var sumCostBooks = newBooks.Sum(book => book.Count * book.Price);

            var cost = sumCostBooks > 1000
                ? sumCostBooks / 10
                : sumCostBooks;

            var shoppingCart = new Models.ShoppingCart()
            {
                Products = newBooks,
                Cost = cost
            };

            return shoppingCart;
        }

        // POST api/users
        /// <summary>
        /// Добавление книги в корзину.
        /// </summary>
        /// <param name="product">Книга.</param>
        /// <returns>Успешно ли прошло добавление.</returns>
        /*[HttpPost]
        public ActionResult<bool> Post(Models.Product[] products, string nameUser)
        {
            if (products == null || string.IsNullOrEmpty(nameUser))
            {
                return BadRequest();
            }

            var guid = new Guid();
            var countBooks = products.Length;
            var costBooks = products.Sum(book => book.Count * book.Price);

            var order = new Models.Order()
            {
                NameUser = nameUser,
                Products = products,
                GuidOrder = guid,
                Cost = costBooks,
                Count = countBooks
            };

            var json = JsonConvert.SerializeObject(order);

            System.IO.File.WriteAllText(OrderPath, json);

            return true;
        }*/
    }
}
