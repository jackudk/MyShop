/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productEditController', productEditController)

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', 'generateSeoTitleService', '$state', '$stateParams'];

    function productEditController($scope, apiService, notificationService, generateSeoTitleService, $state, $stateParams) {
        $scope.product = {

        };
        $scope.productCategories = [];
        $scope.moreImages = [];

        $scope.choseImage = choseImage;
        $scope.choseMoreImages = choseMoreImages;
        $scope.getSeoTitle = getSeoTitle;
        $scope.updateProduct = updateProduct;

        function getSeoTitle() {
            $scope.product.Alias = generateSeoTitleService.getSeoTitle($scope.product.Name);
        }

        function getDetail() {
            var config = {
                params: {
                    id: $stateParams.id
                }
            };

            apiService.get("api/product/getbyid", config, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError("Không thể lấy dữ liệu.");
            });
        }

        function getProductCategories() {
            apiService.get("api/productCategory/getall", null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log("Cannot load product categories.")
            });
        }

        function choseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        function choseMoreImages() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }

        function updateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put("api/product/update", $scope.product, function (result) {
                notificationService.displaySuccess('Cập nhật thành công sản phẩm \'' + result.data.Name + '\'');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Cập nhật không thành công.');
            });
        }

        getProductCategories();
        getDetail();
    }
})(angular.module('myshop.products'));