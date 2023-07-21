let showMessage = document.getElementById('showMessage');
if (showMessage.innerText == '使用者資訊變更成功!') {
    Swal.fire(
        '使用者資訊變更成功!',
        '',
        'success'
    )
}
else if (showMessage.innerText == '使用者密碼變更成功!') {
    Swal.fire(
        '使用者密碼變更成功!',
        '',
        'success'
    )
}