//for Create and Edit
var showLength = document.querySelectorAll(".show-length");
showLength.forEach(element => {
    ShowLength(element);
});

//start check trade is money or barter

function TradeCheck() {
    //document.getElementById('UnitPrice_div').classList.remove("d-none");
    let check1 = document.getElementById('moneytradecheckbox');
    let check2 = document.getElementById('bartertradecheckbox');
    if (check1.checked && check2.checked) {
        document.getElementById('Trade').value = "買賣與交換";
        if (document.getElementById('UnitPrice').value == "999999999") {
            document.getElementById('UnitPrice').value = "";
        }
        document.getElementById('UnitPrice_div').classList.remove("d-none");
    }
    if (check1.checked && !check2.checked) {
        document.getElementById('Trade').value = "買賣";
        if (document.getElementById('UnitPrice').value == "999999999") {
            document.getElementById('UnitPrice').value = "";
        }
        document.getElementById('UnitPrice_div').classList.remove("d-none");
    }
    if (!check1.checked && check2.checked) {
        document.getElementById('Trade').value = "交換";
        if (document.getElementById('UnitPrice').value == "") {
            document.getElementById('UnitPrice').value = "999999999";
        }
        document.getElementById('UnitPrice_div').classList.add("d-none");
    }
    if (!check1.checked && !check2.checked) {
        document.getElementById('Trade').value = "";
    }
}
//end
//start check empty and show length
var showLength = document.querySelectorAll(".show-length");
showLength.forEach(element => {
    element.addEventListener("input", () => {
        ShowLength(element);
    })
    element.addEventListener("blur", () => {
        ShowLength(element);
    })
});
function ShowLength(element) {
    let length = element.value.length;
    element.nextElementSibling.innerText = `${length}/${element.getAttribute('maxlength')}`;
}
//end
//start check img value valid
for (let i = 2; i <= 9; i++) {
    let preImg = document.getElementById(`Image${i}`).value;
    let preId = document.getElementById(`ProductId`).value;
    let preUserId = document.getElementById(`CreateBy`).value;
    if (preImg != "無圖片") {
        document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload="testLoad('${i}')" onerror="testError('${i}')">`;
    }
    document.getElementById(`file${i}`).addEventListener("click", () => {
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
        let preImg = document.getElementById(`Image${i}`).value;
        let preId = document.getElementById(`ProductId`).value;
        let preUserId = document.getElementById(`CreateBy`).value;
        if (preImg != "無圖片") {
            document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload="testLoad('${i}')" onerror="testError('${i}')">`;
        }
    });
    document.getElementById(`file${i}`).addEventListener("input", () => {
        let file = document.getElementById(`file${i}`);
        let p_file = document.getElementById(`p-file${i}`);
        if (file.files[0].size > 2097152 ) {
            alert("檔案過大!");
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
            let preImg = document.getElementById(`Image${i}`).value;
            let preId = document.getElementById(`ProductId`).value;
            let preUserId = document.getElementById(`CreateBy`).value;
            document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload="testLoad('${i}')" onerror="testError('${i}')">`;
        }
        else if (!file.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
            alert('不是正確圖檔!');
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
            let preImg = document.getElementById(`Image${i}`).value;
            let preId = document.getElementById(`ProductId`).value;
            let preUserId = document.getElementById(`CreateBy`).value;
            document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload="testLoad('${i}')" onerror="testError('${i}')">`;
        }
        else {
            p_file.innerText = file.files[0].name;
            let url = URL.createObjectURL(file.files[0]);
            document.getElementById(`img${i}Div`).innerHTML = `<img src='${url}' alt='img${i}' id='img${i}' onload="testLoad('${i}')" onerror="testError('${i}')">`;
        };
    });
    document.getElementById(`Image${i}-clear`).addEventListener("click", () => {
        document.getElementById(`Image${i}`).value = '無圖片';
        document.getElementById(`file${i}`).value = '';
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
        document.getElementById(`img${i}Div`).innerHTML = `<img src='/DeafultPictures/DeafultBookPicture.jpg' alt='img${i}' id='img${i}'>`;
    });
}
function testLoad(i) {
    let _img = document.getElementById(`img${i}`);
    if (_img.height > 1280 || _img.width > 1280) {
        document.getElementById(`file${i}`).value = "";
        document.getElementById(`p-file${i}`).innerText = "寬或高大於 1280px!";
        document.getElementById(`img${i}Div`).innerHTML = `<img src='/DeafultPictures/DeafultBookPicture.jpg' alt='img${i}' id='img${i}'>`;
    }
}
function testError(i) {
    document.getElementById(`file${i}`).value = "";
    document.getElementById(`p-file${i}`).innerText = "無法取得圖片訊息!";
    document.getElementById(`img${i}Div`).innerHTML = `<img src='/DeafultPictures/DeafultBookPicture.jpg' alt='img${i}' id='img${i}'>`;
}
//end
//start detect leave page
let inputLeave = document.querySelectorAll("input");
let textareaLeave = document.querySelectorAll("textarea");
inputLeave.forEach(element => {
    element.addEventListener("focus", () => {
        window.onbeforeunload = function () {
            return "You have attempted to leave this page. Are you sure?";
        };
    })
});
textareaLeave.forEach(element => {
    element.addEventListener("focus", () => {
        window.onbeforeunload = function () {
            return "You have attempted to leave this page. Are you sure?";
        };
    })
});
let submit = document.querySelectorAll(".disable-onbeforeunload");
submit.forEach(element => {
    element.addEventListener("click", () => {
        window.onbeforeunload = null;
    })
});
//end
//start only number
function process(input) {
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
    url = `https://cdnec.sanmin.com.tw/product_images/${input.value.substring(3, 6)}/${input.value.substring(3, 12)}.jpg`;
    document.getElementById(`img1Div`).innerHTML = `<img src='${url}' alt='img1' id='img1' onerror="this.src='/DeafultPictures/DeafultBookPicture.jpg';this.onerror='';">`;
}
//end
let tradeMessage = document.querySelector("#tradeMessage");
if (tradeMessage.innerText == "交易方式不可為空!") {
    Swal.fire(
        '交易方式不可為空!',
        '',
        'error'
    )
}
/**/
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
        let authorsInput = document.getElementById("Author");
        authorsInput.value = authors;
        const publisher = bookInfo.publisher;
        let publisherInput = document.getElementById("Publisher");
        publisherInput.value = publisher;
        const description = bookInfo.description || "無描述";
        let descriptionInput = document.getElementById("ContentText");
        descriptionInput.value = description;
        const thumbnail = bookInfo.imageLinks ? bookInfo.imageLinks.thumbnail : "";
        showLength.forEach(element => {
            ShowLength(element);
        });
        const bookDetails = `
                    <h2>${title}</h2>
                    <p><strong>作者:</strong> ${authors}</p>
                    <p><strong>描述:</strong> ${description}</p>
                    <img src="${thumbnail}" alt="${title}封面">
                `;

        resultContainer.innerHTML = bookDetails;
        Swal.fire(
            '找到書籍',
            '已自動填入',
            'success'
        )
    } else {
        resultContainer.innerHTML = "找不到符合的書籍";
        Swal.fire(
            '查無此書，請手動輸入商品資訊',
            '',
            'error'
        )
    }
}
/**/