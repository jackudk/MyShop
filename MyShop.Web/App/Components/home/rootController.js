(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$scope', '$state', 'authenticationData', 'authenticationService', 'loginService'];

    function rootController($scope, $state, authenticationData, authenticationService, loginService) {
        $scope.logout = logout;
        $scope.authentication = authenticationData.authenticationData;

        //authenticationService.validateRequest();

        function logout() {
            loginService.logOut();
            $state.go('login');
        }
    }
})(angular.module('myshop'));