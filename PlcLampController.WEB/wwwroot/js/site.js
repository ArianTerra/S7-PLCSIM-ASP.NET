$('#s1').on('change', function () {
    var data = $('#s1').is(':checked');
    console.log('S1 ' + data);
    $.ajax({
        url: 'api/sim/write?byteIndex=0&bitIndex=0&data=' + Number(data),
        success: readLampStatus
    });
});

$('#s2').on('change', function () {
    var data = $('#s2').is(':checked');
    console.log('S2 ' + data);
    $.ajax({
        url: 'api/sim/write?byteIndex=0&bitIndex=1&data=' + Number(data),
        success: readLampStatus
    });
});

$('#s3').on('change', function () {
    var data = $('#s3').is(':checked');
    console.log('S3 ' + data);
    $.ajax({
        url: 'api/sim/write?byteIndex=0&bitIndex=2&data=' + Number(data),
        success: readLampStatus
    });
});

function readLampStatus() {
    $.ajax({
        url: 'api/sim/read?byteIndex=0&bitIndex=0',
        success: function (data) {
            console.log('Bulb is ' + data);
            if (data) {
                $('#bulb .filament').css('fill', 'orange');
                $('#bulb .cls-5').css('fill', '#FFF9A3');
            } else {
                $('#bulb .filament').css('fill', '');
                $('#bulb .cls-5').css('fill', '');
            }
        },
    });
}

function readSimStatus() {
    $.ajax({
        url: 'api/sim/status',
        success: function (data) {
            $('#status').html(data)
        }
    });
}

$(document).ready(function () {
    readLampStatus();
    readSimStatus();
});

// buttons
$("#connect").on("click", function() {
    $.ajax({
        url: "api/sim/connect"
    });

    readSimStatus();
});
