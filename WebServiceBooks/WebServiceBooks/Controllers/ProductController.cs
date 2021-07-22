using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebServiceBooks.Controllers
{
    [EnableCors(origins: "http://localhost:63342", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        private const string ProductsPath = @"D:\BookStore\BS\WebServiceBooks\WebServiceBooks\Data\product.json";
        private const string ShoppingCartPath = @"D:\BookStore\BS\WebServiceBooks\WebServiceBooks\Data\shoppingCart.json";
        private const string OrderPath = @"D:\BookStore\BS\WebServiceBooks\WebServiceBooks\Data\order.json";

        // GET api/product
        /// <summary>
        /// Получение каталога товара или список товаров в корзину.
        /// </summary>
        /// <param name="page">Страница, для которой нужна загрузка товраов.</param>
        /// <returns>JSON товаров.</returns>
        public string GetAllProduct(string page)
        {
            var json = String.Empty;

            if (page == "catalog")
            {
                json = System.IO.File.ReadAllText(ProductsPath);
            }
            else
            {
                json = System.IO.File.ReadAllText(ShoppingCartPath);
            }

            return json;
        }

        /// <summary>
        /// Добавляет книгу в корзину.
        /// </summary>
        /// <param name="isbnProduct">ISBN книги.</param>
        public void Get(string isbnProduct)
        {
            var json = System.IO.File.ReadAllText(ProductsPath);
            var allBooks = JsonConvert.DeserializeObject<Models.Product[]>(json);

            json = System.IO.File.ReadAllText(ShoppingCartPath);

            var shoppingCart = new List<Models.Product>();

            if (json != "")
            {
                shoppingCart = JsonConvert.DeserializeObject<Models.Product[]>(json).ToList();
            }

            for (int i = 0; i < allBooks.Length; i++)
            {
                if (allBooks[i].ISBN == isbnProduct)
                {
                    var exist = false;

                    foreach (var book in shoppingCart)
                    {
                        if (book.ISBN == allBooks[i].ISBN)
                        {
                            exist = true;
                            book.Count++;
                        }
                    }

                    if (!exist)
                    {
                        var buyBook = allBooks[i].Clone() as Models.Product;
                        buyBook.Count = 1;

                        shoppingCart.Add(buyBook);

                        allBooks[i].Count--;
                    }

                    break;
                }
            }


            var jsonShoppingCart = JsonConvert.SerializeObject(shoppingCart.ToArray());

            System.IO.File.WriteAllText(ShoppingCartPath, jsonShoppingCart);

            jsonShoppingCart = JsonConvert.SerializeObject(allBooks.ToArray());

            System.IO.File.WriteAllText(ProductsPath, jsonShoppingCart);
        }

        /// <summary>
        /// Удаление книги из корзины.
        /// </summary>
        /// <param name="isbnProduct">ISBN книги.</param>
        public string Get(string isbnDeleteProduct, string isActive)
        {
            var json = System.IO.File.ReadAllText(ShoppingCartPath);
            var shoppingCart = JsonConvert.DeserializeObject<Models.Product[]>(json).ToList();

            json = System.IO.File.ReadAllText(ProductsPath);

            var allBooks = JsonConvert.DeserializeObject<Models.Product[]>(json).ToList();

            for (int i = 0; i < shoppingCart.Count; i++)
            {
                if (shoppingCart[i].ISBN == isbnDeleteProduct)
                {
                    foreach (var book in allBooks)
                    {
                        if (book.ISBN == shoppingCart[i].ISBN)
                        {
                            if (shoppingCart[i].Count == 1)
                            {
                                shoppingCart.RemoveAt(i);
                            }
                            else
                            {
                                shoppingCart[i].Count--;
                            }

                            book.Count++;
                            break;
                        }
                    }
                }
            }


            var jsonShoppingCart = JsonConvert.SerializeObject(shoppingCart.ToArray());

            System.IO.File.WriteAllText(ShoppingCartPath, jsonShoppingCart);

            jsonShoppingCart = JsonConvert.SerializeObject(allBooks.ToArray());

            System.IO.File.WriteAllText(ProductsPath, jsonShoppingCart);

            return "";
        }

        /// <summary>
        /// Формление заказа.
        /// </summary>
        /// <param name="order">Имя пользователя.</param>
        public string GetOrder(string nameUser, string user, string data)
        {
            var json = System.IO.File.ReadAllText(ShoppingCartPath);
            var shoppingCart = JsonConvert.DeserializeObject<Models.Product[]>(json).ToList();

            json = System.IO.File.ReadAllText(OrderPath);

            var orderList = new List<Models.Order>();

            var orderProducts = new List<Models.Product>(shoppingCart);

            if (json != "")
            {
                orderList = JsonConvert.DeserializeObject<Models.Order[]>(json).ToList();
            }

            var cost = orderProducts.Sum(book => book.Price * book.Count);

            if (cost > 1000)
            {
                cost = Convert.ToInt32(cost * 0.9); 
            }

            var order = new Models.Order()
            {
                NameUser = nameUser,
                Products = orderProducts.ToArray(),
                GuidOrder = Guid.NewGuid(),
                Cost = cost,
                Count = orderProducts.Sum(book => book.Count)
            };

            orderList.Add(order);

            var jsonOrder = JsonConvert.SerializeObject(orderList);

            System.IO.File.WriteAllText(OrderPath, jsonOrder);
            System.IO.File.WriteAllText(ShoppingCartPath, "");

            return "";
        }
    }
}
