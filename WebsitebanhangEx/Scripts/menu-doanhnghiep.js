var navItemDoanhNghiep = document.getElementById('doanh-nghiep');
var productMenu2 = document.getElementById('product-menu2');

// Xử lý sự kiện khi di chuột vào "KHÁM PHÁ"
navItemDoanhNghiep.addEventListener('mouseover', function () {
    showMenu2();
});

// Xử lý sự kiện khi di chuột ra khỏi "KHÁM PHÁ"
navItemDoanhNghiep.addEventListener('mouseout', function () {
    hideMenu2();
});

function showMenu2() {
    productMenu2.style.display = "block";
}

function hideMenu2() {
    // Không ẩn menu ngay lập tức
    setTimeout(function () {
        productMenu2.style.display = "none";
    }, 1800); // 3200 ms = 3.2 giây
}
