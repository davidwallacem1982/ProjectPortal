var portugues = {
    "decimal": ",",
    "thousands": ".",
    "sEmptyTable": "Nenhum registro encontrado",
    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
    "sInfoFiltered": "(Filtrados de _MAX_ registros)",
    "sInfoPostFix": "",
    "sLengthMenu": "_MENU_ resultados por página",
    "sLoadingRecords": "Em Processamento...",
    "sProcessing": "Processando...",
    "sZeroRecords": "Nenhum registro encontrado",
    "sSearch": "Pesquisar",
    "oPaginate": {
        "sNext": "Próximo",
        "sPrevious": "Anterior",
        "sFirst": "Primeiro",
        "sLast": "Último"
    },
    "oAria": {
        "sSortAscending": ": Ordenar colunas de forma ascendente",
        "sSortDescending": ": Ordenar colunas de forma descendente"
    },
    "select": {
                    "rows": {
                        "_": "Selecionado %d linhas",
                        "0": "Nenhuma linha selecionada",
                        "1": "Selecionado 1 linha"
                    }
     }
}

var responsivo = {
    breakpoints: [
        { name: 'bigdesktop', width: Infinity },
        { name: 'meddesktop', width: 1480 },
        { name: 'smalldesktop', width: 1280 },
        { name: 'medium', width: 1188 },
        { name: 'tabletl', width: 1024 },
        { name: 'btwtabllandp', width: 848 },
        { name: 'tabletp', width: 768 },
        { name: 'mobilel', width: 480 },
        { name: 'mobilep', width: 320 }
    ],
    details: true
}

var util =
{
    //url = Link de onde serão obtidos os valores
    //elemento = Elemento onde será inserido o dropdown
    //msgpadrao = Mensagem default do dropdown
    dropdown: function (url, elemento, msgpadrao) {
        var result;
        $.get(url, function (data) {
            result = data;
            elemento.html('');

            // opção padrão

            elemento.append('<option value="0">' + msgpadrao + '</option>');

            // checa se o resultado é vazio

            if (result != '0') {

                // Loop para popular a dropdown

                $.each(result, function (k, v) {
                    console.log(k, v);
                    elemento.append('<option value="' + v.id + '">' + v.tipo + '</option>');

                });

            }
        });
    },

    //url = Link de onde serão obtidos os valores
    //elemento = Elemento onde será inserido o dropdown
    //msgpadrao = Mensagem default do dropdown
    dropdownGrupo: function (url, elemento, msgpadrao) {
        var result;
        $.get(url, function (data) {
            result = data;
            elemento.html('');

            // opção padrão

            elemento.append('<option value="0">' + msgpadrao + '</option>');

            // checa se o resultado é vazio

            if (result != '0') {

                // Loop para popular a dropdown

                $.each(result, function (k, v) {
                    elemento.append('<option value="' + v.id + '">' + v.titulo + '</option>');

                });

            }
        });
    },

     //url = Link de onde serão obtidos os valores
    //elemento = Elemento onde será inserido o dropdown
    //msgpadrao = Mensagem default do dropdown
    dropdownCargo: function (url, elemento, msgpadrao) {
        var result;
        $.get(url, function (data) {
            result = data;
            elemento.html('');

            // opção padrão

            elemento.append('<option value="0">' + msgpadrao + '</option>');

            // checa se o resultado é vazio

            if (result != '0') {

                // Loop para popular a dropdown

                $.each(result, function (k, v) {
                    elemento.append('<option value="' + v.id + '">' + v.nome + '</option>');

                });

            }
        });
    }
}