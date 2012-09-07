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
    else {
        $('#msg-warn-text').html('Unable detect your location. Please enable location service if your browser supports it');
        $('#msg-warn').removeClass("hidden");
        $('#msg-warn').alert();
    }
}

function setUserMarker(map, latLng) {
    var image = new google.maps.MarkerImage(
            '/Content/Images/me.png',
            new google.maps.Size(50, 50),
            new google.maps.Point(0, 0),
            new google.maps.Point(25, 50)
        );

    var shadow = new google.maps.MarkerImage(
            '/Content/Images/me-shadow.png',
            new google.maps.Size(78, 50),
            new google.maps.Point(0, 0),
            new google.maps.Point(25, 50)
        );
    
    var marker = new google.maps.Marker({
        position: latLng,
        animation: google.maps.Animation.DROP,
        map: map,
        icon: image,
        shadow: shadow,
        title: 'You are here'
    });
}