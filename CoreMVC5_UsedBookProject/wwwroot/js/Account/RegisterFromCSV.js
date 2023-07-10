//start check img value valid
for (let i = 1; i <= 1; i++) {
    document.getElementById(`file${i}`).addEventListener("click", () => {
        document.getElementById(`p-file${i}`).innerText = "未選擇任何檔案";
        document.getElementById('out').innerHTML = "<p>文件內容將出現在這裡</p>";
    });
    document.getElementById(`file${i}`).addEventListener("input", () => {
        let file = document.getElementById(`file${i}`);
        let p_file = document.getElementById(`p-file${i}`);
        if (file.files[0].size > 2097152) {
            alert("檔案過大!");
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
            document.getElementById('out').innerHTML = "<p>文件內容將出現在這裡</p>";
        }
        else if (!file.files[0].name.match(/\.(csv)$/i)) {
            alert('不是csv檔!');
            file.value = "";
            p_file.innerText = "未選擇任何檔案";
            document.getElementById('out').innerHTML = "<p>文件內容將出現在這裡</p>";
        }
        else {
            p_file.innerText = file.files[0].name;
            readFile();
        };
    });
}
const readFile = () => {
    const reader = new FileReader()
    reader.onload = () => {
        document.getElementById('out').innerHTML = reader.result
    }
    // start reading the file. When it is done, calls the onload event defined above.
    let file1 = document.getElementById("file1");
    reader.readAsBinaryString(file1.files[0])
}
//end