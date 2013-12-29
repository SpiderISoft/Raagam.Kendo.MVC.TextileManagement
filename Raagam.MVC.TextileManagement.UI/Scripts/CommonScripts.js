disableControls = function (controlArrary) {

    $.each(controlArrary, function (key, control) {

        $(this).attr('readonly', 'readonly');

    });

}

enableControls = function (controlArrary) {

    $.each(controlArrary, function (key, control) {

        $(this).attr('disabled', false);

    });

}