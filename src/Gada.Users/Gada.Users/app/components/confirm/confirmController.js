(function () {
    "use strict";

    angular
        .module("gada")
        .controller("confirmController", ["$http", "appSettings", "$stateParams", confirmController]);

    function confirmController($http, appSettings, $stateParams) {
        var vm = this;
        vm.message = '';
        confirmEmail();

        function confirmEmail() {
            var req = {
                method: 'POST',
                url: 'https://localhost:44306/api/confirmEmail/' + $stateParams.base64UserId
            }

            $http(req).success(function (response) {
                vm.message = "Thank you for confirming your email address.";
            })
            .error(function (err) {
                vm.message = "An error occured confirming your email address. Please try again later and contact support if the issue persists."
            });
        };
    }
})();