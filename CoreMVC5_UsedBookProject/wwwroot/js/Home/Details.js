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
        window.location.href = `/Account/Login?ReturnUrl=ProductId-${id}`;
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
function checklogin(ProductId, CreateBy, Trade, Order) {
    var myCookie = getCookie("Login");
    let createby = document.getElementById("CreateBy");
    let meid = document.getElementById("MeId");
    if (myCookie == null) {
        var id = document.getElementById("shoppingcart").value;
        window.location.href = `/Account/Login?ReturnUrl=ProductId-${id}`;
    }
    else if (createby.value == meid.value) {
        Swal.fire(
            '這是自己的商品',
            '',
            'error'
        )
    }
    else {
        if (Order == "買賣") {
            Swal.fire({
                title: '確認要送出購買訂單?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: '確認送出!'
            }).then((result) => {
                if (result.isConfirmed) {
                    const endpoint = `/Buyer/CreateOrder?ProductId=${ProductId}&Sellername=${CreateBy}&trade=${Trade}&Order=${Order}`;
                    $.ajax({
                        type: "GET",
                        url: endpoint,
                        dataType: "text",
                        success: function (data) {
                            if (data == "true") {
                                Swal.fire(
                                    '成功!',
                                    '訂單建立成功.',
                                    'success'
                                );
                                setTimeout(function () {
                                    location.replace('/Buyer/MySales?trade=買賣');
                                }, 1000);
                            }
                            else if (data == "selfporducts") {
                                Swal.fire({
                                    icon: 'error',
                                    title: '這是自己的商品',
                                    text: ''
                                })
                            }
                            else if (data == "false") {
                                Swal.fire({
                                    icon: 'error',
                                    title: '錯誤',
                                    text: '出了些問題!'
                                })
                            }
                        },
                        error: function (thrownError) {
                            console.log(thrownError);
                        }
                    });
                }
            })
        }
        else if (Order == "交換") {
            Swal.fire({
                title: '確認要送出交換訂單?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: '確認送出!'
            }).then((result) => {
                if (result.isConfirmed) {
                    const endpoint = `/Buyer/CreateOrder?ProductId=${ProductId}&Sellername=${CreateBy}&trade=${Trade}&Order=${Order}`;
                    $.ajax({
                        type: "GET",
                        url: endpoint,
                        dataType: "text",
                        success: function (data) {
                            console.log(data);
                            console.log(data == "needselfporducts");
                            if (data == "needselfporducts") {
                                Swal.fire({
                                    icon: 'error',
                                    title: '交換商品',
                                    text: '你需要先上架自己的交換商品!'
                                })
                            }
                            else if (data == "true") {
                                Swal.fire(
                                    '成功!',
                                    '訂單建立成功.',
                                    'success'
                                );
                                setTimeout(function () {
                                    location.replace('/Buyer/MySales?trade=交換');
                                }, 1000);
                            }
                            else if (data == "selfporducts") {
                                Swal.fire({
                                    icon: 'error',
                                    title: '這是自己的商品',
                                    text: ''
                                })
                            }
                            else if (data == "false") {
                                Swal.fire({
                                    icon: 'error',
                                    title: '錯誤',
                                    text: '出了些問題!'
                                })
                            }
                        },
                        error: function (thrownError) {
                            console.log(thrownError);
                        }
                    });
                }
            })
        }
        else {
            Swal.fire({
                icon: 'error',
                title: '錯誤',
                text: '出了些問題！'
            })
        }
    }
}
let error = document.getElementById("error");
if (error.innerText == "你不能購買自己的商品!") {
    Swal.fire(
        '你不能購買自己的商品',
        '',
        'error'
    )
}
else if (error.innerText == "你需要先上架自己的交換商品!") {
    Swal.fire(
        '你需要先上架自己的交換商品',
        '',
        'error'
    )
}