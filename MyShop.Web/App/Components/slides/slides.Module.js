(function () {
    angular.module('myshop.slides', ['myshop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('slides', {
            parent: 'base',
            url: '/slides',
            templateUrl: '/App/Components/slides/slideListView.html',
            controller: 'slideListController'
        }).state('slide_add', {
            parent: 'base',
            url: '/slide_add',
            templateUrl: '/App/Components/slides/slideAddView.html',
            controller: 'slideAddController'
        }).state('slide_edit', {
            parent: 'base',
            url: '/slide_edit/:id',
            templateUrl: '/App/Components/slides/slideEditView.html',
            controller: 'slideEditController'
        });
    }
})();