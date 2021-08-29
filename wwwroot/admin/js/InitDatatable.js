var initTable = function (urlLoad) {

    console.log("initTable")

    tabla = $("#kt_table_1").DataTable({
        processing: true,
        columns: columns,
        columnDefs: columnDefs,
        ajax: {
            "url": urlLoad,
            "type": "GET",
            "error": function (e) {

            },
            "dataSrc": function (d) {
                if (d.success) {
                    if (d.message !== null && d.message !== undefined)
                        successMessage(d.message);
                    return d.data;
                }
                else {
                    errorMessage(d.message);
                    return false;
                }
            }
        },
        "initComplete": function (settings, json) {

            console.log("initComplete");
            $("#tabla-partial tr").addClass("hvr-grow-shadow");
            InitTabla();

        },
        "bPaginate": true,
        "bDestroy": true,
        responsive: !0,
        dom: `<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>
                                                    <'row'<'col-sm-12'tr>>
                                                    <'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            'colvis'
        ],
        lengthMenu: [50, 100, 250],
        pageLength: 50,
        language: {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningun dato disponible en esta tabla",
            "sInfo": "_TOTAL_ registros",
            "sInfoEmpty": "Registros 0 al 0 de un total de 0",
            "sInfoFiltered": "",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
        },
        order: []
    });
};

function initMetodos(crear, editar, eliminar, cambiar) {

    console.log("InitMetodos");

    urlCreate = crear;
    urlEdit = editar;
    urlDelete = eliminar;
    urlCambiar = cambiar;

}