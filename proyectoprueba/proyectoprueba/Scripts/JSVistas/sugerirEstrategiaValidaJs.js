//$('#dpt').fdatepicker({
//    format: 'mm-dd-yyyy',
//    disableDblClickSelection: true,
//    language: 'vi'
//});


var nowTemp = new Date();
var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);
var checkin = $('#dpt').fdatepicker({
    onRender: function (date) {
        return date.valueOf() < now.valueOf() ? 'disabled' : '';
    }
}).on('changeDate', function (ev) {
    if (ev.date.valueOf() > checkout.date.valueOf()) {
        var newDate = new Date(ev.date)
        newDate.setDate(newDate.getDate() + 1);
        checkout.update(newDate);
    }
    checkin.hide();
}).data('datepicker');


google.charts.load('current', { 'packages': ['bar'] });
//google.charts.setOnLoadCallback(drawChart);

function drawChart(_data) {
    var fffff = JSON.parse(_data);
    console.log(fffff);
    var data = google.visualization.arrayToDataTable(fffff);
    var options = {
        chart: {
            title: 'Company Performance',
            subtitle: 'Sales, Expenses, and Profit: 2014-2017',
        }
    };
    var chart = new google.charts.Bar(document.getElementById('columnchart_material'));

    chart.draw(data, google.charts.Bar.convertOptions(options));
}


$("#btnGuardar").on("click", function (e) {

    e.preventDefault();


    var idEstrategia = $('#hdIDEstrategia').val();


    var _date = new Date($("#dpt").val());

    if (idEstrategia != 0) {

        var Estrategia = {
            Id_Estrategia: idEstrategia,
            Id_Objetivo: $("#SELOBJ").val(),
            NombreEstrategia: $("#txtnombre").val(),
            DescripcionEstrategia: $("#txtdescripcion").val(),
            Fechacumplimiento: _date,
        }

        var aData = [];
        var index = 0;
        $('.ContenedorRubros .RubroEstrategia_ input').each(function (key, value) {
            var Puntuacion = $(this).val() == '' ? 0 : $(this).val();
            var idRubroAccion = this.id;
            var BEDatoEstadisticoEstrategia = {
                Id_Estrategia: idEstrategia,
                idRubroAccion: idRubroAccion,
                Puntuacion: Puntuacion,
                DescripcionEstrategia: $("#txtdescripcion").val(),
                Fechacumplimiento: _date,
            };
            aData[index] = BEDatoEstadisticoEstrategia;
            index++;
        });

        var jsonData = JSON.stringify({ BEEstrategia: Estrategia, BEDatoEstadisticoEstrategia: aData });
        $.ajax({
            type: "POST",
            url: "http://localhost:7382/sugerirEstrategiaValida/UPDEstrategia",
            contentType: "application/json; charset=utf-8",
            data: jsonData,
            dataType: "json",
            success: OnSuccess,
            error: OnErrorCall
        });
        function OnSuccess(response) {
            if (response) {
                ListarEstrategia();
            }
        }

    } else {
        var Estrategia = {
            Id_Objetivo: $("#SELOBJ").val(),
            NombreEstrategia: $("#txtnombre").val(),
            DescripcionEstrategia: $("#txtdescripcion").val(),
            Fechacumplimiento: _date,
        }
        $.ajax({
            type: "POST",
            url: "http://localhost:7382/sugerirEstrategiaValida/ADDEstrategia",
            contentType: "application/json; charset=utf-8",
            data: "{ BEEstrategia:" + JSON.stringify(Estrategia) + "}",
            dataType: "json",
            success: OnSuccess,
            error: OnErrorCall
        });

        function OnSuccess(response) {

            if (response != 0) {
                var aData = [];
                var index = 0;
                $('.ContenedorRubros .RubroEstrategia_ input').each(function (key, value) {

                    var Puntuacion = $(this).val() == '' ? 0 : $(this).val();
                    var idRubroAccion = this.id;
                    var BEDatoEstadisticoEstrategia = {
                        Id_Estrategia: response,
                        idRubroAccion: idRubroAccion,
                        Puntuacion: Puntuacion,
                        DescripcionEstrategia: $("#txtdescripcion").val(),
                        Fechacumplimiento: _date,
                    };
                    aData[index] = BEDatoEstadisticoEstrategia;
                    index++;
                });

                var jsonData = JSON.stringify({ BEDatoEstadisticoEstrategia: aData });

                $.ajax({
                    type: "POST",
                    url: "http://localhost:7382/sugerirEstrategiaValida/ADDDatoEstadisticoEstrategia",
                    contentType: "application/json; charset=utf-8",
                    data: jsonData,
                    dataType: "json",
                    success: OnSuccess2,
                    error: OnErrorCall
                });
                function OnSuccess2(response) {
                    if (response) {
                        ListarEstrategia();
                    }
                }
            }
        }
        function OnErrorCall(response) { console.log(error); }
    }


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
        data: "{ PMKT:" + JSON.stringify(PlanMarketing) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });

    function OnSuccess(response) {
        $('#SELOBJ').html('');
        $('#SELOBJ').append('<option>--Seleccione--</option>');
        $.each(response, function (key, value) {
            $('#SELOBJ').append('<option value=' + value.Id_Objetivo + '>' + value.NombreObjetivo + '</option>');
        });
        $('#SELOBJ').on('change', function (e) {
            e.preventDefault();
            ListarEstrategia();

        });
    }

});


$('#btnNuevo').on('click', function (e) {
    $('#tituloObj h3').text('Estrategia para cumplir el objetivo de ' + $('#SELOBJ option:selected').text());

    $('#txtnombre').val('');
    $('#txtdescripcion').val('');

    ListarRubroEstrategia();
});

$('#btnSugerir').on('click', function (e) {
    e.preventDefault();


    var RubroEstrategia = {
        Id_Objetivo: $("#SELOBJ").val()
    }
    var jsonData = JSON.stringify({ BERubroEstrategia: RubroEstrategia });
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarRubroEstrategia",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });

    function OnSuccess(response) {
        var jsonData = JSON.stringify({ BEEstrategia: RubroEstrategia });
        $.ajax({
            type: "POST",
            //getListOfCars is my webmethod   
            url: "http://localhost:7382/sugerirEstrategiaValida/ListarDatoEstadisticoEstrategiaObjetivo",
            data: jsonData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess
        });

        function OnSuccess(response2) {


            //Cabezera
            var ArrayCabezera = '';
            var Cabezera = 0;
            ArrayCabezera = '["Rubro","Esperado",';
            $.each(response2, function (key, value2) {
                if (Cabezera != value2.Id_Estrategia) {
                    Cabezera = value2.Id_Estrategia
                    ArrayCabezera += '"' + value2.NombreEstadisticoEstrategia + '",';
                }
            });

            ArrayCabezera = ArrayCabezera.substring(0, ArrayCabezera.length - 1);
            ArrayCabezera += '],';
            
            //Cuerpo
            var data = '';
            $.each(response, function (key, value1) {
                data += '["' + value1.NombreRubroEstrategia + '",';
                data += value1.PorcentajeImportancia +','
                $.each(response2, function (key, value2) {
                    if (value1.idRubroAccion == value2.idRubroAccion) {
                        data += value2.Puntuacion + ',';
                    }
                });
                data = data.substring(0, data.length - 1);
                data += '],';
            });
            data = data.substring(0, data.length - 1);

            data = '[' + ArrayCabezera + data + ']';

            google.charts.setOnLoadCallback(drawChart(data));

            var fffff = JSON.parse(data);

            var asda = '';
            var textomostrar = '';
            $.each(fffff, function (key, value) {
                if (key >0) {
                    textomostrar +="En rubro => "+ value[0];
                    textomostrar += " el valor esperado  es " + value[1];
                    for (var i = 2; i < value.length; i++) {
                        if (value[1] > value[i]) {
                            textomostrar += "  y Falta " + (value[1] - value[i]) ;
                        } else {
                            textomostrar += " y sobra " + (value[i] - value[1]) ;
                        }
                    }
                    textomostrar += '</br>';
                }
            });
            debugger;
            $('#TextoShow').html(textomostrar);
            
        }




    }
    //['Year', 'Sales', 'Expenses', 'Profit'],
    //  ['2014', 1000, 400, 200],
    //  ['2015', 1170, 460, 250],
    //  ['2016', 660, 1120, 300],
    //  ['2017', 1030, 540, 350]
});


function soloNumeros(e) {
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

function ListarEstrategia() {


    var BEEstrategia = {
        Id_Objetivo: $('#SELOBJ').val()
    }
    var jsonData = JSON.stringify({ BEEstrategia: BEEstrategia });
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarEstrategia",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });

    function OnSuccess(response) {
        $('#tbGeneral tbody').html('');
        var html = '';
        $.each(response, function (key, value) {
            var date = new Date(parseInt(value.Fechacumplimiento.substr(6)));
            var displayDate = date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear();

            html += '<tr>' +
            '<td><input type="checkbox" checked /></td>' +
            '<td>' + value.NombreEstrategia + '</td>' +
            '<td>' + (value.EstadoEstrategia == 0? "Registrado":"Sugerido") + '</td>' +
            '<td>' + displayDate + '</td>' +
            '<td> <a href="#" id="btnEditar" class="btnEditar" data-Est="' + value.Id_Estrategia + '"  data-open="exampleModal11">Editar</a> </td>' +
            '</tr>';
        });
        $('#tbGeneral tbody').html(html);


        


        $('.btnEditar').on('click', function () {
            var Id_Estrategia = $(this).attr('data-Est');

            $('#hdIDEstrategia').val(Id_Estrategia);
            var BEEstrategia = {
                Id_Estrategia: Id_Estrategia
            }
            var jsonData = JSON.stringify({ BEEstrategia: BEEstrategia });
            $.ajax({
                type: "POST",
                url: "http://localhost:7382/sugerirEstrategiaValida/ListarEstrategia",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess
            });
            function OnSuccess(response) {

                $.each(response, function (keu, value) {
                    $('#txtnombre').val(value.NombreEstrategia);
                    $('#txtdescripcion').val(value.DescripcionEstrategia);
                    var date = new Date(parseInt(value.Fechacumplimiento.substr(6)));
                    var displayDate = date.getMonth() + "/" + date.getDate() + "/" + date.getFullYear();
                    $('#dpt').val(displayDate);
                    //$("#datepicker").datepicker("setDate", '2017-08-25');
                });

                var jsonData = JSON.stringify({ BEDatoEstadisticoEstrategia: BEEstrategia });
                $.ajax({
                    type: "POST",
                    url: "http://localhost:7382/sugerirEstrategiaValida/ListarDatoEstadisticoEstrategia",
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess
                });
                function OnSuccess(response2) {
                    $('.ContenedorRubros .numeral').each(function () {
                        var _id = this.id;
                        $.each(response2, function (key, value) {
                            if (_id == value.idRubroAccion) {
                                $('#' + _id).val(value.Puntuacion);
                            }
                        });
                    });
                }
            }
        });
        ListarRubroEstrategia();
    }
}
function listarPlanMKT() {
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarPlanMKT",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
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
function ListarRubroEstrategia() {

    var RubroEstrategia = {
        Id_Objetivo: $("#SELOBJ").val()
    }

    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarRubroEstrategia",
        data: "{ BERubroEstrategia:" + JSON.stringify(RubroEstrategia) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });
    function OnSuccess(response) {
        $('.ContenedorRubros').html('');
        var html = '';
        $.each(response, function (key, value) {
            html += '<div class="cell small-3">' +
            '<label>' + value.NombreRubroEstrategia + '</label>' +
            '</div>' +
            '<div class="cell small-2 RubroEstrategia_">' +
                '<input id="' + value.idRubroAccion + '" class="numeral" type="text" onkeypress="return soloNumeros(event)" maxlength="2" />' +
            '</div>';
        });
        $('.ContenedorRubros').html(html);

    }
}

//$('#tbGeneral').DataTable();

listarPlanMKT();

