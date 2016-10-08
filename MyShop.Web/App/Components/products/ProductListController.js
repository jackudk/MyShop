/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productListController', productListController);
    
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.totalPages = 0;
        $scope.totalCount = 0;
        $scope.page = 0;
        $scope.keyWord = '';

        $scope.selectedItems = [];
        $scope.checkAll = checkAll;
        $scope.isCheckedAll = false;

        $scope.getProducts = getProducts;
        $scope.deleteProduct = deleteProduct;
        $scope.deleteMulti = deleteMulti;

        $scope.$watch('products', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selectedItems = checked;
                $('#btnDeleteMulti').removeAttr('disabled');
            } else {
                $('#btnDeleteMulti').attr('disabled', true);
            }
        }, true);

        function checkAll() {
            if ($scope.isCheckedAll == true) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
            }
        }

        function deleteMulti() {
            var selectedID = [];
            angular.forEach($scope.selectedItems, function (item) {
                selectedID.push(item.ID);
            });

            var config = {
                params: {
                    ids: JSON.stringify(selectedID)
                }
            }

            $ngBootbox.confirm('Bạn có chắc muốn xóa những bản ghi này không?').then(function () {
                apiService.del('/api/product/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' sản phẩm.');
                    $scope.getProducts();
                }, function (error) {
                    console.log('Delete Products failed.');
                    notificationService.displayError("Không xóa được bản ghi.")
                });
            });
        }

        function deleteProduct(id) {
            var config = {
                params: {
                    id: id
                }
            };

            $ngBootbox.confirm('Bạn có chắc muốn xóa bản ghi này không?').then(function () {
                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công sản phẩm \'' + result.data.Name + '\'');
                    $scope.getProducts();
                }, function (error) {
                    console.log('Delete Products failed.');
                    notificationService.displayError("Không xóa được bản ghi.")
                });
            });
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 4
                }
            };

            apiService.get('/api/product/getallpaging', config, function (result) {
                if (result.data.TotalCount <= 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào.")
                }

                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log('Get Products failed.');
                console.log(error);
            });
        }


        $scope.getProducts();
    }
})(angular.module('myshop.products'));