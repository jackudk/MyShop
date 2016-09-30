/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/Assets/admin/libs/angular-ui-router/release/angular-ui-router.js" />

(function () {
    angular.module('myshop', ['myshop.products', 'myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider,$urlRouterProvider) {
        $stateProvider.state('home', {
            url: '/admin',
            templateUrl: '/App/Components/home/homeView.html',
            controller:'homeController'
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();