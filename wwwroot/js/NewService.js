jQuery(document).ready(function ($) {
    $('#ThisUser').change(function () {
        let personId = $(this).val();
        $.get(`/user/cars/${personId}`, response => {
            //console.log(response);
            for (car of response) {

                //console.log(car)
                //console.log(car.make)
                $("#CarDropDown").append(`<option value="${car.carId}">${car.year} ${car.make} ${car.model}</option>`)
            }
        })
    })
});