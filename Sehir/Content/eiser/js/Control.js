function IdCheck(element) {

    if (element.name == "id") {
        element.setCustomValidity(element.value.length < 9 ? 'Please give a valid id' : '')
    }
    else if (element.name == "password2" || element.name == "pass") {

        if (element.value.length < 8) {
            element.setCustomValidity('The password has to be more than 8 characters');

        }
        else if (document.getElementById('ps2').value != document.getElementById('ps1').value) {

            element.setCustomValidity('Passwords do not match');
        }
        else {
            element.setCustomValidity('');
        }

    }
    else if (element.name == "U_name" || element.name == "U_Surname" | element.name == "dept") {
        if ($.trim(element.value) == '') {
            element.setCustomValidity('You cant leave this field empty.');
        }
        else if ($.isNumeric(element.value) == true) {
            element.setCustomValidity(element.placeholder + " can't be a number");

        }
        else {
            element.setCustomValidity('');
        }
    }

}