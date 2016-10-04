/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.totalPages = 0;
        $scope.page = 0;

        $scope.getProductCategories = function (page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 2
                }
            };

            apiService.get('/api/productCategory/getall', config, function (result) {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log('Get ProductCategories failed.');
                console.log(error.message);
            });
        }
        $scope.getProductCategories();
    }

})(angular.module('myshop.productCategories'));