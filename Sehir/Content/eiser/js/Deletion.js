$(".delete_item").click(function () {
    var Name = $(this).data("name");
    var Code = $(this).data("code");
    var usid = $(this).data("usid");

    $("#YES").data("name", Name);
    $("#YES").data("code", Code);
    $("#YES").data("usid", usid);
    document.getElementById("popup").innerHTML = "Would you like to delete " + Name;
    document.getElementById("no1").innerHTML = "No";
})