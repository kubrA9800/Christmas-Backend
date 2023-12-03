$(function () {

    $(document).on("click", ".image-delete button", function (e) {

        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `/admin/product/deleteproductimage?id=${id}`,
            type: "Post",
            success: function (res) {
                $(e.target).parent().remove();
            }
        })

    })



    $(document).on("click", ".imageblogs-delete button", function (e) {
        console.log("gvhbjn")
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `/admin/blog/deleteblogimage?id=${id}`,
            type: "Post",
            success: function (res) {
                $(e.target).parent().remove();
            }
        })

    })
})