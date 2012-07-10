//taken from http://stackoverflow.com/a/499158/60108
//caret position of a textbox
function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.focus();
        input.setSelectionRange(selectionStart, selectionEnd);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd('character', selectionEnd);
        range.moveStart('character', selectionStart);
        range.select();
    }
}

function setCaretToPos(input, pos) {
    setSelectionRange(input, pos, pos);
}

function formatInfoContent(service) {
    var contentString = '<div id="content">' +
    '<h3>' + service.Title + '</h3>' +
    '<p>' + service.Body + '</p>' +
    '</div>';

    return contentString;
}