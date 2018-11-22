$(document).ready(function () {

    $(".left-chat li .chatList").off('click').on('click', function () {
        var conversationID = $(this).data('conversationid')
        var conversationname = $(this).find('.desc h5').text();
        $.ajax({
            url: "/MainChat/GetListMessage",
            data: { conversationID: conversationID },
            dataType: "json",
            type: "post",
            success: function (result) {
                $('.headRight-chat h3').empty();
                $('.headRight-chat small').empty();
                $('.right-chat .message ul').empty();
                var s = '';
                for (var i = 0; i < result.length; i++) {
                    var parsedDate = new Date(parseInt(result[i].Creationtime.substr(6)));
                    var jsDate = new Date(parsedDate);
                    if (result[i].Direction == "inbound") {
                        s += '<li class="msg-left">'
                            + '<div class="msg-left-sub">'
                            + '<img src="/Images/img-friend.png">'
                            +      '<div class="msg-desc">'
                            +           result[i].Contents
                            +       '</div>'
                            +       '<small>' + jsDate.toLocaleString('en-GB')  +'</small>'
                            + '</div>'
                            + '</li >'
                        
                    } else {
                        s += '<li class="msg-right">'
                            + '<div class="msg-left-sub">'
                            + '<img src="/Images/img-friend.png">'
                            + '<div class="msg-desc">'
                            + result[i].Contents
                            + '</div>'
                            + '<small>' + jsDate.toLocaleString('en-GB') + '</small>'
                            + '</div>'
                            + '</li >'
                    }
                    
                }
                $('.right-chat .message ul').html(s);
                $('.headRight-chat h3').text(conversationname);
                scrollBottom();
            }
        });
    });
    $(".online-list li .onlineitem").off('click').on('click', function () {
        var conversationID = $(this).data('conversationid')
        var conversationname = $(this).find('.desc h5').text();
        $.ajax({
            url: "/MainChat/GetListMessage",
            data: { conversationID: conversationID },
            dataType: "json",
            type: "post",
            success: function (result) {
                $('.headRight-chat h3').empty();
                $('.headRight-chat small').empty();
                $('.right-chat .message ul').empty();
                var s = '';
                for (var i = 0; i < result.length; i++) {
                    var parsedDate = new Date(parseInt(result[i].Creationtime.substr(6)));
                    var jsDate = new Date(parsedDate);
                    if (result[i].Direction == "inbound") {
                        s += '<li class="msg-left">'
                            + '<div class="msg-left-sub">'
                            + '<img src="/Images/img-friend.png">'
                            + '<div class="msg-desc">'
                            + result[i].Contents
                            + '</div>'
                            + '<small>' + jsDate.toLocaleString('en-GB') + '</small>'
                            + '</div>'
                            + '</li >'

                    } else {
                        s += '<li class="msg-right">'
                            + '<div class="msg-left-sub">'
                            + '<img src="/Images/img-friend.png">'
                            + '<div class="msg-desc">'
                            + result[i].Contents
                            + '</div>'
                            + '<small>' + jsDate.toLocaleString('en-GB') + '</small>'
                            + '</div>'
                            + '</li >'
                    }
                }
                $('.right-chat .message ul').html(s);
                $('.headRight-chat h3').text(conversationname);
                scrollBottom();
            }
        });
    });

    function scrollBottom() {
        $('.message.mCustomScrollbar').animate({
            scrollTop: $('.message.mCustomScrollbar')[0].scrollHeight
        }, 200);
    }
}); 