function AddToShoppingcart(id) {
    var url = `/Buyer/AddToShoppingcart?id=${id}`;
    $.ajax({
        type: "POST",
        url: url,
        data: id,
        error: function (xhr, status, error) { },
        success: function (data) {
            if (data == "自己的商品") {
                Swal.fire(
                    '這是自己的商品',
                    '',
                    'error'
                )
            }
            else if (data == "成功") {
                Swal.fire(
                    '加入購物車了',
                    '',
                    'success'
                )
            }
            else if (data == "失敗") {
                Swal.fire(
                    '已經在購物車囉',
                    '',
                    'error'
                )
            }
        }
    });
    return false;
}
var shoppingcart = document.getElementById("shoppingcart");
shoppingcart.addEventListener("click", () => {
    var id = document.getElementById("shoppingcart").value;
    var myCookie = getCookie("Login");
    if (myCookie == null) {
        Swal.fire({
            icon: 'error',
            title: '請先登入後再操作',
            text: '',
            footer: '<a href="/Account/Login">登入</a>'
        })
    }
    else {
        AddToShoppingcart(id);
    }
})
function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end == -1) {
            end = dc.length;
        }
    }
    // because unescape has been deprecated, replaced with decodeURI
    //return unescape(dc.substring(begin + prefix.length, end));
    return decodeURI(dc.substring(begin + prefix.length, end));
}
function checklogin() {
    var myCookie = getCookie("Login");
    let createby = document.getElementById("CreateBy");
    let meid = document.getElementById("MeId");
    if (myCookie == null) {
        Swal.fire({
            icon: 'error',
            title: '請先登入後再操作',
            text: '',
            footer: '<a href="/Account/Login">登入</a>'
        })
    }
    else if (createby.value == meid.value) {
        Swal.fire(
            '這是自己的商品',
            '',
            'error'
        )
    }
}