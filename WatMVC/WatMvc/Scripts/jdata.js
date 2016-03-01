(function (catalog, $, undefined) {

    catalog.OnDocumentReady = function () {
        $(document).ready(function () {
            LoadTest();
            JSONTest();
            Init();
        });
    }

    //catalog.JSONTest = function () {
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

    //catalog.LoadTest = function () {
    function LoadTest() {
        $("#tester1").load("/home/TestDetails", function (response, status, xhr) {
            if (status != "success") {
                $("#tester1").html('an error has occured');
            }
        });
    }

    //catalog.Init = function () {
    function Init() {
        $("IMG.lazy").lazyload();
        $("IMG.lazy").click(function () { window.location.href = $(this).attr("rel"); }).css("cursor", "pointer");
        $(".GItm .logos[rel='1']").append("<img src='http://www.watshop.ru/img/dacha.png'/>");
    }

    //Private Property
    //var isHot = true;

    //Public Property
    //catalog.ingredient = "Bacon Strips";

    //Public Method
    //catalog.fry = function () {
    //    var oliveOil;

    //    addItem("\t\n Butter \n\t");
    //    addItem(oliveOil);
    //    console.log("Frying " + skillet.ingredient);
    //};

    //Private Method
    //function addItem(item) {
    //    if (item !== undefined) {
    //        console.log("Adding " + $.trim(item));
    //    }
    //}
}(window.catalog = window.catalog || {}, jQuery));