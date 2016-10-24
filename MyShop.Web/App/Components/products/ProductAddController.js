/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'generateSeoTitleService', 'notificationService', '$state'];

    function productAddController($scope, apiService, generateSeoTitleService, notificationService, $state) {
        $scope.ckEditorOptions = {
            language: 'en',
            height: '200px'
        };
        $scope.product = {
            Status: true
        };
        $scope.productCategories = [];
        $scope.moreImages = [];

        $scope.getSeoTitle = getSeoTitle;
        $scope.choseImage = choseImage;
        $scope.choseMoreImages = choseMoreImages;
        $scope.addProduct = addProduct;

        function getSeoTitle() {
            $scope.product.Alias = generateSeoTitleService.getSeoTitle($scope.product.Name);
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

        function getProductCategories() {
            apiService.get("api/productCategory/getall", null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log("Cannot load product categories.")
            });
        }

        function addProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("api/product/add", $scope.product, function (result) {
                notificationService.displaySuccess('Thêm mới thành công sản phẩm \'' + result.data.Name + '\'');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }

        getProductCategories();
    }
})(angular.module('myshop.products'));