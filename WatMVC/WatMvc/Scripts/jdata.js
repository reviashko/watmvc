(function (catalog, $, undefined) {

    catalog.OnDocumentReady = function () {
        $(document).ready(function () {

            //ctalog
            LoadTest();
            InitLazyLoad();
            //$("#content IMG.lazy").click(function () { window.location.href = $(this).attr("rel"); });

            $(".lmenu A").click(function () {
                catalog.Open($(this).attr("id"));
                return false;
            });

            //card
            $("#add_btn").click(function () {
                basket.AddToBasket($(this).attr("rel"), $("#add_qnt").val());
                return false;
            });
        });
    }

    catalog.Open = function (menu_id) {

        //$("#content").html("<img src=\"http://img.watshop.ru/d/loader.gif\"/>");
        $.ajax({
            type: 'POST',
            data: { menu_id: menu_id },
            url: '/catalog/GetCatalogDataById',
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
                $("#content").text("error on load");
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
        //$(".GItm .logos[rel='1']").append("<img src='http://www.watshop.ru/img/dacha.png'/>");
    }

}(window.catalog = window.catalog || {}, jQuery));


(function (basket, $, undefined) {

    basket.OnDocumentReady = function () {
        $(document).ready(function () {
            InitLazyLoad();
        });
    }

    basket.Open = function (menu_id) {

        //$("#content").html("<img src=\"http://img.watshop.ru/d/loader.gif\"/>");
        $.ajax({
            type: 'POST',
            data: { menu_id: menu_id },
            url: '/basket/Get',
            success: function (res) {
                var items = res.bit;
                var template = $.templates("#basketTemplate");
                var htmlOutput = template.render(items);
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

    function InitLazyLoad() {

        $("#content IMG.lazy").lazyload();

        $(".rem_btn").click(function () {
            basket.RemoveFromBasket($(this).attr("rel"));
            basket.Open();
            return false;
        });
    }

}(window.basket = window.basket || {}, jQuery));