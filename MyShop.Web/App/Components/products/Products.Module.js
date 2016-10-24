
(function () {
    angular.module('myshop.products', ['myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            parent: 'base',
            url: '/products',
            templateUrl: '/App/Components/products/productListView.html',
            controller: 'productListController'
        }).state('product_add', {
            parent: 'base',
            url: '/product_add',
            templateUrl: '/App/Components/products/productAddView.html',
            controller: 'productAddController'
        }).state('product_edit', {
            parent: 'base',
            url: '/product_edit/:id',
            templateUrl: '/App/Components/products/productEditView.html',
            controller: 'productEditController'
        });
    }
})();