(function (app) {
    'use strict';
    app.service('authenticationService', ['$http', '$q', 'localStorageService', 'authenticationData',
        function ($http, $q, localStorageService, authenticationData) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                tokenInfo = data;
                localStorageService.set("TokenInfo", JSON.stringify(tokenInfo));
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                //localStorageService.set("TokenInfo", null);
                localStorageService.remove('TokenInfo');
            }

            this.init = function () {
                if (localStorageService.get("TokenInfo")) {
                    tokenInfo = JSON.parse(localStorageService.get("TokenInfo"));
                    authenticationData.authenticationData.IsAuthenticated = true;
                    authenticationData.authenticationData.userName = tokenInfo.userName;
                }
            }

            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                }
                else {
                    $http.defaults.headers.common['Authorization'] = '';
                }
            }

            this.init();

            //this.validateRequest = function () {
            //    var deferred = $q.defer();
            //    //var url = 'api/home/TestMethod';
            //    //$http.get(url).then(function () {
            //    //    deferred.resolve(null);
            //    //}, function (error) {
            //    //    deferred.reject(error);
            //    //});
            //    return deferred.promise;
            //}
        }
    ]);
})(angular.module('myshop.common'));