$(document).ready(function () {
    $(document).on('change', '.male', function () {
        if (this.checked) {
            $('.female').prop('checked', false);
        }
    });
    $(document).on('change', '.female', function () {
        if (this.checked) {
            $('.male').prop('checked', false);
        }
    });
});