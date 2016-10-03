/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.getProductCategories = function () {
            apiService.get('/api/productCategory/getall', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log('Get ProductCategories failed.');
            });
        }
        $scope.getProductCategories();
    }

})(angular.module('myshop.productCategories'));