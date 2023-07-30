/*autoresizing*/
textarea = document.querySelectorAll("textarea");
textarea.forEach(element => {
    autoresizing(element);
});
textarea.forEach(element => {
    element.addEventListener("input", () => {
        autoresizing(element);
    })
});
function autoresizing(element) {
    element.style.height = 'auto';
    element.style.height = element.scrollHeight + 'px';
}
/*top btn*/
let topbutton = document.getElementById("topBtn");
// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    let main = document.querySelector("main");
    if (document.body.scrollTop > 75 || document.documentElement.scrollTop > 75) {
        topbutton.style.display = "block";
    } else {
        topbutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
}
// Home Index Search
function getPredictions() {
    var searchText = $('#searchInput').val();
    if (searchText.length > 0) {
        $.ajax({
            url: '/Home/GetPredictions',
            type: 'GET',
            data: { searchText: searchText },
            success: function (data) {
                $('#predictionList').html(data);
            }
        });
    } else {
        $('#predictionList').empty();
    }
}
/**/
function checkDeleteProduct(ProductId) {
    Swal.fire({
        title: '確認移至刪除區?',
        text: "您將無法恢復此狀態！",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '確認刪除!'
    }).then((result) => {
        if (result.isConfirmed) {
            const endpoint = `/Seller/Delete?ProductId=${ProductId}`;
            $.ajax({
                type: "GET",
                url: endpoint,
                dataType: "json",
                success: function (response) {
                    Swal.fire(
                        '刪除!',
                        '你的商品已被刪除.',
                        'success'
                    );
                    location.reload();
                },
                error: function (thrownError) {
                    console.log(thrownError);
                }
            });
        }
    })
}
/**/