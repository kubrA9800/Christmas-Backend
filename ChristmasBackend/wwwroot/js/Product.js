$(document).ready(function () {


    $(document).on("click", ".show-more button", function () {
        console.log("salam")

        let parent = $(".parent-elem");
        let skipCount = $(parent).children().length;
        console.log(skipCount);

        let productsCount = $(parent).attr("data-count")
        $.ajax({
            url: `home/loadmore?skipCount=${skipCount}`,
            type: "Get",
            success: function (res) {
                $(parent).append(res);


                skipCount = $(parent).children().length
                if (skipCount >= productsCount) {
                    $(".show-more button").addClass("d-none")
                    $(".show-less button").removeClass("d-none")
                }

            }
        })



    })



    $(document).on("click", ".show-less button", function () {


        let parent = $(".parent-elem");

        let skipCount = 0;


        $.ajax({
            url: `home/loadmore?skipCount=${skipCount}`,
            type: "Get",
            success: function (res) {
                parent.html("");
                $(parent).append(res);
                $(".show-more button").removeClass("d-none")
                $(".show-less button").addClass("d-none")
            }
        })


    })


})