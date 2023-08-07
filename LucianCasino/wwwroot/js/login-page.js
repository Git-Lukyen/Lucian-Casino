$(document).ready(function () {

    $(document).mousemove(function (e) {
        let background = $(".background img");
        let offX = e.clientX * -0.1;
        let offY = e.clientY * -0.1;
        background.css("transform", `translateX(${offX}px) translateY(${offY}px`);
    });

    $("#account-form").validate({
        rules: {
            inputName: {
                required: true,
                lettersonly: true,
                minlength: 3,
                maxlength: 25
            },
            inputEmail: {
                required: true,
                email: true,
            },
            inputPassword: {
                required: true,
                minlength: 6,
                maxlength: 15
            }
        },
        messages: {
            inputName: {
                required: "name is required",
                lettersonly: "cannot contain numbers",
                minlength: "must be at least 3 characters long",
                maxlength: "must be less than 26 characters long"
            },
            inputEmail: {
                required: "email is required",
                email: "must be a valid email",
            },
            inputPassword: {
                required: "password is required",
                minlength: "must be at least 6 characters long",
                maxlength: "must be less than 16 characters long"
            }
        }
    });

    $("#login-btn").click(function(){
        console.log($("form").valid());
    });

});