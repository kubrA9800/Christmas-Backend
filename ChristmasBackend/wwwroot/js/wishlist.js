$(document).ready(function () {


    $(document).on("click", ".wishlist-add", function () {

        var id = $(this).parent().parent().data('id')

        let data = { id: id };
        let count = (".wishlist-count");


        $.ajax({
            method: 'POST',
            url: "/Shop/AddToWishlist",
            data: {
                id: id
            },
            success: function (res) {

                $(count).text(res);
            }
        })
        return false;

    })