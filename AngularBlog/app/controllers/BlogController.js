(function () {

    var app = angular.module('blogApp', []);
    
    var blogService = function ($http) {

        // todo: move this into a separate javascript file
        // todo: wrap success/error in a single object to provide status

        var onComplete = function (response) {
            return response.data;
        };

        var onError = function (response) {
            return "Something went wrong";
        };

        this.GetEntries = function () {
            var promise = $http.get('/blogservice/api/Blog/GetAllBlogEntries')
                .then(onComplete, onError);
            return promise;
        };
    };

    app.service('BlogService', ['$http', blogService]);

    var blogController = function ($scope, BlogService) {
        
        $scope.ShowBlog = function () {
            BlogService.GetEntries().then(function(d) {
                $scope.Entries = d;
            });
        };
    };

    app.controller('BlogController', ['$scope', 'BlogService', blogController]);

}());
