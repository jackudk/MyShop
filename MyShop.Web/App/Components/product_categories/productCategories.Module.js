/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('myshop.productCategories', ['myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('productCategories', {
            parent: 'base',
            url: '/productCategories',
            templateUrl: '/App/Components/product_categories/productCategoryListView.html',
            controller: 'productCategoryListController'
        }).state('productCategory_add', {
            parent: 'base',
            url: '/productCategory_add',
            templateUrl: '/App/Components/product_categories/productCategoryAddView.html',
            controller: 'productCategoryAddController'
        }).state('productCategory_edit', {
            parent: 'base',
            url: '/productCategory_edit/:id',
            templateUrl: '/App/Components/product_categories/productCategoryEditView.html',
            controller: 'productCategoryEditController'
        });
    }
})();