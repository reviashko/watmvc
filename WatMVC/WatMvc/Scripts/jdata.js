(function (product, $, undefined) {

    product.OnDocumentReady = function () {
        $(document).ready(function () {

            $("#add_btn").click(function () {
                basket.AddToBasket($(this).attr("rel"), 1);
                return false;
            });
        });
    }

}(window.product = window.product || {}, jQuery));



(function (catalog, $, undefined) {

    catalog.OnDocumentReady = function () {
        $(document).ready(function () {

            LoadTest();
            InitLazyLoad();
            $("#content IMG.lazy").click(function () { window.location.href = $(this).attr("rel"); });
           
            $(".lmenu A").click(function () {
                catalog.Open($(this).attr("id"));
                return false;
            });

        });
    }

    catalog.Open = function (menu_id) {

        $.ajax({
            type: 'POST',
            data: { menu_id: menu_id },
            url: '/ajax/GetCatalogDataById',
            success: function (res) {
                var items = res.itms;
                var menu = res.mnus;
                var template = $.templates("#catalogTemplate");
                var htmlOutput = template.render(items);
                $("#content").html(htmlOutput);
                InitLazyLoad();
                $("#content IMG.lazy").click(function () { window.location.href = menu.Item_url + $(this).attr("rel"); });
            },
            error: function (res) {
                $("#content").text("error on load ((");
            }
        });
    }

    function LoadTest() {
        $("#tester1").load("/home/TestDetails", function (response, status, xhr) {
            if (status != "success") {
                $("#tester1").html('an error has occured');
            }
        });
    }

    function InitLazyLoad() {
        $("#content IMG.lazy").lazyload();
    }

}(window.catalog = window.catalog || {}, jQuery));


(function (basket, $, undefined) {

    basket.OnDocumentReady = function () {
        $(document).ready(function () {
            InitLazyLoad();
        });
    }

    basket.Open = function (menu_id) {

        $.ajax({
            type: 'POST',
            data: { menu_id: menu_id },
            url: '/basket/Get',
            success: function (res) {
                var template = $.templates("#basketTemplate");
                var htmlOutput = template.render(res.bit);
                $("#content").html(htmlOutput);
                InitLazyLoad();
            },
            error: function (res) {
                $("#content").text("error on load");
            }
        });
    }

    basket.AddToBasket = function (articul, qnt) {

        $.ajax({
            url: '/basket/Add',
            type: 'POST',
            data: { goods_id: articul, cnt: qnt },
            success: function (result) {
                alert(result.name);
            },
            error: function () {
                alert("error");
            }
        });
    }

    basket.RemoveFromBasket = function (basket_id) {

        $.ajax({
            url: '/basket/Remove',
            type: 'POST',
            data: { basket_id: basket_id },
            success: function (result) {
                alert(result.name);
            },
            error: function () {
                alert("error");
            }
        });
    }

    basket.SaveOrder = function (client_id, pay_type) {

        $.ajax({
            url: '/basket/SaveOrder',
            type: 'POST',
            data: { client_id: client_id, pay_type: pay_type },
            success: function (result)
            {
                $("#content").html("Спасибо за заказ");
            },
            error: function (xhr, status, error)
            {
                var err = JSON.parse(xhr.responseText);
                alert(err.Message);
            }
        });
    }

    function InitLazyLoad() {

        $("#content IMG.lazy").lazyload();

        $(".rem_btn").click(function () {
            basket.RemoveFromBasket($(this).attr("rel"));
            basket.Open();
            return false;
        });

        $(".order_btn").click(function () {

            var ptype = $("input[name='ptype_radio']:checked").val();
            if (typeof ptype === "undefined")
            {
                alert("Не выбран способ оплаты");
                return false;
            }

            basket.SaveOrder($(this).attr("rel"), ptype);
            basket.Open();
            return false;
        });

    }

}(window.basket = window.basket || {}, jQuery));