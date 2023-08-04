$( document ).ready(function() {

    $(document).mousemove(function(e){
        let background = $(".background img");
        let offX = e.pageX * -0.1;
        let offY = e.pageY * -0.1;
        background.css("transform", `translateX(${offX}px) translateY(${offY}px`);
    });

});