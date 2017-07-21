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


    if ($('#txtnombre').val() == "") {
        alert('Ingrese un nombre para la estrategia');
        return;
    }

    if ($('#txtdescripcion').val() == "") {
        alert('Ingrese una descripción para la estrategia');
        return;
    }
    if ($('#txtcosto').val() == "") {
        alert('Ingrese un costo para la Acción');
        return;
    }
    var diferencial = parseFloat($('#hdPresupuesto').val()) - (parseFloat($('#hdCostosAccion').val()) + parseFloat($('#txtcosto').val()));
    if (diferencial < 0) {
        alert('El costo de la Acción sobrepasa un ' + (diferencial * -1) + ' al presupuestado del Plan de Marketing');
        return;
    }

    var idAccion = $('#hdIDAccion').val();


    var _date = new Date($("#dpt").val());

    if (idAccion != 0) {

        var Accion = {
            Id_Accion: idAccion,
            Id_Estrategia: $("#SELESTR").val(),
            nombreAccion: $("#txtnombre").val(),
            descipcionAccion: $("#txtdescripcion").val(),
            costoAccion: $("#txtcosto").val(),
            Fechacumplimiento: _date,
        }

        var aData = [];
        var index = 0;
        $('.ContenedorRubros .RubroAccion_ input').each(function (key, value) {

            var Puntuacion = $(this).val() == '' ? 0 : $(this).val();
            var idRubroAccion = this.id;
            var BEDatoEstadisticoAccion = {
                Id_Accion: idAccion,
                idRubroAccion: idRubroAccion,
                Puntuacion: Puntuacion,
                Fechacumplimiento: _date,
            };
            aData[index] = BEDatoEstadisticoAccion;
            index++;
        });

        var jsonData = JSON.stringify({ BEAccion: Accion, BEDatoEstadisticoAccion: aData });
        $.ajax({
            type: "POST",
            url: "http://localhost:7382/sugerirAccionValida/UPDAccion",
            contentType: "application/json; charset=utf-8",
            data: jsonData,
            dataType: "json",
            success: OnSuccess,
            error: OnErrorCall
        });
        function OnSuccess(response) {
            if (response) {
                ListarAccion();
            }
        }

    } else {
        var BEAccion = {
            Id_Estrategia: $("#SELESTR").val(),
            nombreAccion: $("#txtnombre").val(),
            descipcionAccion: $("#txtdescripcion").val(),
            Fechacumplimiento: _date,
            costoAccion: $("#txtcosto").val(),
        }
        $.ajax({
            type: "POST",
            url: "http://localhost:7382/sugerirAccionValida/ADDAccion",
            contentType: "application/json; charset=utf-8",
            data: "{ BEAccion:" + JSON.stringify(BEAccion) + "}",
            dataType: "json",
            success: OnSuccess,
            error: OnErrorCall
        });

        function OnSuccess(response) {

            if (response != 0) {
                var aData = [];
                var index = 0;
                $('.ContenedorRubros .RubroAccion_ input').each(function (key, value) {

                    var Puntuacion = $(this).val() == '' ? 0 : $(this).val();
                    var idRubroAccion = this.id;
                    var BEDatoEstadisticoAccion = {
                        Id_Accion: response,
                        idRubroAccion: idRubroAccion,
                        Puntuacion: Puntuacion,
                        Fechacumplimiento: _date,
                    };
                    aData[index] = BEDatoEstadisticoAccion;
                    index++;
                });

                var jsonData = JSON.stringify({ BEDatoEstadisticoAccion: aData });

                $.ajax({
                    type: "POST",
                    url: "http://localhost:7382/sugerirAccionValida/ADDDatoEstadisticoAccion",
                    contentType: "application/json; charset=utf-8",
                    data: jsonData,
                    dataType: "json",
                    success: OnSuccess2,
                    error: OnErrorCall
                });
                function OnSuccess2(response) {
                    if (response) {
                        ListarAccion();
                    }
                }
            }
        }
        function OnErrorCall(response) { console.log(error); }
    }
    $('#columnchart_material').html('');
    $('#TextoShow').html('');
    $('#exampleModal11').foundation('close');

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
        //$('#SELOBJ').append('<option value="0">--Seleccione--</option>');
        var html = '<option value="0">--Seleccione--</option>';
        $.each(response, function (key, value) {
            html += '<option value=' + value.Id_Objetivo + '>' + value.NombreObjetivo + '</option>';
            //$('#SELOBJ').append('<option value=' + value.Id_Objetivo + '>' + value.NombreObjetivo + '</option>');
        });
        $('#SELOBJ').html(html);

        $('#SELESTR').html('');
        var html = '<option value="0">--Seleccione--</option>';
        $('#SELESTR').html(html);

        if ($('#SELESTR').val() != "0") {
            $('#btnSugerir').removeAttr('disabled');
            $('#btnNuevo').removeAttr('disabled');
        } else {
            $('#btnSugerir').attr('disabled', true);
            $('#btnNuevo').attr('disabled', true);
            $('#tbGeneral tbody').html('');
        }

        CalcularPresupuesto();
    }

});


$('#SELOBJ').change(function (e) {
    e.preventDefault();

    var BEEstrategia = {
        Id_Objetivo: $("#SELPMKT").val()
    }

    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarEstrategia",
        data: "{ BEEstrategia:" + JSON.stringify(BEEstrategia) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });

    function OnSuccess(response) {

        $('#SELESTR').html('');
        var html = '<option value="0">--Seleccione--</option>';
        $.each(response, function (key, value) {
            html += '<option value=' + value.Id_Estrategia + '>' + value.NombreEstrategia + '</option>';
        });

        $('#SELESTR').html(html);
        if ($('#SELESTR').val() != "0") {
            $('#btnSugerir').removeAttr('disabled');
            $('#btnNuevo').removeAttr('disabled');
        } else {
            $('#btnSugerir').attr('disabled', true);
            $('#btnNuevo').attr('disabled', true);
            $('#tbGeneral tbody').html('');
        }
    }

    //if ($('#SELOBJ').val() != "0") {
    //    $('#btnSugerir').removeAttr('disabled');
    //    $('#btnNuevo').removeAttr('disabled');
    //    ListarAccion();
    //} else {
    //    $('#btnSugerir').attr('disabled', true);
    //    $('#btnNuevo').attr('disabled', true);
    //    $('#tbGeneral tbody').html('');
    //}
});


$('#SELESTR').on('change', function (e) {
    e.preventDefault();

    if ($('#SELESTR').val() != "0") {
        $('#btnSugerir').removeAttr('disabled');
        $('#btnNuevo').removeAttr('disabled');
        ListarAccion();
    } else {
        $('#btnSugerir').attr('disabled', true);
        $('#btnNuevo').attr('disabled', true);
        $('#tbGeneral tbody').html('');
    }
});


$('#btnNuevo').on('click', function (e) {
    if ($(this).attr('disabled')) {
        return;
    }
    $('#exampleModal11').foundation('open');
    $('#tituloEstr h3').text('Acción para cumplir la Estrategia de ' + $('#SELESTR option:selected').text());

    $('#txtnombre').val('');
    $('#txtdescripcion').val('');
    $('#txtcosto').val('');

    var _date = new Date();
    var displayDate = (_date.getMonth() + 1) + "/" + (_date.getDate() + 1) + "/" + _date.getFullYear();
    $('#dpt').val(displayDate);

    ListarRubroAccion();

});

$('#btnSugerir').on('click', function (e) {
    e.preventDefault();

    if ($(this).attr('disabled')) {
        return;
    }

    var BERubroAccion = {
        Id_Objetivo: $("#SELOBJ").val()
    }
    var jsonData = JSON.stringify({ BERubroAccion: BERubroAccion });
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirAccionValida/ListarRubroAccion",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });

    function OnSuccess(response) {

        var BEAccion = {
            Id_Estrategia: $("#SELESTR").val()
        }
        var jsonData = JSON.stringify({ BEAccion: BEAccion });
        $.ajax({
            type: "POST",
            //getListOfCars is my webmethod   
            url: "http://localhost:7382/sugerirAccionValida/ListarDatoEstadisticoAccionEstrategia",
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
                if (Cabezera != value2.Id_Accion) {
                    Cabezera = value2.Id_Accion
                    ArrayCabezera += '"' + value2.nombreDatoEstadisticoAccion + '",';
                }
            });

            ArrayCabezera = ArrayCabezera.substring(0, ArrayCabezera.length - 1);
            ArrayCabezera += '],';

            //Cuerpo
            var data = '';
            $.each(response, function (key, value1) {
                data += '["' + value1.nombreRubroAccion + '",';
                data += value1.PorcentajeImportancia + ','
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
            var fffff = JSON.parse(data);
            var jsoncabezera = JSON.parse("[" + ArrayCabezera.substring(0, ArrayCabezera.length - 1) + "]");
            var textomostrar = '';

            //$.each(fffff, function (key, value) {

            //    if (key > 0) {
            //        textomostrar += "En rubro => " + value[0];
            //        textomostrar += " el valor esperado  es " + value[1];

            //        for (var i = 2; i < value.length; i++) {
            //            if (value[1] > value[i]) {
            //                textomostrar += " , falta " + (value[1] - value[i]) + " en la estrategia " + jsoncabezera[0][i] + " para alcanzar el valor esperado";
            //            } else {
            //                textomostrar += " , excede " + (value[i] - value[1]) + " al esperado con la estrategia " + jsoncabezera[0][i];
            //            }
            //        }
            //        textomostrar += '</br>';
            //    }
            //});
            //$('#TextoShow').html(textomostrar);
            google.charts.setOnLoadCallback(drawChart(data));
        }
    }
    //['Year', 'Sales', 'Expenses', 'Profit'],
    //  ['2014', 1000, 400, 200],
    //  ['2015', 1170, 460, 250],
    //  ['2016', 660, 1120, 300],
    //  ['2017', 1030, 540, 350]
});


function soloNumeros_Longitud(e) {
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

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    if (key >= 48 && key <= 57) {
        return true
    } else {
        return false;
    }

}

function ListarAccion() {


    var BEEstrategia = {
        Id_Objetivo: $('#SELOBJ').val()
    }
    var jsonData = JSON.stringify({ BEEstrategia: BEEstrategia });
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirAccionValida/ListarAccion",
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
            var displayDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();

            html += '<tr>' +
            //'<td><input type="checkbox" checked /></td>' +
            '<td>' + value.nombreAccion + '</td>' +
            '<td>' + (value.EstadoAccion == 0 ? "Registrado" : "Sugerido") + '</td>' +
            '<td>' + displayDate + '</td>' +
            '<td> <a href="#" id="btnEditar" class="btnEditar" data-Est="' + value.Id_Accion + '"  data-open="exampleModal11">Editar</a> </td>' +
            '</tr>';
        });
        $('#tbGeneral tbody').html(html);

        $('.btnEditar').on('click', function () {

            var Id_Accion = $(this).attr('data-Est');

            $('#hdIDAccion').val(Id_Accion);
            var BEAccion = {
                Id_Accion: Id_Accion
            }
            var jsonData = JSON.stringify({ BEAccion: BEAccion });
            $.ajax({
                type: "POST",
                url: "http://localhost:7382/sugerirAccionValida/ListarAccion",
                data: jsonData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess
            });
            function OnSuccess(response) {

                $.each(response, function (keu, value) {
                    $('#txtnombre').val(value.nombreAccion);
                    $('#txtdescripcion').val(value.descipcionAccion);
                    $('#txtcosto').val(value.costoAccion);
                    var date = new Date(parseInt(value.Fechacumplimiento.substr(6)));
                    var displayDate = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
                    $('#dpt').val(displayDate);

                });

                var jsonData = JSON.stringify({ BEDatoEstadisticoAccion: BEAccion });
                $.ajax({
                    type: "POST",
                    url: "http://localhost:7382/sugerirAccionValida/ListarDatoEstadisticoAccion",
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


        ListarRubroAccion();
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
        $('#SELPMKT').append('<option value="0">--Seleccione--</option>');
        $.each(response, function (key, value) {
            $('#SELPMKT').append('<option value=' + value.Id_PlanMarketing + '>' + value.nombrePanMarketing + '</option>');
        });
    }
    function OnErrorCall(response) { console.log(error); }
}
function ListarRubroAccion() {

    var BERubroAccion = {
        Id_Objetivo: $("#SELOBJ").val()
    }

    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirAccionValida/ListarRubroAccion",
        data: "{ BERubroAccion:" + JSON.stringify(BERubroAccion) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });
    function OnSuccess(response) {
        $('.ContenedorRubros').html('');
        var html = '';
        $.each(response, function (key, value) {
            html += '<div class="cell small-3">' +
            '<label>' + value.nombreRubroAccion + '</label>' +
            '</div>' +
            '<div class="cell small-2 RubroAccion_">' +
                '<input id="' + value.idRubroAccion + '" class="numeral" type="text" onkeypress="return soloNumeros_Longitud(event)" maxlength="2" />' +
            '</div>';
        });
        $('.ContenedorRubros').html(html);

        if ($("#SELESTR").val() != 0) {
            CalcularPresupuesto();
        }

    }
}

function CalcularPresupuesto() {
    var BEPlanMarketing = {
        Id_PlanMarketing: $("#SELPMKT").val()
    }
    var jsonData = JSON.stringify({ BEPlanMarketing: BEPlanMarketing });
    $.ajax({
        type: "POST",
        url: "http://localhost:7382/sugerirEstrategiaValida/ListarPlanMKTPresupuesto",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess
    });
    function OnSuccess(response) {
        var presupuesto = response[0].presupuesto;
        var costoAccionGeneral = response[0].costoAccionGeneral;

        $('#hdPresupuesto').val(presupuesto);
        $('#hdCostosAccion').val(costoAccionGeneral);
        var mensaje = '';
        mensaje = 'El Plan de Marketing cuenta con un presupuesto de ' + presupuesto + ', hasta el momento se tiene acumulado en todas las acciones un total de ' + costoAccionGeneral;
        $('#lblMensaje').html(mensaje);


    }
}

//$('#tbGeneral').DataTable();

listarPlanMKT();


//new Vue({
//    el: "#app",
//    data: {
//        ListaPlanMKT:null,
//        listaPersonas: null,
//        misDatos: null
//    },
//    methods: {
//        traerAxios: function () {
//            axios.post('/sugerirEstrategiaValida/ListarPlanMKT/').then(function (response) {
//                if (response.data.Error == false) {
//                    this.ListaPlanMKT = response.data.ListaPlanMKT
//                } else {
//                    alert("Ocurrió un error, revisar el console log para mas detalles...");
//                    console.error(data.Mensaje);
//                }
//            }.bind(this)).catch(function (error) {
//                console.log(error);
//            });
//        },
//        traerJquery: function () {
//            $.get('/Home/GetPersonas/', function (data) {
//                if (data.Error == false) {
//                    this.listaPersonas = data.personaLista
//                } else {
//                    alert("Ocurrió un error, revisar el console log para mas detalles...");
//                    console.error(data.Mensaje);
//                }
//            }.bind(this));
//        },
//        limpiarLista: function () {
//            this.listaPersonas = null;
//        }
//    },
//    computed: {
//    },
//    created: function () {
//        this.traerAxios();
//        //this.traerAxios();
//        //this.traerJquery();
//    },
//    mounted: function () {

//    }
//});
