$(document).ready(function () {
    $(document).on("click", ".add-item .group-item .btn-delete", function () {
        var strHTML = '';
        var element = $(this).closest('.group-item');
        var pathimg = element.find('.image img').attr('src');
        var name = element.find('.name').text();
        var id = element.attr("data-userid");
        strHTML += '<div class="group-item" data-userid="' + id + '">'
            + '<div class="image"><img src="' + pathimg + '" alt=""></div>'
            + '<div class="name">' + name + '</div>'
            + '<div class="btn-add"><span><i class="fas fa-plus"></i></span></div>'
            + '</div>'
        $('.flex-container.friend-list').append(strHTML);
        element.remove();
    });

    $(document).on("click", ".friend-list .group-item .btn-add", function () {
        var strHTML = '';
        var element = $(this).closest('.group-item');
        var pathimg = element.find('.image img').attr('src');
        var name = element.find('.name').text();
        var id = element.attr("data-userid");
        strHTML += '<div class="group-item" data-userid="' + id + '">'
            + '<div class="image"><img src="' + pathimg + '" alt=""></div>'
            + '<div class="name">' + name + '</div>'
            + '<div class="btn-delete"><span><i class="fas fa-times"></i></span></div>'
            + '</div>'
        $('.flex-container.add-item').append(strHTML);
        element.remove();
    });

    $(document).on("click", ".btn-save", function () {
        var list = document.getElementsByClassName('add-item')[0];
        var n = list.childElementCount;
        var conversationname = document.getElementById('conversation-name').value;
        if (conversationname == '' || n == 0) {
            $('#warningmodal').modal('show');
            return;
        }
        var listUserid = [];
        //Validate sau
        var list = document.getElementsByClassName('add-item')[0];
        var i = 0;
        for (i = 0; i < list.childElementCount; i++) {
            var element = list.getElementsByClassName('group-item')[i];
            listUserid.push(element.getAttribute('data-userid'));
        }
        createGroupChat(listUserid, conversationname);
        $('#addnewgroup-modal').modal('hide');
        location.reload();
    });
    $(document).on("click", ".btn-addnewgroup", function () {
        var strHTML = '';
        var list = document.getElementsByClassName('add-item')[0];
        var i = 0;
        var n = list.childElementCount;
        for (i = 0; i < n; i++) {
            strHTML = '';
            var element = list.getElementsByClassName('group-item')[i];
            var pathimg = $(element).find('.image img').attr('src');
            var name = $(element).find('.name').text();
            var id = $(element).attr("data-userid");
            strHTML += '<div class="group-item" data-userid="' + id + '">'
                + '<div class="image"><img src="' + pathimg + '" alt=""></div>'
                + '<div class="name">' + name + '</div>'
                + '<div class="btn-add"><span><i class="fas fa-plus"></i></span></div>'
                + '</div>'
            $('.flex-container.friend-list').append(strHTML);
            $(element).remove();
            i--;
            n--;
        }
    });
    
    $(document).on("click", ".btn-leavegroup", function () {
        var conversationid = $(this).closest('.function-friend').data('conversationid');
        leavGroupChat(conversationid);
        location.reload();
    });

    
    $(document).on("click", "#btnsearch", function () {
        var keyword = $('#txtKeyword').val();
        $.ajax({
            url: "/ManageGroupChat/Finding",
            data: { keyword: keyword },
            dataType: "json",
            type: "post",
            success: function (result) {
                var s = '';
                for (var i = 0; i < result.length; i++) {
                    var about = "";
                    for (var j = 0; j < result[i].ListUserConversation.length; j++) {
                        about += result[i].ListUserConversation[j].Nameshow;
                        about += ' ,';
                        if (j == 4) {
                            about += '...';
                            break;
                        }
                    }
                    s=s+'<li class="list-group-item list-group-item-light">'
                        +'<div class="row">'
                           +' <div class="col-sm-2 image-friend">'
                               +' <span><img src="/Images/group.png" isGroup="true" /></span>'
                            +'</div>'
                            +'<div class="col-sm-8 info-friend">'
                                +'<div class="row name-group">'
                                    +'<div class="col-sm-12">'
                                        +'<b>'+result[i].Name+'</b>'
                                    +'</div>'
                                +'</div>'
                                +'<div class="row about-group">'
                                    +'<div class="col-sm-12">'
                                         +'<i>'+about+'</i>'
                                    +'</div>'
                                +'</div>'
                        + '</div>'
                        + '<div class="col-sm-2 function-friend" data-conversationid="' + result[i].Conversationid + '">'
                                +'<a href="#" class="btn btn-secondary btn-sm btn-leavegroup">Rời nhóm</a>'
                            +'</div>'
                        +'</div>'
                   +'</li>'
                }
                $('.list-group').empty().html(s);
            }
        });

    });
});



//function
function createGroupChat(listUserid, groupName) {
    $.ajax(
        {
            async: false,
            url: "/ManageGroupChat/CreateGroupChat",
            data: { listUserid: listUserid, groupName: groupName },
            dataType: "json",
            type: "post",
            success: function () {
            }
        });
}
function leavGroupChat(conversationid) {
    $.ajax(
        {
            async: false,
            url: "/ManageGroupChat/LeaveConversation",
            data: { conversationid: conversationid },
            dataType: "json",
            type: "post",
            success: function () {
                location.reload();
            }
        });
}