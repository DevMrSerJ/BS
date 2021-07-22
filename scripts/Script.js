function buyBook(data) {
    let countBook = Number(document.getElementById("countBook" + data).textContent);
    let ISBNBook = document.getElementById("ISBNBook" + data).textContent;

    if (countBook != 0) {
        document.getElementById("countBook" + data).textContent = --countBook;

        let url = 'https://localhost:44307/api/product?isbnProduct=' + ISBNBook;

        $.get(url);
    }
    else {
        alert("Вы не можете купить данную книгу!")
    }
}

function deleteBook(index) {
    let ISBNBook = document.getElementById("ISBNBook" + index).textContent;

    let url = 'https://localhost:44307/api/product?isbnDeleteProduct=' + ISBNBook + '&isActive=str';
    $.get(url);

    location.reload();
}

function loadCatalog() {
    $.get('https://localhost:44307/api/product?page=catalog', function(data){
        var jsonObj = $.parseJSON(data);

        for(let i = 0; i < jsonObj.length; i++) {
            $(".catalog").append("<div class=\"product\">\n" +
                "            <div class=\"imgProduct\">\n" +
                "                <img src=\"" + jsonObj[i].ImageURL +"\" class=\"productImg\">\n" +
                "            </div>\n" +
                "            <p><span>Название: " + jsonObj[i].Name + "</span></p>\n"+
                "            <p><span>Автор: " + jsonObj[i].Author + "</span></p>\n" +
                "            <p><span>Дата издания: " + jsonObj[i].DatePublish + "</span></p>\n" +
                "            <p>ISBN: <span id=\"ISBNBook" + i + "\">" + jsonObj[i].ISBN + "</span></p>\n" +
                "            <p><span>Стоимость: " + jsonObj[i].Price + "₽</span></p>\n" +
                "            <p>Количество: <span id=\"countBook" + i + "\">" + jsonObj[i].Count + "</span> шт.</p>\n" +
                "            <div>\n" +
                "                <button class=\"buttonBuy\" onclick=\"buyBook(" + i + ");\">КУПИТЬ</button>\n" +
                "            </div>\n" +
                "        </div>");
        }
    });
}

function loadShoppingCart() {
    $.get('https://localhost:44307/api/product?page=shoppingCart', function(data) {
        let jsonObj = $.parseJSON(data);
        let cost = 0;

        if (jsonObj.length === 0) {
            document.getElementById('placeOrder').style.display = 'none';
        }

        for (let i = 0; i < jsonObj.length; i++) {
            $(".catalog table").append("<tr>\n" +
                "            <td class=\"imageCell\">\n" +
                "                <div class=\"imgProductShoppingCart\" id=\"images" + i +"\">\n" +
                "                    <img src=\"" + jsonObj[i].ImageURL + "\" class=\"productImg\">\n" +
                "                </div>\n" +
                "            </td>\n" +
                "            <td><span id=\"Name" + i +"\">" + jsonObj[i].Name + "</span></td>\n" +
                "            <td><span id=\"Author" + i +"\">" + jsonObj[i].Author + "</span></td>\n" +
                "            <td><span id=\"ISBNBook" + i +"\">" + jsonObj[i].ISBN + "</span></td>\n" +
                "            <td><span id=\"priceBook" + i +"\">" + jsonObj[i].Price + "</span>₽</td>\n" +
                "            <td><span id=\"countBookShoppingCart" + i +"\">" + jsonObj[i].Count + "</span></td>\n" +
                "            <td><span id=\"costBooks" + i +"\">" + (jsonObj[i].Price * jsonObj[i].Count) + "</span>₽</td>\n" +
                "            <td>\n" +
                "                <div class=\"blockButtonProduct\">\n" +
                "                    <button onclick=\"deleteBook(" + i + ");\">Удалить</button>\n" +
                "                </div>\n" +
                "            </td>\n" +
                "        </tr>");

            cost += jsonObj[i].Price * jsonObj[i].Count;
        }

        cost = cost > 1000? cost * 0.9: cost;

        document.getElementById("costShoppingCart").textContent = cost;
    });
}

function placeOrder() {
    let nameUser = prompt("Введите ваше имя");

    let answerName  = confirm("Ваше имя " + nameUser + "?");

    if (answerName) {
        let url = 'https://localhost:44307/api/product?nameUser=' + nameUser + "&user=Supervisor&data=1";
        $.get(url);

        location.reload();
    }
}