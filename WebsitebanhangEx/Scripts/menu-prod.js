var navItemKhamPha = document.getElementById('mua-sam');
var productMenu = document.getElementById('product-menu');

// Xử lý sự kiện khi di chuột vào "KHÁM PHÁ"
navItemKhamPha.addEventListener('mouseover', function () {
    showMenu();
});

// Xử lý sự kiện khi di chuột ra khỏi "KHÁM PHÁ"
navItemKhamPha.addEventListener('mouseout', function () {
    hideMenu();
});

function showMenu() {
    productMenu.style.display = "block";
}

function hideMenu() {
    // Không ẩn menu ngay lập tức
    setTimeout(function () {
        productMenu.style.display = "none";
    }, 1800); // 3200 ms = 3.2 giây
}
