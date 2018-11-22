$(document).ready(function () {
    $("#datepicker").datepicker({
        autoclose: true,
        todayHighlight: true
    }).datepicker('update');


    $('.btn-cancel').click(function () {
        location.reload();
    });
    $('.btn-save').click(function () {
        var pathImage = $('#picture').val();
        var nameshow = $('#nameshow').val();
        var email = $('#email').val();
        var birthdate = $('#birthdate').val();
        var password = $('#password').val();

        if (nameshow.length < 1 || password.length < 1) {
            alert('Thông tin không hợp lệ !')
        } else {
            $.ajax(
                {
                    url: "/Profile/SaveChange",
                    data: { pathImage: pathImage, nameshow: nameshow, email: email, birthdate: birthdate, password: password },
                    dataType: "json",
                    type: "post",
                    success: function () {
                        location.reload();
                    }
                });
        }

    });
    $('#btnUpload').click(function () {
        $('#fileUpload').trigger('click');
    });
    $('#fileUpload').change(function () {
        if (window.FormData !== undefined) {
            var fileUpload = $('#fileUpload').get(0);
            var files = fileUpload.files;

            var formData = new FormData();
            formData.append('file', files[0]);

            $.ajax(
                {
                    url: "/Profile/ProcessUpload",
                    contentType: false,
                    processData: false,
                    data: formData,
                    type: "post",
                    success: function (urlImage) {
                        if (urlImage.length == 0) {
                            return;
                        }
                        $('#pictureUpload').attr('src', urlImage);
                        $('#picture').val(urlImage);
                    },
                    error: function (err) {
                        alert('Có lỗi khi Upload ảnh !')
                    }
                }
            );
        }
    });
});