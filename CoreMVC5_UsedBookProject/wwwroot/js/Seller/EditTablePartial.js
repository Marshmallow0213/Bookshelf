//for Create and Edit
window.onload = function () {
    if (document.querySelector("h1").innerText == "新增商品") {
        document.getElementById("flexRadioTrade1").addEventListener("click", Money);
        document.getElementById("flexRadioTrade2").addEventListener("click", Barter);
    }
    var showLength = document.querySelectorAll(".show-length");
    showLength.forEach(element => {
        ShowLength(element);
    });
    textarea = document.querySelectorAll("textarea");
    textarea.forEach(element => {
        autoresizing(element);
    });
}
//start check trade is money or barter

function Money() {
    document.getElementById('UnitPrice_div').classList.remove("d-none");
}

function Barter() {
    document.getElementById('UnitPrice_div').classList.add("d-none");
    if (document.getElementById('UnitPrice').value == "") {
        document.getElementById('UnitPrice').value = "0";
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
for (let i = 1; i <= 9; i++) {
    document.getElementById(`file${i}`).addEventListener("click", () => {
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
    });
    document.getElementById(`file${i}`).addEventListener("input", () => {
        let file = document.getElementById(`file${i}`);
        let p_file = document.getElementById(`p-file${i}`);
        if (file.files[0].size > 2097152 ) {
            alert("檔案過大!");
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
        }
        else if (!file.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
            alert('不是正確圖檔!');
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
        }
        else {
            p_file.innerText = file.files[0].name;
            let url = URL.createObjectURL(file.files[0]);
            document.getElementById("test_div").innerHTML = `<img src='${url}' alt='test' id='test' onload='testLoad(${i})' onerror='testError(${i})'>
            <p id='test_p' >${i}</p > `;
        };
    });
    document.getElementById(`Image${i}-clear`).addEventListener("click", () => {
        document.getElementById(`Image${i}`).value = '無圖片';
    });
}
function testLoad(i) {
    let _img = document.getElementById('test');
    if (_img.height > 1280 || _img.width > 1280) {
        alert("寬或高大於 1280px!");
        document.getElementById(`file${i}`).value = "";
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
    }
}
function testError(i) {
    let _img = document.getElementById('test');
    alert("無法取得圖片訊息!");
    document.getElementById(`file${i}`).value = "";
    document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
}
//end
//start detect leave page
let input = document.querySelectorAll(".no-empty");
input.forEach(element => {
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
}
//end
//start autoresizing
textarea = document.querySelectorAll("textarea");
textarea.forEach(element => {
    element.addEventListener("input", () => {
        autoresizing(element);
    })
});
function autoresizing(element) {
    element.style.height = 'auto';
    element.style.height = element.scrollHeight + 'px';
}
//end