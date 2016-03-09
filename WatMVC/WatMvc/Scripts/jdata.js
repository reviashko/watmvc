(function (catalog, $, undefined) {

    catalog.OnDocumentReady = function () {
        $(document).ready(function () {
            LoadTest();
            JSONTest();
            Init();
        });

    }

    catalog.Open = function (menu_id) {
       
        $.ajax({
            type: 'POST',
            data: { menu_id: menu_id },
            url: '/catalog/GetCatalodDataById',
            success: function (res) {
                //$("div.content").text(res.itms[0].Articul);
                var template = $.templates("#catalogTemplate");
                var htmlOutput = template.render(res.itms);
                $("#catView").html(htmlOutput);
                alert(1);
            },
            error: function (res) {
                $("div.content").text("error on load");
            }
        });

    }

    function JSONTest() {
        $.ajax({
            type: 'GET',
            url: '/catalog/JSONTest',
            success: function (emp) {
                $('#tester2').text(emp.employee.Employee_name);
            },
            error: function (emp) {
                $('#tester2').text("an error ocured");
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

    function Init() {
        $("IMG.lazy").lazyload();
        $("IMG.lazy").click(function () { window.location.href = $(this).attr("rel"); }).css("cursor", "pointer");
        $(".GItm .logos[rel='1']").append("<img src='http://www.watshop.ru/img/dacha.png'/>");
        $(".lmenu A").click(function () { catalog.Open(1); return false; });
    }

}(window.catalog = window.catalog || {}, jQuery));