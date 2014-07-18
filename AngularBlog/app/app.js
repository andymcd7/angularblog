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
            .when("/blog", {
                templateUrl: "app/views/blog.html",
                controller: "BlogController"
            })
            .when("/add", {
                templateUrl: "app/views/addBlog.html",
                controller: "AddBlogController"
            })
            .otherwise({
                 redirectTo: "/blog"
            });
    });
}());