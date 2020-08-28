$(document).ready(function () {
    $(".schedule").click(function () {
        $("#service").val($(this).val()).trigger('change');
    });
});