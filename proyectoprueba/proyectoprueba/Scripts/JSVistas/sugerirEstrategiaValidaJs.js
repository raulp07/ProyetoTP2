$('#dpt').fdatepicker({
    format: 'mm-dd-yyyy',
    disableDblClickSelection: true,
    language: 'vi'
});


google.charts.load('current', { 'packages': ['bar'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    var data = google.visualization.arrayToDataTable([
      ['Year', 'Sales', 'Expenses', 'Profit'],
      ['2014', 1000, 400, 200],
      ['2015', 1170, 460, 250],
      ['2016', 660, 1120, 300],
      ['2017', 1030, 540, 350]
    ]);

    var options = {
        chart: {
            title: 'Company Performance',
            subtitle: 'Sales, Expenses, and Profit: 2014-2017',
        }
    };

    var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

    chart.draw(data, google.charts.Bar.convertOptions(options));
}


$("#btnGuardar").on("click", function () {


    e.preventDefault();
    var aData= [];
    aData[0] = $("#ddlSelectYear").val(); 
    $("#contentHolder").empty();
    var jsonData = JSON.stringify({ aData:aData});
    $.ajax({
        type: "POST",
        //getListOfCars is my webmethod   
        url: "WebService.asmx/getListOfCars", 
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json", // dataType is json format
        success: OnSuccess,
        error: OnErrorCall
    });
   
    function OnSuccess(response) {
        console.log(response.d)
    }
    function OnErrorCall(response) { console.log(error); }

    alert("hola mundo");
});




