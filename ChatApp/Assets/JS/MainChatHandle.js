$(document).ready(function () {
    var conversationidcurrent = "";
    var pathImageSender = "";
    var pathimage = $('.img-responsive.img-rounded').attr('src');
    var isGroup;
    var pathImageConversation = "";
    var nameconversation = "";
    var useridcurrent = "";
    var useridmain = "";
    $(document).on("click", ".left-chat li .chatList", function () {

        //$(this).
        //Chuyen sang trang thai da doc

        var conversationID = $(this).data('conversationid');
        var conversationname = $(this).find('.desc h5').text();

        conversationidcurrent = $(this).data('conversationid');

        var group = $(this).find('.img img').attr('isgroup');
        if (group == 'false') {
            isGroup = false;

        } else {
            isGroup = true;
        }
        nameconversation = $(this).find('h5').text();

        $(this).find('.desc small.mess-unread').addClass('mess-readed').removeClass('mess-unread');
        setReadOnlineList(conversationID);
        setConversationReaded(conversationID);
        //End set status

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
                    var pathImage = "";
                    if (result[i].PathImage == null || result[i].PathImage == "") {
                        pathImage = '/Images/img-friend.png';
                    } else {
                        pathImage = result[i].PathImage;
                    }

                    if (result[i].Direction == "inbound") {
                        s += '<li class="msg-left">'
                            + '<div class="msg-left-sub">'
                            + '<img src="' + pathImage + '">'
                            + '<div class="msg-desc">'
                            + result[i].Contents
                            + '</div>'
                            + '<small>' + jsDate.toLocaleString('en-GB') + '</small>'
                            + '</div>'
                            + '</li >'

                    } else {
                        s += '<li class="msg-right">'
                            + '<div class="msg-left-sub">'
                            + '<img src="' + pathImage + '">'
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
    //Lich su tro chuyen

    $(document).on("click", ".online-list li .onlineitem", function () {
        var conversationID = $(this).data('conversationid');
        var conversationname = $(this).find('.desc h5').text();
        nameconversation = $(this).find('.desc h5').text();

        conversationidcurrent = $(this).data('conversationid');

        var element = $(this).find('.img img');
        var group = $(element).attr('isgroup');

        if (group == 'false') {
            isGroup = false;

        } else {
            isGroup = true;
        }
        //End setup status


        $(this).find('.tickread').addClass('readfriend');
        setReadConversation(conversationID);
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

                    var pathImage = "";
                    if (result[i].PathImage == null || result[i].PathImage == "") {
                        pathImage = '/Images/img-friend.png';
                    } else {
                        pathImage = result[i].PathImage;
                    }

                    if (result[i].Direction == "inbound") {
                        s += '<li class="msg-left">'
                            + '<div class="msg-left-sub">'
                            + '<img src="' + pathImage + '">'
                            + '<div class="msg-desc">'
                            + result[i].Contents
                            + '</div>'
                            + '<small>' + jsDate.toLocaleString('en-GB') + '</small>'
                            + '</div>'
                            + '</li >'

                    } else {
                        s += '<li class="msg-right">'
                            + '<div class="msg-left-sub">'
                            + '<img src="' + pathImage + '">'
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
    // Danh sach ban

    function scrollBottom() {
        $('.message.mCustomScrollbar').animate({
            scrollTop: $('.message.mCustomScrollbar')[0].scrollHeight
        }, 200);
    }


    //Chat Start

    useridmain = $('.user-info').data('userid');
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.


    chat.client.NotificationUserOnline = function (userid) {
        $(".onlineitem[data-userid=" + userid + "]").find('.img .fa.fa-circle.offline').removeClass('offline').addClass('online');
    }
    //Bat su kien online friend
    chat.client.NotificationUserOffline = function (userid) {
        $(".onlineitem[data-userid=" + userid + "]").find('.img .fa.fa-circle.online').removeClass('online').addClass('offline');
    }
    //Bat su kien offline friend

    chat.client.SendMessage = function (content, userid, conversationid) {
        var presentConversationName = "";
        var usersendmessage;
        pathImageSender;
        var element = $(".chatList[data-conversationid$=" + conversationid + "]");
        if (isGroup == false) {
            presentConversationName = "";
            usersendmessage = setCurrentPathImage(userid);
            pathImageSender = usersendmessage.PathImage;
            element = $(".chatList[data-conversationid$=" + conversationid + "]");
        } else {
            pathImageSender = "/Images/group.png";
        }


        presentConversationName = getNameConversation(conversationid, userid);
        //Set lai name conversation khi tin nhan chua co trong lich su

        if (conversationid == conversationidcurrent) {

            var currentdate = new Date();
            var s = '<li class="msg-left">'
                + '<div class="msg-left-sub">'
                + '<img src="' + pathImageSender + '">'
                + '<div class="msg-desc">'
                + content
                + '</div>'
                + '<small>' + currentdate.toLocaleString('en-GB') + '</small>'
                + '</div>'
                + '</li >';
            $('.right-chat .message.mCustomScrollbar ul').append(s);
            $('#messagecontent').val("");
            scrollBottom();

            resetConversation(conversationid, content, isGroup, pathImageSender, nameconversation, true);
            setComversationReaded(conversationid);
            //setUnreadOnlineList(conversationid, content, isGroup, pathImageSender, nameconversation);

        } else {
            //Khong phai dang noi chuyen thong bao 2 ben kia
            resetConversation(conversationid, content, isGroup, pathImageSender, presentConversationName, false);
        }
    };




    $.connection.hub.start().done(function () {
        var connectionId = $.connection.hub.id;
        updateConnectionId(connectionId);//Cap nhat id vao db
        chat.server.SetupAllConversation(useridmain);// Khai bao cac group



        $(document).keypress(function (e) {
            if (e.which == 13) {
                var content = $('#messagecontent').val();
                if (conversationidcurrent != "" && content.trim().length > 0) {
                    chat.server.SendMessage(content, useridmain, conversationidcurrent);
                    var currentdate = new Date();
                    var s = '<li class="msg-right">'
                        + '<div class="msg-left-sub">'
                        + '<img src="' + pathimage + '">'
                        + '<div class="msg-desc">'
                        + content
                        + '</div>'
                        + '<small>' + currentdate.toLocaleString('en-GB') + '</small>'
                        + '</div>'
                        + '</li >';
                    $('.right-chat .message.mCustomScrollbar ul').append(s);
                    $('#messagecontent').val("");
                    $('#messagecontent').focus();
                    scrollBottom();
                    resetConversation(conversationidcurrent, content, isGroup, pathimage, nameconversation, true);
                }
            }
        });

        $('.btn-send').click(function () {
            //var useridcurrent;
            //var usersendmessage;
            //if (isGroup == false) {
            //    useridcurrent = getIdUserFromSingleConversation(conversationidcurrent, useridmain);
            //    usersendmessage = setCurrentPathImage(useridcurrent);
            //} else {
            //    useridcurrent = getIdUserFromSingleConversation(conversationidcurrent, useridmain);
            //    usersendmessage = setCurrentPathImage(useridcurrent);
            //}


            var content = $('#messagecontent').val();
            if (conversationidcurrent != "" && content.trim().length > 0) {
                chat.server.SendMessage(content, useridmain, conversationidcurrent);
                var currentdate = new Date();
                var s = '<li class="msg-right">'
                    + '<div class="msg-left-sub">'
                    + '<img src="' + pathimage + '">'
                    + '<div class="msg-desc">'
                    + content
                    + '</div>'
                    + '<small>' + currentdate.toLocaleString('en-GB') + '</small>'
                    + '</div>'
                    + '</li >';
                $('.right-chat .message.mCustomScrollbar ul').append(s);
                $('#messagecontent').val("");
                $('#messagecontent').focus();
                scrollBottom();
                resetConversation(conversationidcurrent, content, isGroup, pathimage, nameconversation, true);
            }
        });
    });
    //Thuc hien sau khi ket noi hub thanh cong
});

function updateConnectionId(connectionid) {
    $.ajax(
        {
            url: "/MainChat/UpdateConnectionId",
            data: { connectionid: connectionid },
            dataType: "json",
            type: "post",
            success: function () {
            }
        });
}
function setCurrentPathImage(userid) {
    //var pathImage = "";
    var usersendmessage;
    $.ajax(
        {
            async: false,
            url: "/MainChat/GetInfoOneFriend",
            data: { userid: userid },
            dataType: "json",
            type: "post",
            success: function (result) {
                // pathImage = result.PathImage;
                usersendmessage = result;
            }
        });
    return usersendmessage;
}

function resetConversation(conversationid, contents, isGroup, pathImageSender, nameconversation, readstatus) {

    var lastmessage = "";
    if (contents.length > 20) {
        lastmessage = contents.substr(0, 10);
    } else {
        lastmessage = contents;
    }
    var element = $(".chatList[data-conversationid$=" + conversationid + "]").closest('li');
    element.remove();
    var image = "";
    if (isGroup == true) {
        image = '<img src="/Images/group.png" isgroup="true" />'
    } else {
        image = '<img src="' + pathImageSender + '" isgroup="false" />';
    }
    var currentdate = new Date();
    var readStatusHtml = "";
    if (readstatus == false) {
        readStatusHtml = '<small class="mess-unread">';

        setUnreadOnlineList(conversationid, contents, isGroup, pathImageSender, nameconversation);
    } else {
        readStatusHtml = '<small class="mess-readed">';
    }
    var s = "";
    s = '<li>'
        + '<div class="chatList" data-conversationid=' + conversationid + '>'
        + '<div class="img">'
        + image
        + '</div>'
        + '<div class="desc">'
        + '<small class="time">' + currentdate.toLocaleString('en-GB') + '</small >'
        + '<h5>' + nameconversation + '</h5>'
        + readStatusHtml + lastmessage + '</small>'
        + '</div>'
        + '</div>'
        + '</li>'


    $('.left-chat.mCustomScrollbar ul').prepend(s);
}
function setUnreadOnlineList(conversationid) {
    var element = $(".onlineitem[data-conversationid$=" + conversationid + "]").find('.tickread.readfriend');
    element.removeClass('readfriend');
}

function setUnreadOfflineList(conversationid) {
    var element = $(".onlineitem[data-conversationid$=" + conversationid + "]").find('.tickread');
    element.addClass('readfriend');
}
function setComversationUnread(conversationid) {
    $.ajax(
        {
            async: true,
            url: "/MainChat/SetComversationUnread",
            data: { conversationid: conversationid },
            dataType: "json",
            type: "post",
            success: function (result) {
                // pathImage = result.PathImage;
            }
        });
}
function setConversationReaded(conversationid) {
    $.ajax(
        {
            async: true,
            url: "/MainChat/SetComversationReaded",
            data: { conversationid: conversationid },
            dataType: "json",
            type: "post",
            success: function (result) {
                // pathImage = result.PathImage;
            }
        });
}
function setReadOnlineList(conversationid) {
    var element = $(".onlineitem[data-conversationid$=" + conversationid + "]").find('.tickread');
    element.addClass('readfriend');
    setConversationReaded(conversationid);
}
function setReadConversation(conversationid) {
    var element = $(".chatList[data-conversationid$=" + conversationid + "]").find('.desc small.mess-unread').removeClass('mess-unread').addClass('mess-readed');
    setConversationReaded(conversationid);
}

function getNameConversation(conversationid, userid) {
    var nameConversation = "";
    $.ajax(
        {
            async: false,
            url: "/MainChat/GetNameConversation",
            data: { conversationid: conversationid, userid: userid },
            dataType: "json",
            type: "post",
            success: function (result) {
                nameConversation = result;
            }
        });
    return nameConversation;
}
function getIdUserFromSingleConversation(conversationid, useridmain) {
    var userid = "";
    $.ajax(
        {
            async: false,
            url: "/MainChat/GetIdUserFromSingleConversation",
            data: { conversationid: conversationid, userid: useridmain },
            dataType: "json",
            type: "post",
            success: function (result) {
                userid = result;
            }
        });
    return userid;
}