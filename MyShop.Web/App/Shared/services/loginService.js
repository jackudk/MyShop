(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authenticationData',
    function ($http, $q, authenticationService, authenticationData) {
        var userInfo;
        var deferred;

        this.login = function (userName, password) {
            deferred = $q.defer();
            var data = "grant_type=password&username=" + userName + "&password=" + password;
            $http.post('/oauth/token', data, {
                headers:
                   { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (response) {
                userInfo = {
                    accessToken: response.access_token,
                    userName: userName
                };
                authenticationService.setTokenInfo(userInfo);
                authenticationData.authenticationData.IsAuthenticated = true;
                authenticationData.authenticationData.userName = userName;
                deferred.resolve(null);
            })
            .error(function (err, status) {
                authenticationData.authenticationData.IsAuthenticated = false;
                authenticationData.authenticationData.userName = "";
                deferred.resolve(err);
            });
            return deferred.promise;
        }

        this.logOut = function () {
            authenticationService.removeToken();
            authenticationData.authenticationData.IsAuthenticated = false;
            authenticationData.authenticationData.userName = "";
        }
    }]);
})(angular.module('myshop.common'));