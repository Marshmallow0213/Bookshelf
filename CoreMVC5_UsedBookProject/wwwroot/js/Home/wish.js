function searchByISBN() {
    const apiKey = "AIzaSyAeJpJzP9zX-Bz7P-qwFIwybZ7A92MIa0k"; // 替換為你的 Google Books API 金鑰
    const isbn = document.getElementById("ISBN").value;

    if (!isbn) {
        Swal.fire(
            'ISBN 號碼不能為空',
            '',
            'error'
        )
        return;
    }
    if (isbn.length != 13) {
        Swal.fire(
            'ISBN須為13碼',
            '',
            'error'
        )
        return;
    }
    const apiUrl = `https://www.googleapis.com/books/v1/volumes?q=isbn:${isbn}&key=${apiKey}`;

    fetch(apiUrl)
        .then(response => response.json())
        .then(data => displayResult(data))
        .catch(error => console.error("發生錯誤：" + error.message));
}
function displayResult(data) {
    const resultContainer = document.getElementById("resultContainer");
    resultContainer.innerHTML = "";

    if (data.totalItems > 0) {
        const bookInfo = data.items[0].volumeInfo;
        const title = bookInfo.title;
        let titleInput = document.getElementById("Title");
        titleInput.value = title;
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
        Swal.fire(
            '查到書籍內容資訊',
            '已自動填入',
            'success'
        )
    } else {
        resultContainer.innerHTML = "查不到符合的書籍內容資訊";
        Swal.fire(
            '查無此書，請手動輸入書名',
            '',
            'error'
        )
    }
}