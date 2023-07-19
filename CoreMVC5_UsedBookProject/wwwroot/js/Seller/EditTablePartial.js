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
        document.getElementById('UnitPrice_div').classList.remove("d-none");
        if (document.getElementById('UnitPrice').value == "-1000") {
            document.getElementById('UnitPrice').value = "";
        }
    }
    if (check1.checked && !check2.checked) {
        document.getElementById('Trade').value = "買賣";
        document.getElementById('UnitPrice_div').classList.remove("d-none");
        if (document.getElementById('UnitPrice').value == "-1000") {
            document.getElementById('UnitPrice').value = "";
        }
    }
    if (!check1.checked && check2.checked) {
        document.getElementById('Trade').value = "交換";
        document.getElementById('UnitPrice_div').classList.add("d-none");
        if (document.getElementById('UnitPrice').value == "") {
            document.getElementById('UnitPrice').value = "-1000";
        }
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
for (let i = 1; i <= 9; i++) {
    let preImg = document.getElementById(`Image${i}`).value;
    let preId = document.getElementById(`ProductId`).value;
    let preUserId = document.getElementById(`CreateBy`).value;
    if (preImg != "無圖片") {
        document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
    }
    document.getElementById(`file${i}`).addEventListener("click", () => {
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
        let preImg = document.getElementById(`Image${i}`).value;
        let preId = document.getElementById(`ProductId`).value;
        let preUserId = document.getElementById(`CreateBy`).value;
        if (preImg != "無圖片") {
            document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
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
            document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        }
        else if (!file.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
            alert('不是正確圖檔!');
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
            let preImg = document.getElementById(`Image${i}`).value;
            let preId = document.getElementById(`ProductId`).value;
            let preUserId = document.getElementById(`CreateBy`).value;
            document.getElementById(`img${i}Div`).innerHTML = `<img src='/Images/Users/${preUserId}/Products/${preId}/${preImg}' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        }
        else {
            p_file.innerText = file.files[0].name;
            let url = URL.createObjectURL(file.files[0]);
            document.getElementById(`img${i}Div`).innerHTML = `<img src='${url}' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        };
    });
    document.getElementById(`Image${i}-clear`).addEventListener("click", () => {
        document.getElementById(`Image${i}`).value = '無圖片';
        document.getElementById(`file${i}`).value = '';
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
        document.getElementById(`img${i}Div`).innerHTML = `<div class="m-auto"><i class="bi bi-images text-center m-auto d-block" style="font-size: 50px;"></i><p>No image</p></div>`;
    });
}
function testLoad(i) {
    let _img = document.getElementById(`img${i}`);
    if (_img.height > 1280 || _img.width > 1280) {
        document.getElementById(`file${i}`).value = "";
        document.getElementById(`p-file${i}`).innerText = "寬或高大於 1280px!";
        document.getElementById(`img${i}Div`).innerHTML = `<div class="m-auto"><i class="bi bi-images text-center m-auto d-block" style="font-size: 50px;"></i><p>No image</p></div>`;
    }
}
function testError(i) {
    document.getElementById(`file${i}`).value = "";
    document.getElementById(`p-file${i}`).innerText = "無法取得圖片訊息!";
    document.getElementById(`img${i}Div`).innerHTML = `<div class="m-auto"><i class="bi bi-images text-center m-auto d-block" style="font-size: 50px;"></i><p>No image</p></div>`;
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
