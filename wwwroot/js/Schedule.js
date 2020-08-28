$(document).ready(function () {
    $(".schedule").click(function () {
        $("#service").val($(this).val()).trigger('change');
    });

    // $(document).on("click", "#active", function (e) {
    //     e.preventDefault();
    //     $.ajax({
    //         url: "/service/inactivate/"
    //     })
    //     $.get("/service/inactivate", response => {
    //         PopulatePage(response);
    //     })
    // })
});