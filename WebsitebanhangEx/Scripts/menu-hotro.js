var navItemDoanhNghiep = document.getElementById('ho-tro');
var productMenu3 = document.getElementById('product-menu3');

// Xử lý sự kiện khi di chuột vào "KHÁM PHÁ"
navItemDoanhNghiep.addEventListener('mouseover', function () {
    showMenu3();
});

// Xử lý sự kiện khi di chuột ra khỏi "KHÁM PHÁ"
navItemDoanhNghiep.addEventListener('mouseout', function () {
    hideMenu3();
});

function showMenu3() {
    productMenu3.style.display = "block";
}

function hideMenu3() {
    // Không ẩn menu ngay lập tức
    setTimeout(function () {
        productMenu3.style.display = "none";
    }, 1800); // 3200 ms = 3.2 giây
}
