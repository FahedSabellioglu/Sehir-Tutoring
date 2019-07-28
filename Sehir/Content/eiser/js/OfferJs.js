// add course group
var checkbox = document.getElementById('checkbox');
var Code = document.getElementById('new-course');
var codeelement = document.getElementById("newcode");
var nameelement = document.getElementById("newname");
var c_code = document.getElementById("c_code");
var c_name = document.getElementById("c_name");
var dept = document.getElementById("dept");


var ShiftAttr = function (element_1, element_2, value) {
    element_1.setAttribute("name", value);
    element_2.removeAttribute("name");

    element_1.setAttribute("required", true);
    element_2.setAttribute("required", false);

    element_2.setAttribute("disabled", "disabled");
    element_1.removeAttribute("disabled");

}

var newtitle = document.getElementById("title");

var deptHandler = function (value) {
    if (value == true) {
        console.log('on');
        dept.setAttribute("name", "dept");
        dept.setAttribute("required", true);
    }
    else {
        console.log('off');
        dept.removeAttribute("name");
        dept.setAttribute("required", false);
    }

}



if (document.getElementById("h_checkbox")) {
    //homework group
    var H_checkbox = document.getElementById("h_checkbox");
    var Homework = document.getElementById("new-homework");
    var hTitle = document.getElementById("hTitle");
    var newhomworkelment = document.getElementById("HomeworkTitle");


    var showhomeworktitle = function () {
        if (H_checkbox.checked) {
            Homework.style['display'] = 'block';
            ShiftAttr(newhomworkelment, hTitle, "title");

        }
        else {
            Homework.style['display'] = 'none';
            ShiftAttr(hTitle, newhomworkelment, "title")
        }
    }
    H_checkbox.onclick = showhomeworktitle;
    showhomeworktitle();
}

var showHiddenDiv = function () {
    if (checkbox.checked) {
        Code.style['display'] = 'block';
        ShiftAttr(codeelement, c_code, "C_Code");
        ShiftAttr(nameelement, c_name, "C_Name");
        if ('@ControllerName' == "Homework") {

            ShiftAttr(newtitle, hTitle, "title");


        }

        dept.setAttribute("name", "dept");
        dept.setAttribute("required", true);

    } else {
        Code.style['display'] = 'none';
        ShiftAttr(c_code, codeelement, "C_Code");
        ShiftAttr(c_name, nameelement, "C_Name");
        if ('@ControllerName' == "Homework") {

            ShiftAttr(hTitle, newtitle, "title");
        }
        dept.removeAttribute("name");
        dept.removeAttribute("required")


    }

}

checkbox.onclick = showHiddenDiv;
showHiddenDiv();
