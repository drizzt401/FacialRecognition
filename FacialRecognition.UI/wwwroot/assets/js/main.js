/*Template Name: Quantum Able Bootstrap 4 Admin Template
 Author: Codedthemes
 Email: support@phopenixcoded.net
 File: main.js
 */
'use strict';
$(window).on('load', function() {
    var $window = $(window);
    $('.loader-bar').animate({ width:$window.width()},2000);
    setTimeout(function() {
        while ($('.loader-bar').width() == $window.width()) {
            removeloader();
            break;
        }
    }, 2500);

    //Welcome Message (not for login page)
    function notify(message, type) {
        $.growl({
            message: message
        }, {
            type: type,
            allow_dismiss: false,
            label: 'Cancel',
            className: 'btn-xs btn-inverse',
            placement: {
                from: 'bottom',
                align: 'right'
            },
            delay: 2500,
            animate: {
                enter: 'animated fadeInRight',
                exit: 'animated fadeOutRight'
            },
            offset: {
                x: 30,
                y: 30
            }
        });
    };

    // notify('Welcome to Quantum Admin', 'inverse');
    $('.loader-bg').fadeOut('slow');

});
// function removeloader(){
//     $('.loader-bg').fadeOut('slow', function() {
//         $('.loader-bg').remove();
//     });
// };
$(document).ready(function() {

    //sidebar dropdown open
    $(".designation").on('click', function() {
        $(".extra-profile-list").slideToggle();
    });

    /*chatbar js start*/
    /*chat box scroll*/
    var a = $(window).height() - 50;
    $(".main-friend-list ").slimScroll({
        height: a,
        allowPageScroll: false,
        wheelStep: 5,
        color: '#1b8bf9'
    });

    // search
    $("#search-friends").on("keyup", function() {

        var g = $(this).val().toLowerCase();
        $(".friendlist-box .media-body .friend-header").each(function() {

            var s = $(this).text().toLowerCase();
            $(this).closest('.friendlist-box')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
        });
    });

    // open chat box
    $('.displayChatbox').on('click', function() {

        var options = {
            direction: 'right'
        };
        $('.showChat').toggle('slide', options, 500);
    });
    //open friend chat
    $('.friendlist-box').on('click', function() {


        var options = {
            direction: 'right'
        };
        $('.showChat_inner').toggle('slide', options, 500);
    });
    //back to main chatbar
    $('.back_chatBox').on('click', function() {
        var options = {
            direction: 'right'
        };
        $('.showChat_inner').toggle('slide', options, 500);
        $('.showChat').css('display', 'block');
    });
    /*chatbar js start*/

    $("[data-toggle='utility-menu']").on('click', function() {
        $(this).next().slideToggle(300);
        $(this).toggleClass('open');
        return false;
    });
});



/*Show tooltip*/
$('[data-toggle="tooltip"]').tooltip();
$('[data-toggle="popover"]').popover({
    animation: true,
    delay: {
        show: 100,
        hide: 100
    }
});


$.pushMenu = {
    activate: function(toggleBtn) {

        //Enable sidebar toggle
        $(toggleBtn).on('click', function(e) {
            e.preventDefault();

            //Enable sidebar push menu
            if ($(window).width() > (767)) {
                if ($("body").hasClass('sidebar-collapse')) {
                    $("body").removeClass('sidebar-collapse').trigger('expanded.pushMenu');
                } else {
                    $("body").addClass('sidebar-collapse').trigger('collapsed.pushMenu');
                }
            }
            //Handle sidebar push menu for small screens
            else {
                if ($("body").hasClass('sidebar-open')) {
                    $("body").removeClass('sidebar-open').removeClass('sidebar-collapse').trigger('collapsed.pushMenu');
                } else {
                    $("body").addClass('sidebar-open').trigger('expanded.pushMenu');
                }
            }
            if ($('body').hasClass('fixed') && $('body').hasClass('sidebar-mini') && $('body').hasClass('sidebar-collapse')) {
                $('.sidebar').css("overflow", "visible");
                $('.main-sidebar').find(".slimScrollDiv").css("overflow", "visible");
            }
            if ($('body').hasClass('only-sidebar')) {
                $('.sidebar').css("overflow", "visible");
                $('.main-sidebar').find(".slimScrollDiv").css("overflow", "visible");
            };
        });

        $(".content-wrapper").on('click', function() {
            //Enable hide menu when clicking on the content-wrapper on small screens
            if ($(window).width() <= (767) && $("body").hasClass("sidebar-open")) {
                $("body").removeClass('sidebar-open');
            }
        });
    }
};
$.tree = function(menu) {
    var _this = this;
    var animationSpeed = 200;
    $(document).on('click', menu + ' li a', function(e) {
        //Get the clicked link and the next element
        var $this = $(this);
        var checkElement = $this.next();

        //Check if the next element is a menu and is visible
        if ((checkElement.is('.treeview-menu')) && (checkElement.is(':visible'))) {
            //Close the menu
            checkElement.slideUp(animationSpeed, function() {
                checkElement.removeClass('menu-open');
                //Fix the layout in case the sidebar stretches over the height of the window
                //_this.layout.fix();
            });
            checkElement.parent("li").removeClass("active");
        }
        //If the menu is not visible
        else if ((checkElement.is('.treeview-menu')) && (!checkElement.is(':visible'))) {
            //Get the parent menu
            var parent = $this.parents('ul').first();
            //Close all open menus within the parent
            var ul = parent.find('ul:visible').slideUp(animationSpeed);
            //Remove the menu-open class from the parent
            ul.removeClass('menu-open');
            //Get the parent li
            var parent_li = $this.parent("li");

            //Open the target menu and add the menu-open class
            checkElement.slideDown(animationSpeed, function() {
                //Add the class active to the parent li
                checkElement.addClass('menu-open');
                parent.find('li.active').removeClass('active');
                parent_li.addClass('active');
            });
        }
        //if this isn't a link, prevent the page from being redirected
        if (checkElement.is('.treeview-menu')) {
            e.preventDefault();
        }
    });
};
// Activate sidenav treemenu
$.tree('.sidebar');
$.pushMenu.activate("[data-toggle='offcanvas']");
// side button js code end


// Start [ Menu-bottom ]
$(document).ready(function() {
    $(".dropup-mega, .dropup").hover(function() {
        var dropdownMenu = $(this).children(".dropdown-menu");
        $(this).toggleClass("open");
    });
});
// End [ Menu ]
