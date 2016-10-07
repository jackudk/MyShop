/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.totalPages = 0;
        $scope.totalCount = 0;
        $scope.page = 0;
        $scope.keyWord = '';

        $scope.selectedItems = [];
        $scope.checkAll = checkAll;
        $scope.isCheckedAll = false;

        $scope.getProductCategories = getProductCategories;
        $scope.deleteProductCategories = deleteProductCategories;
        $scope.deleteMulti = deleteMulti;

        $scope.$watch('productCategories', function (n, o) {
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
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
            } else {
                angular.forEach($scope.productCategories, function (item) {
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
                    ids:JSON.stringify(selectedID)
                }
            }
            
            $ngBootbox.confirm('Bạn có chắc muốn xóa những bản ghi này không?').then(function () {
                apiService.del('/api/productCategory/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' danh mục.');
                    $scope.getProductCategories();
                }, function (error) {
                    console.log('Delete ProductCategories failed.');
                    notificationService.displayError("Không xóa được bản ghi.")
                });
            });
        }

        function deleteProductCategories(id) {
            var config = {
                params: {
                    id: id
                }
            };

            $ngBootbox.confirm('Bạn có chắc muốn xóa bản ghi này không?').then(function () {
                apiService.del('/api/productCategory/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công danh mục \'' + result.data.Name + '\'');
                    $scope.getProductCategories();
                }, function (error) {
                    console.log('Delete ProductCategories failed.');
                    notificationService.displayError("Không xóa được bản ghi.")
                });
            });
        }

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 4
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