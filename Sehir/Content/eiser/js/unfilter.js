$(".all").click(function (e) {
    e.preventDefault();
    $(".BasedOnName").removeClass("active")

    $(".single_product").each(function () {

        $(this).css("display", "block")
    })
})