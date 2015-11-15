(function () {
    'use strict';
    var app = angular.module('fortesApp', ['ngRoute', 'ui.mask', 'ui.utils.masks', 'datetime']);
    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.headers.common['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.common['Pragma'] = 'no-cache';
    }]);
    app.controller("categoriasController", ['$http', '$scope', function ($http, $scope) {
        $scope.load;
        $scope.searchValue;
        $scope.categorias = [];
        $scope.categoria = { Id: "", Nome: "" };


        $scope.load = function () {
            $("#loading").show();
            var url = "/categorias/lista";
            if ($scope.searchValue != undefined && $scope.searchValue != "") {
                url += "?valor=" + $scope.searchValue;
            }
            $http.get(url)
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.categorias = response.resultado;
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
        };

        $scope.open = function (data) {
            if (data == undefined) {
                $scope.categoria = { Id: "", Nome: "" };
            } else {
                $scope.categoria = { Id: data.Id, Nome: data.Nome };
            }
            $("#modalForm").modal('show');
        }

        $scope.save = function () {
            if ($scope.categoria.Id == "") {
                $("#loading").show();
                $http.post('/categorias/incluir', $scope.categoria)
                .success(function (response) {
                    if (response.status == "OK") {
                        $scope.load();
                        $("#modalForm").modal('hide');
                        $scope.categoria = { Id: "", Nome: "" };
                        alertaSucesso($("main > section"), response.resultado);
                    } else {
                        alertaErro($("form"), response.resultado || "Ocorreu um erro inesperado!");
                    }
                    $("#loading").hide();
                })
                .error(function (data) {
                    $("#loading").hide();
                    alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                })
            } else {
                $http.post('/categorias/editar', $scope.categoria)
                .success(function (data, status, headers, config) {
                    if (data.status == "OK") {
                        $scope.load();
                        $("#modalForm").modal('hide');
                        $scope.categoria = { Id: "", Nome: "" };
                        alertaSucesso($("main > section"), data.resultado);
                    } else {
                        alertaErro($("form"), data.resultado || "Ocorreu um erro inesperado!");
                    }
                    $("#loading").hide();
                })
                .error(function (data) {
                    $("#loading").hide();
                    alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                })

            }
        }

        $scope.delete = function (data) {
            bootbox.confirm({
                message: "Deseja excluir o registro?",
                buttons: { "confirm": { label: "Sim" }, "cancel": { label: "Não" } },
                callback: function (result) {
                    if (result) {
                        $("#loading").show();
                        $http.get('/categorias/excluir?id=' + data.Id)
                        .success(function (response) {
                            if (response.status == "OK") {
                                $scope.load();
                                alertaSucesso($("main > section"), response.resultado);

                            } else {
                                alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                            }
                            $("#loading").hide();
                        })
                        .error(function (data) {
                            $("#loading").hide();
                            alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                        })
                    }
                }
            });
        };

        $scope.load();
    }]);


    app.controller("despesasController", ['$http', '$scope', '$filter', function ($http, $scope, $filter) {
        $scope.load;
        $scope.filtroData;
        $scope.filtroCategoria;
        $scope.categorias = [];
        $scope.despesas = [];
        $scope.despesa = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };


        $scope.load = function () {
            $("#loading").show();
            var url = "/despesas/lista";
            if ($scope.filtroData != undefined && $scope.filtroData != "") {
                url += "?data=" + $scope.filtroData;
            }
            if ($scope.filtroCategoria != undefined && $scope.filtroCategoria != "") {
                if ($scope.filtroData != undefined && $scope.filtroData != "") {
                    url += "&categoriaId=" + $scope.filtroCategoria;
                } else {
                    url += "?categoriaId=" + $scope.filtroCategoria;

                }
            }
            $http.get(url)
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.despesas = response.resultado;
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
            $http.get("/categorias/lista")
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.categorias = response.resultado;
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
        };

        $scope.open = function (data) {
            if (data == undefined) {
                $scope.despesa = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };
            } else {
                $scope.despesa = { Id: data.Id, Data: $filter("date")(data.Data, "dd/MM/yyyy"), Valor: data.Valor, Observacao: data.Observacao, CategoriaId: data.CategoriaId, CategoriaNome: data.CategoriaNome };
            }
            $("#modalForm").modal('show');
        }

        $scope.save = function () {
            if ($scope.despesa.Data != "") {
                $scope.despesa.Data = $filter("date")(Date.parse($scope.despesa.Data), "dd/MM/yyyy");
            }
            if ($scope.despesa.Id == "") {
                $("#loading").show();
                $http.post('/despesas/incluir', $scope.despesa)
                .success(function (response) {
                    if (response.status == "OK") {
                        $scope.load();
                        $("#modalForm").modal('hide');
                        $scope.despesa = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };
                        alertaSucesso($("main > section"), response.resultado);
                    } else {
                        alertaErro($("form"), response.resultado || "Ocorreu um erro inesperado!");
                    }
                    $("#loading").hide();
                })
                .error(function (data) {
                    $("#loading").hide();
                    alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                })
            } else {
                $http.post('/despesas/editar', $scope.despesa)
                .success(function (data, status, headers, config) {
                    if (data.status == "OK") {
                        $scope.load();
                        $("#modalForm").modal('hide');
                        $scope.despesa = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };
                        alertaSucesso($("main > section"), data.resultado);
                    } else {
                        alertaErro($("form"), data.resultado || "Ocorreu um erro inesperado!");
                    }
                    $("#loading").hide();
                })
                .error(function (data) {
                    $("#loading").hide();
                    alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                })

            }
        }

        $scope.delete = function (data) {
            bootbox.confirm({
                message: "Deseja excluir o registro?",
                buttons: { "confirm": { label: "Sim" }, "cancel": { label: "Não" } },
                callback: function (result) {
                    if (result) {
                        $("#loading").show();
                        $http.get('/despesas/excluir?id=' + data.Id)
                        .success(function (response) {
                            if (response.status == "OK") {
                                $scope.load();
                                alertaSucesso($("main > section"), response.resultado);

                            } else {
                                alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                            }
                            $("#loading").hide();
                        })
                        .error(function (data) {
                            $("#loading").hide();
                            alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                        })
                    }
                }
            });
        };

        $scope.load();
    }]);


    app.controller("receitasController", ['$http', '$scope', '$filter', function ($http, $scope, $filter) {
        $scope.load;
        $scope.filtroData;
        $scope.filtroCategoria;
        $scope.categorias = [];
        $scope.receitas = [];
        $scope.receita = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };


        $scope.load = function () {
            $("#loading").show();
            var url = "/receitas/lista";
            if ($scope.filtroData != undefined && $scope.filtroData != "") {
                url += "?data=" + $scope.filtroData;
            }
            if ($scope.filtroCategoria != undefined && $scope.filtroCategoria != "") {
                if ($scope.filtroData != undefined && $scope.filtroData != "") {
                    url += "&categoriaId=" + $scope.filtroCategoria;
                } else {
                    url += "?categoriaId=" + $scope.filtroCategoria;

                }
            }
            $http.get(url)
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.receitas = response.resultado;
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
            $http.get("/categorias/lista")
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.categorias = response.resultado;
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
        };

        $scope.open = function (data) {
            if (data == undefined) {
                $scope.receita = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };
            } else {
                $scope.receita = { Id: data.Id, Data: $filter("date")(data.Data, "dd/MM/yyyy"), Valor: data.Valor, Observacao: data.Observacao, CategoriaId: data.CategoriaId, CategoriaNome: data.CategoriaNome };
            }
            $("#modalForm").modal('show');
        }

        $scope.save = function () {
            if ($scope.receita.Data != "") {
                $scope.receita.Data = $filter("date")(Date.parse($scope.receita.Data), "dd/MM/yyyy");
            }
            if ($scope.receita.Id == "") {
                $("#loading").show();
                $http.post('/receitas/incluir', $scope.receita)
                .success(function (response) {
                    if (response.status == "OK") {
                        $scope.load();
                        $("#modalForm").modal('hide');
                        $scope.receita = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };
                        alertaSucesso($("main > section"), response.resultado);
                    } else {
                        alertaErro($("form"), response.resultado || "Ocorreu um erro inesperado!");
                    }
                    $("#loading").hide();
                })
                .error(function (data) {
                    $("#loading").hide();
                    alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                })
            } else {
                $http.post('/receitas/editar', $scope.receita)
                .success(function (data, status, headers, config) {
                    if (data.status == "OK") {
                        $scope.load();
                        $("#modalForm").modal('hide');
                        $scope.receita = { Id: "", Data: "", Valor: "", Observacao: "", CategoriaId: "", CategoriaNome: "" };
                        alertaSucesso($("main > section"), data.resultado);
                    } else {
                        alertaErro($("form"), data.resultado || "Ocorreu um erro inesperado!");
                    }
                    $("#loading").hide();
                })
                .error(function (data) {
                    $("#loading").hide();
                    alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                })

            }
        }

        $scope.delete = function (data) {
            bootbox.confirm({
                message: "Deseja excluir o registro?",
                buttons: { "confirm": { label: "Sim" }, "cancel": { label: "Não" } },
                callback: function (result) {
                    if (result) {
                        $("#loading").show();
                        $http.get('/receitas/excluir?id=' + data.Id)
                        .success(function (response) {
                            if (response.status == "OK") {
                                $scope.load();
                                alertaSucesso($("main > section"), response.resultado);

                            } else {
                                alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                            }
                            $("#loading").hide();
                        })
                        .error(function (data) {
                            $("#loading").hide();
                            alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
                        })
                    }
                }
            });
        };

        $scope.load();
    }]);


    app.controller("relatoriosController", ['$http', '$scope', '$filter', function ($http, $scope, $filter) {
        $scope.filtroDataInicio;
        $scope.filtroDataFim;
        $scope.filtroCategoria;
        $scope.filtroAgrupamento;
        $scope.categorias = [];
        $scope.relatorio = [];
        $scope.load = function () {
            $("#loading").show();
            $http.get("/categorias/lista")
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.categorias = response.resultado;
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
        };

        $scope.visualizar = function () {
            $("#loading").show();
            var url = $scope.filtroAgrupamento ? "/relatorios/agrupado" : "/relatorios/detalhado";
            var filtroObj = undefined;
            var filtroDataInicio = undefined;
            var filtroDataFim = undefined;
            var filtroCategoria = undefined;

            if ($scope.filtroDataInicio != undefined && $scope.filtroDataInicio != "") {
                filtroDataInicio = "dataInicio=" + $scope.filtroDataInicio;
                filtroObj = !filtroObj ? "?" : "&";
                url += filtroObj + filtroDataInicio;
            }
            if ($scope.filtroDataFim != undefined && $scope.filtroDataFim != "") {
                filtroDataFim = "dataFim=" + $scope.filtroDataFim;
                filtroObj = !filtroObj ? "?" : "&";
                url += filtroObj + filtroDataFim;
            }
            if ($scope.filtroCategoria != undefined && $scope.filtroCategoria != "") {
                filtroCategoria = "categoriaId=" + $scope.filtroCategoria;
                filtroObj = !filtroObj ? "?" : "&";
                url += filtroObj + filtroCategoria;
            }
            $http.get(url)
            .success(function (response) {
                if (response.status == "OK") {
                    $scope.relatorio = response.resultado;
                    if ($scope.filtroAgrupamento) {
                        $("#agrupado").attr("style", "display:");
                        $("#detalhado").attr("style", "display:none");

                    } else {
                        $("#detalhado").attr("style", "display:");
                        $("#agrupado").attr("style", "display:none");
                    }
                    $("#relatorio").attr("style", "display:");
                    $("#formulario").attr("style", "display:none");
                } else {
                    alertaErro($("main > section"), response.resultado || "Ocorreu um erro inesperado!");
                }
                $("#loading").hide();
            })
            .error(function (data) {
                $("#loading").hide();
                alertaErro($("main > section"), data || "Ocorreu um erro inesperado!");
            })
        }
        $scope.imprimir = function () {
            if ($scope.filtroAgrupamento) {
                exportarPdf("#tabAgrupado");

            } else {
                exportarPdf("#tabDetalhado");
            }
        }
        $scope.fechar = function () {
            $("#relatorio").attr("style", "display:none");
            $("#formulario").attr("style", "display:");
        }

        $scope.valorTotal = function () {
            "use strict";
            var total = 0;
            "use strict";
            var count = 0;
            for (count = 0; count < $scope.relatorio.length; count++) {
                total += $scope.relatorio[count].Valor;
            }
            return total;
        }

        $scope.valorTotalGrupo = function (data) {
            "use strict";
            var total = 0;
            "use strict";
            var count = 0;
            for (count = 0; count < data.Detalhes.length; count++) {
                total += data.Detalhes[count].Valor;
            }
            return total;
        }
        $scope.load();
    }]);
})();