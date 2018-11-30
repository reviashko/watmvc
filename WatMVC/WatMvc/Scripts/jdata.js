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
                catalog.Open(1, $(this).attr("id"));
                return false;
            });

        });
    }

    catalog.Open = function (page_id, menu_id) {

        $.ajax({
            type: 'POST',
            data: { menu_id: menu_id, page_id: page_id },
            url: '/ajax/GetCatalogDataById',
            success: function (res) {
                var items = res.itms;
                var menu = res.mnus;
                var pager_items = res.pager;

                var catalog_template = $.templates("#catalogTemplate");
                var catalog_htmlOutput = catalog_template.render(items);

                $("#content").html(catalog_htmlOutput);
                InitLazyLoad();
                $("#content IMG.lazy").click(function () { window.location.href = $(this).attr("rel"); });

                var pager_div = $('<div class="cl"></div><div id="pager"></div>');
                $("#content").append(pager_div);

                var pager_template = $.templates("#pagerTemplate");
                var pager_htmlOutput = pager_template.render(pager_items);

                $("#pager").html(pager_htmlOutput);

                $("#pager A").click(function () {
                    PagerClick(menu_id, $(this).attr("rel"));
                });

            },
            error: function (res) {
                $("#content").text("error on load ((");
            }
        });
    }

    function PagerClick(menu_id, pager_id)
    {
        catalog.Open(pager_id, menu_id);
        return false;
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

    basket.Open = function () {

        alert('refresh');

        $.ajax({
            type: 'POST',
            data: { },
            url: '/basket/Get',
            success: function (res) {
                var template = $.templates("#basketTemplate");
                var htmlOutput = template.render(res.bit);
                $("#BasketItems").html(htmlOutput);
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
            data: { articul: articul, cnt: qnt },
            success: function (result) {
                alert(result.name);
            },
            error: function () {
                alert("error");
            }
        });
    }

    basket.RemoveFromBasket = function (articul) {

        $.ajax({
            url: '/basket/Remove',
            type: 'POST',
            data: { articul: articul },
            success: function (result) {
                return true;
            },
            error: function () {
                alert("error");
                return false;
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

        if ($(".BItem").length > 0)
        {
            $("#basketService").show();
            $("#basketEmpty").hide();
        } else {
            $("#basketService").hide();
            $("#basketEmpty").show();
        }

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