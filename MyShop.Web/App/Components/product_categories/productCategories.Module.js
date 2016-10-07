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
            url: '/productCategory_add',
            templateUrl: '/App/Components/product_categories/productCategoryAddView.html',
            controller: 'productCategoryAddController'
        }).state('productCategory_edit', {
            url: '/productCategory_edit/:id',
            templateUrl: '/App/Components/product_categories/productCategoryEditView.html',
            controller: 'productCategoryEditController'
        });
    }
})();