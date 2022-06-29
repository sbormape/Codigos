'use strict';

angular.module('myApp')
    .controller('SocioController', function ($scope, $filter) {

        $scope.Listado1ModalOpen = function () {
            $("#Listado1Modal").modal("show");
        }

        $scope.Listado2ModalOpen = function () {
            $("#Listado2Modal").modal("show");
        }

        $scope.Listado3ModalOpen = function () {
            $("#Listado3Modal").modal("show");
        }
    });

