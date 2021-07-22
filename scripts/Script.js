function buyBook() {
    let countBook = Number(document.getElementById("countBook").textContent);

    if (countBook != 0) {
        document.getElementById("countBook").textContent = --countBook;

        // Отправить запрос на сервер для проверки такой книги в корзине
    }
    else {
        alert("Вы не можете купить данную книгу!")
    }
}

function deleteBook() {
    let countBook = Number(document.getElementById("countBookShoppingCart").textContent);
    let priceBook = Number(document.getElementById("priceBook").textContent);
    document.getElementById("costBooks").textContent = countBook * priceBook

    if (countBook > 1) {
        countBook--;
        let costBooks = priceBook * countBook;

        document.getElementById("costBooks").textContent = costBooks;
        document.getElementById("countBookShoppingCart").textContent = countBook;
    }
    else {
        // удалить блок
    }
}

function load() {

}