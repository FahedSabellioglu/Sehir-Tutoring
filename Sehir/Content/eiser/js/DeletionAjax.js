$(".Yes-button").click(function () {
    var Code = $(this).data("code");
    var Name = $(this).data("name");
    var usid = $(this).data("usid");
    var currenturl = window.location.href[window.location.href.length - 1] == '1' ? '/1' : '';
    var Linkto = $("#d_ajax").data("to");
    var ApiLink = $("#d_ajax").data("link");
    console.log(Name);
    $.ajax({
        type: "POST",
        url: ApiLink,
        data: { userId: usid, name: Name, code: Code },
        success: function () {
            $("#exampleModal").modal("hide");
            window.location.replace(Linkto + currenturl);
        },
        error: function () {
            $("#exampleModal").modal("hide");
            document.getElementById("popup").innerHTML = "Couldn't delete the item " + Name;
        document.getElementById("no1").innerHTML = "Close";
        var clone = $("#exampleModal").clone();
        clone.find(".Yes-button").remove();
        clone.modal("show");}

})
})

