/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function slideListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.slides = [];
        $scope.totalPages = 0;
        $scope.totalCount = 0;
        $scope.page = 0;
        $scope.keyWord = '';

        $scope.selectedItems = [];
        $scope.checkAll = checkAll;
        $scope.isCheckedAll = false;

        $scope.getSlides = getSlides;
        $scope.deleteSlide = deleteSlide;
        $scope.deleteMulti = deleteMulti;

        $scope.$watch('slides', function (n, o) {
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
                angular.forEach($scope.slides, function (item) {
                    item.checked = true;
                });
            } else {
                angular.forEach($scope.slides, function (item) {
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
                apiService.del('/api/slide/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' sản phẩm.');
                    $scope.getslides();
                }, function (error) {
                    console.log('Delete slides failed.');
                    notificationService.displayError("Không xóa được bản ghi.")
                });
            });
        }

        function deleteSlide(id) {
            var config = {
                params: {
                    id: id
                }
            };

            $ngBootbox.confirm('Bạn có chắc muốn xóa bản ghi này không?').then(function () {
                apiService.del('/api/slide/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công bản ghi \'' + result.data.Name + '\'');
                    $scope.getslides();
                }, function (error) {
                    console.log('Delete slides failed.');
                    notificationService.displayError("Không xóa được bản ghi.")
                });
            });
        }

        function getSlides(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 4
                }
            };

            apiService.get('/api/slide/getallpaging', config, function (result) {
                if (result.data.TotalCount <= 0) {
                    notificationService.displayWarning("Không tìm thấy bản ghi nào.")
                }

                $scope.slides = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log('Get slides failed.');
                console.log(error);
            });
        }


        $scope.getSlides();
    }
})(angular.module('myshop.slides'));