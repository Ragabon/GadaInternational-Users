(function () {

    var gada = angular.module('gada', ['ui.router', 'common.services']);

    gada.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/home');

        $stateProvider
            .state('home', {
                url: '/home',
                templateUrl: 'app/components/home/homeView.html',
                controller: 'homeController as vm'
            })
            .state('confirmEmail', {
                url: '/confirmEmail/{base64UserId}',
                templateUrl: '/app/components/confirm/confirmView.html',
                controller: 'confirmController as vm'
            });
    });
})();