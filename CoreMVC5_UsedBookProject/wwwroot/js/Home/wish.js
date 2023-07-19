async function searchByISBN() {
    const apiKey = "AIzaSyAeJpJzP9zX-Bz7P-qwFIwybZ7A92MIa0k"; // 替換為你的 Google Books API 金鑰
    const isbn = document.getElementById("isbnInput").value;

    try {
        const apiUrl = `https://www.googleapis.com/books/v1/volumes?q=isbn:${isbn}&key=${apiKey}`;

        const response = await fetch(apiUrl);
        const data = await response.json();

        if (response.ok) {
            displayResult(data);
        } else {
            console.error("查詢失敗，HTTP 響應碼：" + response.status);
        }
    } catch (error) {
        console.error("發生錯誤：" + error.message);
    }
}

function displayResult(data) {
    const resultContainer = document.getElementById("resultContainer");
    resultContainer.innerHTML = "";

    if (data.totalItems > 0) {
        const bookInfo = data.items[0].volumeInfo;
        const title = bookInfo.title;
        const authors = bookInfo.authors ? bookInfo.authors.join(", ") : "未知作者";
        const description = bookInfo.description || "無描述";
        const thumbnail = bookInfo.imageLinks ? bookInfo.imageLinks.thumbnail : "";

        const bookDetails = `
                    <h2>${title}</h2>
                    <p><strong>作者:</strong> ${authors}</p>
                    <p><strong>描述:</strong> ${description}</p>
                    <img src="${thumbnail}" alt="${title}封面">
                `;

        resultContainer.innerHTML = bookDetails;
    } else {
        resultContainer.innerHTML = "找不到符合的書籍";
    }
}