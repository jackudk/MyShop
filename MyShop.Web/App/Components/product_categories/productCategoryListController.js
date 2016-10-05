/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.totalPages = 0;
        $scope.page = 0;
        $scope.keyWord = '';

        $scope.getProductCategories = function (page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 2
                }
            };

            apiService.get('/api/productCategory/getallpaging', config, function (result) {
                if (result.data.TotalCount <= 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào.")
                }

                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log('Get ProductCategories failed.');
                console.log(error);
            });
        }
        $scope.getProductCategories();
    }

})(angular.module('myshop.productCategories'));