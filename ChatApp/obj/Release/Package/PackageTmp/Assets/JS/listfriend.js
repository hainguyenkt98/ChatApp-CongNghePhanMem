$(document).ready(function () {

    $(".btn.btn-secondary.remove-friend").off('click').on('click', function () {
        var userid = $(this).data('userid');
        $.ajax({
            url: "/ListFriend/UnFriend",
            data: { userID: userid },
            dataType: "json",
            type: "post",
            success: function () {

            }
        });
        $(this).removeClass('btn-secondary');
        $(this).empty();
        $(this).text("Đã hủy");
        $(this).prop("disabled", true);
    });

    $(".btn.btn-danger.block-friend").off('click').on('click', function () {
        var userid = $(this).data('userid');
        $.ajax({
            url: "/ListFriend/BlockFriend",
            data: { userID: userid },
            dataType: "json",
            type: "post",
            success: function () {

            }
        });
        $(this).removeClass('btn-danger');
        $(this).empty();
        $(this).text("Đã chặn");
        $(this).prop("disabled", true);
    });

    $('#btnsearch').off('click').on('click', function () {
        var keyword = $('#txtKeyword').val();
        $.ajax({
            url: "/ListFriend/Finding",
            data: { keyword: keyword },
            dataType: "json",
            type: "post",
            success: function (result) {
                $('.list-group').empty();
                var s = '';
                for (var i = 0; i < result.length; i++) {
                    var parsedDate = new Date(parseInt(result[i].Birth.substr(6)));
                    var jsDate = new Date(parsedDate);
                    var dateNow = new Date();
                    var age = dateNow.getFullYear() - jsDate.getFullYear();
                    s = s + '<li class="list-group-item list-group-item-light">'
                        + '<div class="row">'
                        + '<div class="col-sm-2 image-friend">'
                        + '<span><img src="../Images/img-friend.png" alt=""></span>'
                        + '</div>'
                        + '<div class="col-sm-7 info-friend">'
                        + '<div class="row name-friend">'
                        + '<div class="col-sm-12">'
                        + '<b>' + result[i].Nameshow + '</b>'
                        + '</div>'
                        + '</div>'
                        + '<div class="row about-friend">'
                        + '<div class="col-sm-12">'
                        + '<i>' + result[i].Sex + ', </i>'
                        + '<i>' + age + ' tuổi</i>'
                        + '</div>'
                        + '</div>'
                        + '</div>'
                        + '<div class="col-sm-3 function-friend">'
                        + '<button style="margin-right: 4px; type="button" class="btn btn-secondary remove-friend" data-userid=' + result[i].Id+ '>Hủy bạn</button>'
                        + '<button style="margin-right: 4px; type="button" class="btn btn-danger block-friend" data-userid=' + result[i].Id + '>Chặn bạn</button>'
                        + '</div>'
                        + '</div>'
                        + '</li >'
                    
                }
                $('.list-group').empty().html(s);
            }
        });
    });

});