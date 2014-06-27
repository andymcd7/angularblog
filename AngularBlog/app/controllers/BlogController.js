(function () {

    var app = angular.module('blogApp', []);
    
    var blogController = function ($scope, $http) {

        var onComplete = function(response) {
            $scope.Entries = response.data;
        };
        
        var onError = function (response) {
            $scope.Error = "Something went wrong";
        };
        
        $scope.ShowBlog = function () {
            $http.get('/blogservice/api/Blog/GetAllBlogEntries')
                .then(onComplete, onError);
        };
    };

    app.controller('BlogController', ['$scope', '$http', blogController]);

}());
