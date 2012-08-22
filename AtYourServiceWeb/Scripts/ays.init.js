$(document).ready(function () {
    $('.alert').alert();
    $('input, textarea').placeholder();

    var captured = $('#user-location').attr('data-captured');
    if (captured == 'no') {
        getUserLocation(function (loc) {
            $.ajax({
                type: 'POST',
                url: './Accounts/UpdateLocation/',
                data: { lat: loc.lat(), lng: loc.lng() }
            });
        });
    }
});

$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest(".control-group").addClass("error");
    },
    unhighlight: function (element) {
        $(element).closest(".control-group").removeClass("error");
    }
});