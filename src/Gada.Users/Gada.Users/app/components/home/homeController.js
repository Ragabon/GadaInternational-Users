(function () {
    "use strict";

    angular
        .module("gada")
        .controller("homeController", ["$http", "appSettings", homeController]);

    function homeController($http, appSettings) {
        var vm = this;
        var rootUrl = 'https://localhost:44302/api/user/';

        vm.registration = {
            'email': '',
            'password': '',
            'firstName': '',
            'lastName': ''
        };

        vm.loginDetails = {
            'email': '',
            'password': ''
        };

        vm.register = function () {
            var req = {
                method: 'POST',
                url: rootUrl + 'register',
                data: vm.registration
            }

            $http(req).success(function (response) {
                console.log(response);
            })
            .error(function (err) {
                console.log(err);
            });
        };

        vm.login = function () {
            var req = {
                method: 'POST',
                url: rootUrl + 'login',
                data: vm.loginDetails
            }

            $http(req).success(function (response) {
                console.log(response);
            })
            .error(function (err) {
                console.log(err);
            });
        };

        vm.testEmail = function () {
            var req = {
                method: 'GET',
                url: rootUrl + 'email'
            }

            $http(req).success(function (response) {
                console.log(response);
            })
            .error(function (err) {
                console.log(err);
            });
        }
    }
})();