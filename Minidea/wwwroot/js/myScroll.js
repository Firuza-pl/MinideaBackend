$(function () {


    $("#next-ads").on('click', function () {
        $('#carouselExampleControls4').carousel('next');
    });
    $("#prev-ads").on('click', function () {
        $('#carouselExampleControls4').carousel('prev');
    });
    window.FakeLoader.init({
        auto_hide: true
    });

    //fill carusel when open
    $('#exampleModal').on('shown.bs.modal', function (e) {
        var id = $(e.relatedTarget).attr('id');
        try {
            $.ajax({
                url: "/Home/Load",
                type: "get",
                imageurl: String,
                data: { id: id },
                dataType: "json",
                contentType: "application/json",
                beforeSend: function () {
                    $("#loading-image").show();
                },
                success: function (question) {
                    $(JSON.parse(question)).each(function (index, item) {
                        $('#carrusel').append($('<div class="carousel-item ads-background"><div style="height:450px;"><img class="img-fluid d-block w-100" src=/img/' + item.PhotoURL + '></div><p class="item-intro text-muted">' + item.AreaTitle + '</p></div>'));
                        $('#carrusel .carousel-item').first().addClass('active');
                        $("#loading-image").hide();
                        $('#exampleModal').addClass('backColor');
                    });
                },
                timeout: 15000,

                error: function () {
                    alert("error");
                }
            });
        }
        catch (e) {
            alert(e.message);
        }
    });

    //clear carusel after close
    $("#exampleModal").on("hidden.bs.modal", function () {
        $('#carrusel').empty();
    });

    $(window).scroll(function () {
        var $nav = $(".fixed-top");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());

        if ($(this).scrollTop() > $nav.height()) {
            $('.navbar .navbar-brand img').attr('src', '/img/staticdata/logo-transparent.png');
            $('.navbar .navbar-brand img').css('transition', 'opacity 1s ease-in-out');
        }
        if ($(this).scrollTop() < $nav.height()) {
            $('.navbar .navbar-brand img').attr('src', '/img/staticdata/logo-coral.png');
            $('.navbar .navbar-brand img').css('transition', 'opacity 1s ease-in-out');
        }

        var middleScreen = window.matchMedia("(min-width: 300px) and (max-width: 1200px");

        if (middleScreen.matches) {
            if ($(this).scrollTop() > $nav.height()) {
                console.log("1");
                $('.navbar-brand').css({
                    'height': '80px',
                    'padding-top': '27px'
                });

                $('.navbar-light .navbar-toggler').css({
                    'color': 'rgb(0 0 0 / 35%)',
                    'border-color': 'rgb(0 0 0 / 35%)',
                    'margin-top' : '27px'
                });
             
            }
            if ($(this).scrollTop() < $nav.height()) {
                console.log("2");

                $('.navbar-brand').css({
                    'height': '80px',
                    'padding-top': '27px'
                });

                $('.navbar-light .navbar-toggler').css({
                    'color': 'rgb(255 255 255 / 55%)',
                    'border-color': 'rgb(255 255 255 / 55%)'
                });
              
            }
        }

    })

});