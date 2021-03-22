$(function () {
    $("time").each(function (i, e) {
        const dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("llll"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});

//To do Delete
//const $geolocateButton = document.getElementById('geolocation-button');
//$geolocateButton.addEventListener('click', geolocate);


//function geolocate() {
//    if (window.navigator && window.navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(onGeolocateSuccess, onGeolocateError);
//    }
//}

function getWeatherData() {
    $.getJSON("/Weather/GetData", function (json) {
        var card = document.getElementById("weatherPanel");
        /*$("#heroAdd").hide();*/
        $("#weatherPanel").show();
        console.log(json);

        var iconcode = json.weather[0].icon;
        var iconurl = "http://openweathermap.org/img/w/" + iconcode + ".png";
        $('#wicon').attr('src', iconurl);
        $('#cityName').text(json.city);
        $('#description').text(json.weather[0].description);
        $('#temp').text(`Temp: ${json.main.temp} C`);
        $('#wind').text(`Wind: ${json.wind.speed} m/s`);
        $('#pressure').text(`Pressure: ${json.main.pressure} Pa`);
        $('#humidity').text(`Humidity: ${json.main.humidity} %`);
        console.log(json);
    })
}

//function onGeolocateSuccess(coordinates) {
//    const { latitude, longitude } = coordinates.coords;
//    //console.log('Found coordinates: ', latitude, longitude);
//    $.getJSON("/Weather/GetData", { latitude, longitude }, function (json) {
//        var card = document.getElementById("weatherPanel");
//        /*$("#heroAdd").hide();*/
//        $("#weatherPanel").show();

//        var iconcode = json.weather[0].icon;
//        var iconurl = "http://openweathermap.org/img/w/" + iconcode + ".png";
//        $('#wicon').attr('src', iconurl);
//        $('#cityName').text(json.city);
//        $('#description').text(json.weather[0].description);
//        $('#temp').text(`Temp: ${json.main.temp} C`);
//        $('#wind').text(`Wind: ${json.wind.speed} m/s`);
//        $('#pressure').text(`Pressure: ${json.main.pressure} Pa`);
//        $('#humidity').text(`Humidity: ${json.main.humidity} %`);
//        console.log(json);
//    })
//}

//function onGeolocateError(error) {
//    console.warn(error.code, error.message);

//    if (error.code === 1) {
//        console.log("no");
//        // they said no
//    } else if (error.code === 2) {
//        console.log("un");
//        // position unavailable
//    } else if (error.code === 3) {
//        console.log("time");
//        // timeout
//    }
//}