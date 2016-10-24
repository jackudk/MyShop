(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService','$state',
        function ($scope, loginService, $injector, notificationService, $state) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.login = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.error != undefined) {
                        notificationService.displayError("Đăng nhập không đúng.");
                    }
                    else {
                        //var stateService = $injector.get('$state');
                        //stateService.go('home');
                        $state.go('home');
                    }
                });
            }
        }]);
})(angular.module('myshop'));