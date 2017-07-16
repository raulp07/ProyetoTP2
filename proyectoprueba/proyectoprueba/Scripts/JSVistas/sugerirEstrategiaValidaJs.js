$('#dpt').fdatepicker({
    format: 'mm-dd-yyyy',
    disableDblClickSelection: true,
    language: 'vi'
});


//google.charts.load('current', { 'packages': ['bar'] });
//google.charts.setOnLoadCallback(drawChart);

//function drawChart() {
//    var data = google.visualization.arrayToDataTable([
//      ['Year', 'Sales', 'Expenses', 'Profit'],
//      ['2014', 1000, 400, 200],
//      ['2015', 1170, 460, 250],
//      ['2016', 660, 1120, 300],
//      ['2017', 1030, 540, 350]
//    ]);

//    var options = {
//        chart: {
//            title: 'Company Performance',
//            subtitle: 'Sales, Expenses, and Profit: 2014-2017',
//        }
//    };

//    var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

//    chart.draw(data, google.charts.Bar.convertOptions(options));
//}


function listarPlanMKT() {
    $.ajax({
        type: "POST",
        //getListOfCars is my webmethod   
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarPlanMKT",
        //data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json", // dataType is json format
        success: OnSuccess,
        error: OnErrorCall
    });
    function OnSuccess(response) {
        $('#SELPMKT').html('');
        $('#SELPMKT').append('<option>--Seleccione--</option>');
        $.each(response, function (key, value) {
            $('#SELPMKT').append('<option value=' + value.Id_PlanMarketing + '>' + value.nombrePanMarketing + '</option>');
        });
    }
    function OnErrorCall(response) { console.log(error); }
}

$("#btnGuardar").on("click", function (e) {

    e.preventDefault();
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/prueba",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        error: OnErrorCall
    });

    function OnSuccess(response) {
        console.log(response.d)
    }
    function OnErrorCall(response) { console.log(error); }

});

$('#SELPMKT').change(function (e) {

    e.preventDefault();
    var PlanMarketing = {
        Id_PlanMarketing: $("#SELPMKT").val()
    }

    $.ajax({
        type: "POST",
        //getListOfCars is my webmethod   
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarObjetivos",
        data: "{ PMKT:"+ JSON.stringify(PlanMarketing)+ "}",
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: OnSuccess
    });

    function OnSuccess(response) {
        $('#SELOBJ').html('');
        $('#SELOBJ').append('<option>--Seleccione--</option>');
        $.each(response, function (key, value) {
            $('#SELOBJ').append('<option value=' + value.Id_Objetivo + '>' + value.NombreObjetivo + '</option>');
        });
    }

});


$('#btnNuevo').on('click', function (e) {
    $('#tituloObj h3').text('Estrategia para cumplir el objetivo de ' + $('#SELOBJ option:selected').text());
});

function soloNumeros(e) {
    debugger;
    var key = window.Event ? e.which : e.keyCode
    if (key >= 48 && key <= 57) {
        var _ID = $('#' + e.currentTarget.id).val() + e.key;
        if (parseInt(_ID) <= 10 && parseInt(_ID) >= 0) {
            return true
        } else {
            $('#' + e.currentTarget.id).val('');
            return true;
        }
    } else {
        return false;
    }
    
}


listarPlanMKT();