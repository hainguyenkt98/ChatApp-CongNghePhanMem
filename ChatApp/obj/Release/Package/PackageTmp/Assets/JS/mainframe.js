$(function () {
    $(".sidebar").click(function () {
        $('.sidebar').removeClass("sidebar-selected");
        $(this).addClass("sidebar-selected");
    });
    $('.toggle-wrap').on('click', function () {
        $(this).toggleClass('active');
        $('aside').animate({ width: 'toggle' }, 200);
    });
});  