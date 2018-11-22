$(document).ready(function(){
  	$(".chatList").mouseover(function(){
        $(this).css("background-color", "#36393F");
        $(this).css("border-radius", "5px");
    });
    $(".chatList").mouseout(function(){
        $(this).css("background-color", "#444753");
        $(this).css("border-radius", "0px");
    });
});