$(document).ready(function () {
    // ...

    // 刪除圖片的按鈕事件處理
    $('#deleteButton').click(function () {
        var imageId = 1; // 假設要刪除的圖片Id為1，你可以根據需求動態獲取圖片Id

        // 執行刪除圖片的相關邏輯
        $.ajax({
            url: '/Home/DeleteImage', // 替換為刪除圖片的路由
            type: 'POST',
            data: { id: imageId },
            success: function (response) {
                if (response.success) {
                    // 刪除成功後重新載入輪播圖片
                    reloadCarousel();
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    // ...
});
