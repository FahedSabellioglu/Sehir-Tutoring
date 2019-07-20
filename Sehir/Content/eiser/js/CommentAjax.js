$("#target").on("click", ".commentbutton", function () {
    var scripttag = document.getElementById("commentsubmit");

    var tid = scripttag.getAttribute("data-id");
    var sid = scripttag.getAttribute("data-usid");
    var code = scripttag.getAttribute("data-code");
    var name = scripttag.getAttribute("data-name");
    var controllerName = scripttag.getAttribute("data-controller");
    var reviewtext = document.getElementById("comment").value.trim();

    if (reviewtext == "") { alert("Please Fill the area first"); return false };

    if (controllerName == "Homework") {
        $.ajax({
            type: "POST",
            url: "/Homework/Comment",
            data: { review: reviewtext, H_ID: tid, C_Code: code, title: name,S_ID:sid },
            success:
                function (response) {
                    $("#target").html(response);
                    var myurl = '@Html.Raw(@Url.Action("getFeedbacks", new { T_ID = Model.T_ID, title = Model.title, C_Code = Model.C_Code }))';
                    var reviews = $(".target");
                    reviews.load(myurl);
                },
            error: function () { alert("Faild") },
            cache: false
        })
    }
    else if (controllerName == "Courses") {
        $.ajax({
            type: "POST",
            url: "/Courses/Comment",
            data: { review: reviewtext, T_ID: tid, C_Code: code, S_ID:sid },
            success:
                function (response) {
                    $("#target").html(response);
                    var myurl = '@Html.Raw(@Url.Action("getFeedbacks", "Courses", new { S_ID = Model.T_ID, C_Code = Model.C_Code, T_ID=Model.H_ID }))';
                    var reviews = $(".target");
                    reviews.load(myurl);
                },
            error: function () { alert("Faild") },
            cache: false
        })
    }

});