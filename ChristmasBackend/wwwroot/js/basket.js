$(document).ready(function () {


    $(document).on("click", ".product .add-to-cart a", function (e) {
        e.preventDefault();
        let id= parseInt($(this).closest(".product").attr("data-id"))
        console.log(id)

        
        let basketCount = $(".basket-count").text();
        $.ajax({

            url: `home/addtocart?id=${id}`,
            type: "Post",
            success: function (res) {

                basketCount++;
                $(".basket-count").text(basketCount)
            }
        })






    })


    $(document).on("click", ".table-responsive .table-striped button", function (e) {
        e.preventDefault();

        let id = parseInt($(this).attr("data-id"))

        $.ajax({

            url: `cart/DeleteProductFromBasket?id=${id}`,
            type: "Post",
            success: function (res) {
                console.log(res.count)

                $(".header-top .basket-count").text(res.count);
                $(e.target).closest("tr").remove();
                $(".grand-total h1 span").text(res.grandTotal);

                if (res.count === 0) {
                    $(".alert-basket-empty").removeClass("d-none");
                    $(".basket-table").addClass("d-none");
                }

            }
        })
    })



    $(document).on("click", ".basket-table .fa-plus", function (e) {
        let id = parseInt($(this).attr("data-id"))
        let basketCount = $("header .rounded-circle").text();
        $.ajax({

            url: `cart/productcountplus?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).prev().text(res.count)
                $(".grand-total h1 span").text(res.basketGrandTotal);
                $(e.target).parent().next().children().text(res.productGrandTotal);
                basketCount++;
                $("header .rounded-circle").text(basketCount)
            }
        })

    })



    $(document).on("click", ".basket-table .fa-minus", function (e) {
        let id = parseInt($(this).attr("data-id"))
        let basketCount = $("header .rounded-circle").text();

        $.ajax({

            url: `cart/productcountminus?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).next().text(res.count)

                $(".grand-total h1 span").text(res.basketGrandTotal)

                $(e.target).parent().next().children().text(res.productGrandTotal);
                if (basketCount > 1) {
                    basketCount--;
                }

                $("header .rounded-circle").text(basketCount);

            }
        })

    })

})