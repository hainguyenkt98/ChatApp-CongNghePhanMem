$(document).ready(function(){
    $(document).on("mouseover",".chatList",function(){
        $(this).css("background-color", "#36393F");
        $(this).css("border-radius", "5px");
    });
    $(document).on("mouseout", ".chatList",function(){
        $(this).css("background-color", "#444753");
        $(this).css("border-radius", "0px");
    });
});