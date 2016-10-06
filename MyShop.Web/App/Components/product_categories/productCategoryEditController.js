/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController)

    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', 'generateSeoTitleService', '$state', '$stateParams'];

    function productCategoryEditController($scope, apiService, notificationService, generateSeoTitleService, $state, $stateParams) {
        $scope.productCategory = {
            
        };
        $scope.parentCategories = [];
        $scope.getSeoTitle = getSeoTitle;
        $scope.updateProductCategory = updateProductCategory;

        function getSeoTitle() {
            $scope.productCategory.Alias = generateSeoTitleService.getSeoTitle($scope.productCategory.Name);
        }

        function getDetail() {
            var config = {
                params: {
                    id: $stateParams.id
                }
            };

            apiService.get("api/productCategory/getbyid", config, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError("Không thể lấy dữ liệu.");
            });
        }

        function getParents() {
            apiService.get("api/productCategory/getall", null, function (result) {
                $scope.parentCategories = result.data;
            }, function (error) {
                console.log("Cannot load parents list.")
            });
        }

        function updateProductCategory() {
            apiService.put("api/productCategory/update", $scope.productCategory, function (result) {
                notificationService.displaySuccess('Cập nhật thành công danh mục \'' + result.data.Name + '\'');
                $state.go('productCategories');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        getParents();
        getDetail();
    }
})(angular.module('myshop.productCategories'));