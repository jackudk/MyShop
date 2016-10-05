/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/Assets/admin/libs/toastr/toastr.js" />

(function (app) {
    app.service('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-bottom-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 100
        };

        function displaySuccess(message) {
            toastr.success(message);
        }
        function displayWarning(message) {
            toastr.warning(message);
        }
        function displayInfo(message) {
            toastr.info(message);
        }
        function displayError(message) {
            if (Array.isArray(message)) {
                message.each(function (mes) {
                    toastr.error(mes);
                });
            } else {
                toastr.error(message);
            }
        }

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };
    }
})(angular.module('myshop.common'))