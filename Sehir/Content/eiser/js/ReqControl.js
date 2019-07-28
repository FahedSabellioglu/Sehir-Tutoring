function CheckFields(element) {
    if (element.name == "C_Code" || element.name == "C_Name" || element.name == "title" || element.name == "dept") {

        if ($.trim(element.value) == '') {
            element.setCustomValidity("You can't leave the fields empty.");
        }
        else if ($.isNumeric(element.value) == true) {
            element.setCustomValidity(element.value + " can't be a number.");

        }
        else {
            element.setCustomValidity("");

        }
    }
}
