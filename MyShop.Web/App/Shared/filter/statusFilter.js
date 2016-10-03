/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true) {
                return 'Kích hoạt';
            } else {
                return 'Đã khóa';
            }
        };
    });
})(angular.module('myshop.common'));