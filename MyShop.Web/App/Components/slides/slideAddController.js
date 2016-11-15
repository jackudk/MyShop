/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function slideAddController($scope, apiService, notificationService, $state) {
        
        $scope.slide = {
            Status: true
        };
        $scope.choseImage = choseImage;
        $scope.addSlide = addSlide;

        function choseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                });
            }
            finder.popup();
        }

        function addSlide() {
            $scope.slide.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("api/slide/add", $scope.slide, function (result) {
                notificationService.displaySuccess('Thêm mới thành công bản ghi \'' + result.data.Name + '\'');
                $state.go('slides');
            }, function (error) {
                notificationService.displayError('Thêm mới không thành công.');
            });
        }
    }
})(angular.module('myshop.slides'));