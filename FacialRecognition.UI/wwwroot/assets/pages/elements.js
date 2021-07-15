"use strict";
$(document).ready(function(){
    $(".md-form-control").each(function() {
        $(this).parent().append('<span class="md-line"></span>');
    });
    $(".md-form-control").change(function() {
        if ($(this).val() == "") {
            $(this).removeClass("md-valid");
        } else {
            $(this).addClass("md-valid");
        }
    });

    $('[name="radio"]').on('change', function () {
        if ($(this).attr('id') == 'isStudent') {
            $('#userID').text('Registration Number');
        } 
        else {
            $('#userID').text('Staff ID');
        }
    });

});