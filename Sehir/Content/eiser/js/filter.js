$(".namefilter").click(function (e) {
    e.preventDefault();
    $(".BasedOnName").removeClass("active")
    $(this.parentNode).addClass("active")
    var name = this.innerHTML


    $(".single_product").each(function () {
        var datavalue = $(this).data("name")
        if (datavalue != name) {
            $(this).css("display", "none")
        }
        else if (datavalue == name) {
            $(this).css("display", "block")
        }
    })
})