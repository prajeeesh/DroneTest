//if ($("#btnExecute").length > 0) {
   /* $("#droneInput").onsubmit(function () {
        var btn = $(this);
        var droneoutput = $('#droneoutput');
        var DroneInputModel = $('#DroneInputModel').val();
        $("span").text("");
        if (validateInput()) { 
            //console.log(btn.data('submiturl'));
            console.log('move to Ajax posting');
        //$.ajax({
        //    type: 'POST',
        //    url: btn.data('submiturl'),
        //    data: JSON.stringify({ droneInputModel: DroneInputModel }),
        //    contentType: 'application/json',
        //    success: function (data) {
        //        droneoutput.html(data);
        //        console.log(data);
        //    }
        //});
        }
        return false;
    });*/
//}
var validateInput = function () {

    var DroneInputModel = $('#DroneInputModel').val();
    var errorMessage = "";
    $("span").text("");
    if (DroneInputModel.length > 0) {
       var inputArray = DroneInputModel.split(/\n|\r/);
        if (inputArray.length > 2) {
            var coordinates = inputArray[0];

            if (! ValidateCoordinates(coordinates)){
                $("span").text("Please enter a valid coordinate for the battlefield").show();
                return false;
            }

            var droneCount = 1;
            for (var i = 1; i < inputArray.length; i += 2) {
                var position = inputArray[i];
                var instruction = inputArray[i + 1];

                if (!ValidatePosition(position)) {
                    $("span").text("Please enter a valid position for the Drone " + droneCount).show();
                    return false;
                }
                //Validate the instruction 
                if (!ValidateInstruction(instruction)) {
                    $("span").text("Entered instruction is not valid").show();
                    return false;
                }
                droneCount++;
            }
       }
        else {
            $("span").text("Please enter all the requrired details").show();
            return false;
        }
    }
    else {
        $("span").text("Please enter all the requrired details").show();
        return false;
    }
    return true;
}
var ValidateInstruction = function (instruction) {
    var exp = /^[LRM]*$/;
    instruction.match(exp)
    if (!instruction.match(exp)) {
        return false;
    }
    else {
        return true;
    }
}
var ValidatePosition = function (position) {
    var exp = /^\d \d [NSEW]$/;
    position.match(exp)
    if (!position.match(exp)) {
        return false;
    }
    else {
        return true;
    }
}
var ValidateCoordinates = function (position) {
    var exp = /^\d?\d \d?\d$/;
    position.match(exp)
    if (!position.match(exp)) {
        return false;
    }
    else {
        return true;
    }
}