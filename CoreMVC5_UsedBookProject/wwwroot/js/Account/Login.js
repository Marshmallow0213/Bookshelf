let ErrorMessage = document.getElementById('ErrorMessage')
if (ErrorMessage.innerText == "帳號不存在") {
    Swal.fire(
        '帳號不存在!!!',
        '',
        'error'
    )
}
else if (ErrorMessage.innerText == "帳號密碼有錯") {
    Swal.fire(
        '帳號密碼有錯!!!',
        '',
        'error'
    )
}