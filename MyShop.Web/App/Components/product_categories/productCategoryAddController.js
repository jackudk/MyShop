/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'generateSeoTitleService', 'notificationService', '$state'];

    function productCategoryAddController($scope, apiService,generateSeoTitleService, notificationService, $state) {
        $scope.productCategory = {
            Status: true
        };
        $scope.parentCategories = [];
        $scope.getSeoTitle = getSeoTitle;
        $scope.addProductCategory = addProductCategory;

        function getSeoTitle() {
            $scope.productCategory.Alias = generateSeoTitleService.getSeoTitle($scope.productCategory.Name);
        }

        function getParents() {
            apiService.get("api/productCategory/getall", null, function (result) {
                $scope.parentCategories = result.data;
            }, function (error) {
                console.log("Cannot load parents list.")
            });
        }

        function addProductCategory() {
            apiService.post("api/productCategory/add", $scope.productCategory, function (result) {
                notificationService.displaySuccess('Thêm mới thành công danh mục \'' + result.data.Name + '\'');
                $state.go('productCategories');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }

        getParents();
    }
})(angular.module('myshop.productCategories'));