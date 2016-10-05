/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('myshop.productCategories', ['myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('productCategories', {
            url: '/productCategories',
            templateUrl: '/App/Components/product_categories/productCategoryListView.html',
            controller: 'productCategoryListController'
        }).state('productCategory_add', {
            url: '/productCategory-add',
            templateUrl: '/App/Components/product_categories/productCategoryAddView.html',
            controller: 'productCategoryAddController'
        });
    }
})();