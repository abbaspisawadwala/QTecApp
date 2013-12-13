﻿
'use strict';

define([], function () {

    var app = angular.module('QTec', ['ngRoute', 'ngAnimate', 'qtec.Directives', 'qtec.Animations', 'ui.bootstrap']);

    app.config(['$routeProvider',  '$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$httpProvider',
        function ($routeProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, $httpProvider) {

            

            app.register =
            {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                filter: $filterProvider.register,
                factory: $provide.factory,
                service: $provide.service
            };

            

            $routeProvider.when('/', {
                templateUrl: '/Personal/Employees',
                controller: 'EmployeesController'
            });
            $routeProvider.when('/about', {
                templateUrl: '/Personal/About',
                controller: 'AboutController'
            });
            $routeProvider.when('/employeeedit/:employeeId', {
                templateUrl: '/Personal/EmployeeEditNew',
                controller: 'EmployeeEditNewController'
            });
            $routeProvider.when('/reports/designations', {
                templateUrl: '/QTecRpts/Designations',
                
            });
           $routeProvider.otherwise({
                redirectTo: '/'
            });

    }]);

    

    return app;
});





