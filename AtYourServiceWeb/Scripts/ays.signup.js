
function SignUpGeoSerivce(map) {
    
    this.map = map;
    this.geocoder = new google.maps.Geocoder();
    this.marker = null;
    var signUpService = this;

    google.maps.event.addListener(map, 'click', function (args) {
        signUpService.lookupLocation(args.latLng);
    });
}

SignUpGeoSerivce.prototype.lookupLocation = function (latLng) {

    var signUpService = this;

    this.geocoder.geocode({ 'latLng': latLng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {

            if (signUpService.isValidLocation(results)) {
                signUpService.moveMarker(latLng);
                if (results.length > 1) {
                    $('#location-text').html(' ' + results[1].formatted_address);
                } else {
                    $('#location-text').html(' ' + results[0].formatted_address);
                }

            } else {
                signUpService.clearLocation();

                $('#modal-message-body').html('<p>Please select a location in Sri Lanka.</p>');
                $('#modal-message').modal('show');

                $('#location-text').html(' Select your location on the map by clicking on it');
            }

        } else {
            signUpService.clearLocation();
            alert("Geocoder failed due to: " + status);
        }
    });
};

SignUpGeoSerivce.prototype.isValidLocation = function (results) {

    var valid = false;
    $.each(results[0].address_components, function (key, component) {
        if (component.short_name === 'LK' && component.types[0] == 'country') {
            valid = true;
        }

        return !valid;
    });

    return valid;
};

SignUpGeoSerivce.prototype.moveMarker = function (latLng) {
    
    if (this.marker != null) {
        this.marker.setPosition(latLng);
    }
    else {
        var image = new google.maps.MarkerImage(
            '../Content/Images/me.png',
            new google.maps.Size(50, 50),
            new google.maps.Point(0, 0),
            new google.maps.Point(25, 50)
        );

        var shadow = new google.maps.MarkerImage(
            '../Content/Images/me-shadow.png',
            new google.maps.Size(78, 50),
            new google.maps.Point(0, 0),
            new google.maps.Point(25, 50)
        );        
        this.marker = new google.maps.Marker({
            position: latLng,
            animation: google.maps.Animation.DROP,
            map: map,
            icon: image,
            shadow: shadow,
            title: 'You are here'
        });
    }

    $('#Latitude').val(latLng.lat());
    $('#Longitude').val(latLng.lng());
};

SignUpGeoSerivce.prototype.clearLocation = function () {
    
    $('#Latitude').val('');
    $('#Longitude').val('');
    $('#location-text').html(' Select your location on the map by clicking on it');
};