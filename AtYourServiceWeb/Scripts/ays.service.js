
function AdvertGeoSerivce(map) {

    this.map = map;
    this.geocoder = new google.maps.Geocoder();
    this.marker = null;
    var advertGeoSerivce = this;

    google.maps.event.addListener(map, 'click', function (args) {
        advertGeoSerivce.lookupLocation(args.latLng);
    });
}

AdvertGeoSerivce.prototype.lookupLocation = function (latLng) {

    var advertGeoSerivce = this;

    this.geocoder.geocode({ 'latLng': latLng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {

            if (advertGeoSerivce.isValidLocation(results)) {
                advertGeoSerivce.moveMarker(latLng);
                if (results.length > 1) {
                    $('#location-text').html(' ' + results[1].formatted_address);
                } else {
                    $('#location-text').html(' ' + results[0].formatted_address);
                }

            } else {
                advertGeoSerivce.clearLocation();

                $('#modal-message-body').html('<p>Please select a location in Sri Lanka.</p>');
                $('#modal-message').modal('show');

                $('#location-text').html(' Select your location on the map by clicking on it');
            }

        } else {
            advertGeoSerivce.clearLocation();
            alert("Geocoder failed due to: " + status);
        }
    });
};

AdvertGeoSerivce.prototype.isValidLocation = function (results) {

    var valid = false;
    $.each(results[0].address_components, function (key, component) {
        if (component.short_name === 'LK' && component.types[0] == 'country') {
            valid = true;
        }

        return !valid;
    });

    return valid;
};

AdvertGeoSerivce.prototype.moveMarker = function (latLng) {

    if (this.marker != null) {
        this.marker.setPosition(latLng);
    }
    else {
        this.marker = new google.maps.Marker({
            position: latLng,
            animation: google.maps.Animation.DROP,
            map: map,
            title: 'You are here'
        });
    }

    $('#Latitude').val(latLng.lat());
    $('#Longitude').val(latLng.lng());
};

AdvertGeoSerivce.prototype.clearLocation = function () {

    $('#Latitude').val('');
    $('#Longitude').val('');
    $('#location-text').html(' Select your location on the map by clicking on it');
};