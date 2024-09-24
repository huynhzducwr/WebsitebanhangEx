var navItemKhamPha = document.getElementById('kham-pha');
var productMenu1 = document.getElementById('product-menu1');

// Xử lý sự kiện khi di chuột vào "KHÁM PHÁ"
navItemKhamPha.addEventListener('mouseover', function () {
    showMenu1();
});

// Xử lý sự kiện khi di chuột ra khỏi "KHÁM PHÁ"
navItemKhamPha.addEventListener('mouseout', function () {
    hideMenu1();
});

function showMenu1() {
    productMenu1.style.display = "block";
}

function hideMenu1() {
    // Không ẩn menu ngay lập tức
    setTimeout(function () {
        productMenu1.style.display = "none";
    }, 1800); // 3200 ms = 3.2 giây
}

