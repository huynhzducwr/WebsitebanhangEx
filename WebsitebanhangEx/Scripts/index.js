
const listImage = document.querySelector('.list-images');
const imgs = listImage.getElementsByTagName('img'); // Chỉ lấy ảnh bên trong .list-images
const btnLeft = document.querySelectorAll('.btn-left')
const btnRight = document.querySelectorAll('.btn-right')
const length = imgs.length
let current = 0


const handleChangeSlide = () => {
    if (current == length - 1) {
        current = 0
        let width = imgs[0].offsetWidth
        listImage.style.transform = `translateX(0px)`
        document.querySelector('.active').classList.remove('active')
        document.querySelector('.index-items-' + current).classList.add('active')


    } else {
        current++
        let width = imgs[0].offsetWidth
        listImage.style.transform = `translateX(${width * -1 * current}px)`
        document.querySelector('.active').classList.remove('active')
        document.querySelector('.index-items-' + current).classList.add('active')
    }
}
let handleEventChangeSlide = setInterval(handleChangeSlide, 4000)
btnRight.forEach(btn => {
    btn.addEventListener('click', () => {
        clearInterval(handleEventChangeSlide)
        handleChangeSlide();
        handleEventChangeSlide = setInterval(handleChangeSlide, 4000)
    });
});

btnLeft.forEach(btn => {
    btn.addEventListener('click', () => {
        clearInterval(handleEventChangeSlide)
        if (current == 0) {
            current = length - 1;
            let width = imgs[0].offsetWidth;
            listImage.style.transform = `translateX(${width * -1 * current}px)`;
            document.querySelector('.active').classList.remove('active')
            document.querySelector('.index-items-' + current).classList.add('active')
        } else {
            current--;
            let width = imgs[0].offsetWidth;
            listImage.style.transform = `translateX(${width * -1 * current}px)`;
            document.querySelector('.active').classList.remove('active')
            document.querySelector('.index-items-' + current).classList.add('active')
        }
        handleEventChangeSlide = setInterval(handleChangeSlide, 4000)
    });
});




// Gọi hàm lấy kích thước ảnh ban đầu
