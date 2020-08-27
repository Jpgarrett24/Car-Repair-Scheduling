/*$.ajaxSetup({
    headers: {
        "content-type": "application/json",
        "authorization": "Basic OWYxOTQ2ZGYtYmZiNy00ZmNmLTkyNTgtNmIwOWNhNjc0Njdi",
        "partner-token": "ac11a56f425c4fff8056611b4d000bcc"
    }
});
let year = @Model.Car.Year;
let make = "@Model.Car.Make.ToUpper()";
let model = "@Model.Car.Model.ToUpper()";
let mileage = @Model.Car.Mileage;
$.ajax({
    url: `https://cors-anywhere.herokuapp.com/http://api.carmd.com/v3.0/engine?year=@Model.Car.Year&make=@Model.Car.Make.ToUpper()&model=@Model.Car.Model.ToUpper()"`,
    type: 'GET',
    dataType: 'json', // added data type
    success: function(res) {
        console.log(res);
    }
});
$.ajax({
    url: `https://cors-anywhere.herokuapp.com/http://api.carmd.com/v3.0/upcoming?year=@Model.Car.Year&make=@Model.Car.Make.ToUpper()&model=@Model.Car.Model.ToUpper()"&mileage=@Model.Car.Mileage`,
    type: 'GET',
    dataType: 'json', // added data type
    success: function(res) {
        console.log(res);
    }
});*/
let results = undefined;
$.ajax({
    url: "https://cors-anywhere.herokuapp.com/@Model.CarDetails",
    type: 'GET',
    dataType: 'json', // added data type
    success: function (res) {
        console.log(res);
        results = res.Results;
        if ("@Model.Car.Model" != "Motorcycle") {
            $(".data").append(`
                <p><strong>${res.Results[14].Variable}:</strong> ${res.Results[14].Value}</p>
                <p><strong>Vehicle Class:</strong> ${res.Results[23].Value}</p>
                <p><strong>Vehicle Weight:</strong> ${res.Results[28].Value}</p>
                <p><strong>${res.Results[24].Variable}:</strong> ${res.Results[24].Value}</p>
                <p><strong>${res.Results[70].Variable}:</strong> ${res.Results[70].Value}</p>
                <p><strong>${res.Results[71].Variable}:</strong> ${res.Results[71].Value}</p>
                <p><strong>Horesepower:</strong> ${res.Results[82].Value}</p>
                <p><strong>Mileage:</strong> @Model.Car.Mileage</p>
                `)
        }
        else {
            $(".data").append(`
                <p><strong>${res.Results[14].Variable}:</strong> ${res.Results[14].Value}</p>
                <p><strong>Vehicle Class:</strong> ${res.Results[23].Value}</p>
                <p><strong>Vehicle Weight:</strong> ${res.Results[28].Value}</p>
                <p><strong>${res.Results[70].Variable}:</strong> ${res.Results[70].Value}</p>
                <p><strong>${res.Results[71].Variable}:</strong> ${res.Results[71].Value}</p>
                <p><strong>Horesepower:</strong> ${res.Results[82].Value}</p>
                <p><strong>Mileage:</strong> @Model.Car.Mileage</p>
                `)
        }
    }
});
$.ajax({
    url: "https://cors-anywhere.herokuapp.com/https://one.nhtsa.gov/webapi/api/Recalls/vehicle/modelyear/@Model.Car.Year/make/@Model.Car.Make/model/@Model.Car.Model?format=json",
    type: 'GET',
    dataType: 'json', // added data type
    success: function (res) {
        console.log(res);
        res.Results.forEach((item) => {
            $(".recall").append(`
                        <ul><h3>${item.Component}</h3>
                            <li><strong>Risk: </strong>${item.Conequence}</li>
                            <li><strong>Fix: </strong>${item.Remedy}</li>
                        </ul>
                    `)
        })
    }
});