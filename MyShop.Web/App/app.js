﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/Assets/admin/libs/angular-ui-router/release/angular-ui-router.js" />

(function () {
    angular.module('myshop',
        ['myshop.products',
            'myshop.productCategories',
            'myshop.slides',
            'myshop.common'])
        .config(config)
        .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('base', {
            templateUrl: '/App/Shared/views/baseView.html',
            abstract: true
        }).state('login', {
            url: '/login',
            templateUrl: '/App/Components/login/loginView.html',
            controller: 'loginController'
        }).state('home', {
            parent: 'base',
            url: '/admin',
            templateUrl: '/App/Components/home/homeView.html',
            controller: 'homeController'
        });
        $urlRouterProvider.otherwise('/login');
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();