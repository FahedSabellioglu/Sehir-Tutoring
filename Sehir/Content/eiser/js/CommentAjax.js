$("#target").on("click", ".commentbutton", function () {
    var scripttag = document.getElementById("commentsubmit");

    var id = scripttag.getAttribute("data-id");
    var code = scripttag.getAttribute("data-code");
    var name = scripttag.getAttribute("data-name");
    var reviewtext = document.getElementById("comment").value.trim();
    if (reviewtext== "") { alert("Please Fill the area first"); return false };
    $.ajax({
        type: "POST",
        url: "/Courses/Comment",
        data: { review: reviewtext, T_ID: id, C_Code: code, C_Name: name },
        success:
            function (response) {
                $("#target").html(response);
                var myurl = '@Html.Raw(@Url.Action("getFeedbacks",  new { ID = Model.ID, C_Code = Model.C_Code, C_Name = Model.C_Name }))';
                var reviews = $(".target");
                reviews.load(myurl);
            },
        error: function () { alert("Faild") },
        cache: false
    })
});