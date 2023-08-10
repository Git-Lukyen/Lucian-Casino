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

    let basePath = "https://localhost:44364/lc/";

    $("#signup-btn").click(function () {
        if (!$("#account-form").valid())
            return;

        let userDto = {
            "Name": $("[name='inputName']").val(),
            "Email": $("[name='inputEmail']").val(),
            "Password": $("[name='inputPassword']").val()
        };

        $.ajax({
            url: basePath + "users/add/single",
            type: "POST",
            data: JSON.stringify(userDto),
            contentType: 'application/json',
            success: function (data) {
                $.removeCookie("Authorization");
                $.cookie("Authorization", "Bearer " + data, {path: "/lc/home"});
                window.location.replace(basePath + "home");
            },
            error: function(data) {

                $.growl({
                    title: "Error signing up!",
                    message: "Invalid email or username.",
                    duration: "5000",
                    location:"bl",
                    style: "error"
                });
                
            }
        });

    });

    $("#login-btn").click(function () {
        if (!$("#account-form").valid())
            return;

        let userDto = {
            "Name": $("[name='inputName']").val(),
            "Email": $("[name='inputEmail']").val(),
            "Password": $("[name='inputPassword']").val()
        };

        $.ajax({
            url: basePath + "users/login",
            type: "POST",
            data: JSON.stringify(userDto),
            contentType: 'application/json',
            success: function (data) {
                $.removeCookie("Authorization");
                $.cookie("Authorization", "Bearer " + data, {path: "/lc/home"});
                window.location.replace(basePath + "home");
            },
            error: function(error) {
                
                $.growl({
                    title: "Error signing in!",
                    message: "Invalid username or password.",
                    duration: "5000",
                    location:"bl",
                    style: "error"
                });
                
            }
        });

    });



});