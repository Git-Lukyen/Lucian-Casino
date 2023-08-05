$(document).ready(function () {

    $(document).mousemove(function (e) {
        let background = $(".background img");
        let offX = e.clientX * -0.1;
        let offY = e.clientY * -0.1;
        background.css("transform", `translateX(${offX}px) translateY(${offY}px`);
    });
    
});