function getUserLocation(callBack) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                                             position.coords.longitude);

            callBack(pos);
        }, function () {
            handleLocationResolutionFail(callBack);
        });
    } else {
        handleLocationResolutionFail(callBack);
    }
}

function handleLocationResolutionFail(callback) {
    var location = google.loader.ClientLocation;
    
    if (location) {
        callback(new google.maps.LatLng(location.latitude, location.longitude));
    }
}