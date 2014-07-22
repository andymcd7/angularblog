(function () {
    var app = angular.module('blogApp', [
        'blogApp.controllers',
        'blogApp.services',
        'ngRoute'
    ]);

    angular.module('blogApp.controllers', []);
    angular.module('blogApp.services', []);
    
    app.config(function($routeProvider) {
        $routeProvider
            .when("/blog/:userName", {
                templateUrl: "app/views/blog.html",
                controller: "BlogController"
            })
            .when("/add/:userName", {
                templateUrl: "app/views/addBlog.html",
                controller: "AddBlogController"
            })
            .otherwise({
                 redirectTo: "/blog"
            });
    });
}());