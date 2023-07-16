//start check img value valid
for (let i = 1; i <= 9; i++) {
    let preImg = document.getElementById(`Image${i}`).value;
    if (preImg != "無圖片") {
        document.getElementById(`img${i}Div`).innerHTML = `<img src='Images/Home/${preImg}' class='d-block m-auto' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
    }
    document.getElementById(`file${i}`).addEventListener("click", () => {
        document.getElementById(`p-file${i}`).innerText = "未選擇照片";
        let preImg = document.getElementById(`Image${i}`).value;
        if (preImg != "無圖片") {
            document.getElementById(`img${i}Div`).innerHTML = `<img src='Images/Home/${preImg}' class='d-block m-auto' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        }
    });
    document.getElementById(`file${i}`).addEventListener("input", () => {
        let file = document.getElementById(`file${i}`);
        let p_file = document.getElementById(`p-file${i}`);
        if (file.files[0].size > 2097152) {
            alert("檔案過大!");
            file.value = "";
            p_file.innerText = "未選擇照片";
            let preImg = document.getElementById(`Image${i}`).value;
            document.getElementById(`img${i}Div`).innerHTML = `<img src='Images/Home/${preImg}' class='d-block m-auto' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        }
        else if (!file.files[0].name.match(/\.(jpg|jpeg|png|gif)$/i)) {
            alert('不是正確圖檔!');
            file.value = "";
            p_file.innerText = "未選擇照片";
            let preImg = document.getElementById(`Image${i}`).value;
            document.getElementById(`img${i}Div`).innerHTML = `<img src='Images/Home/${preImg}' class='d-block m-auto' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        }
        else {
            p_file.innerText = file.files[0].name;
            let url = URL.createObjectURL(file.files[0]);
            document.getElementById(`img${i}Div`).innerHTML = `<img src='${url}' class='w-100' alt='img${i}' id='img${i}' onload='testLoad(${i})' onerror='testError(${i})'>`;
        };
    });
    document.getElementById(`Image${i}-clear`).addEventListener("click", () => {
        document.getElementById(`Image${i}`).value = '無圖片';
        document.getElementById(`file${i}`).value = '';
        document.getElementById(`p-file${i}`).innerText = "未選擇照片";
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