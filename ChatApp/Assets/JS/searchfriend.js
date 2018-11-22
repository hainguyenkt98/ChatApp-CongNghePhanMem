$(document).ready(function () {

    $(".btn.btn-primary.add-friend").off('click').on('click', function () {
        var userid = $(this).data('userid');
        $.ajax({
            url: "/SearchFriend/AddFriend",
            data: { userid: userid },
            dataType: "json",
            type: "post",
            success: function () {

            }
        });
        $(this).removeClass('btn-primary');
        $(this).empty();
        $(this).text("Bạn bè");
        $(this).prop("disabled", true);
    });

    $('#btnsearch').off('click').on('click', function () {
        var keyword = $('#txtKeyword').val();
        $.ajax({
            url: "/SearchFriend/Finding",
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
                        + ' <span><img src="../Images/img-friend.png" alt=""></span>'
                        + '</div>'
                        + '<div class="col-sm-8 info-friend">'
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
                        + '<div class="col-sm-2 function-friend ">'
                        + '<button type="button" class="btn btn-primary add-friend " data-userid= ' + result[i].Id + '>Kết bạn</button>'
                        + '</div>'
                        + '</div>'
                        + '</li>'
                }
                $('.list-group').empty().html(s);
            }
        });
    });

});