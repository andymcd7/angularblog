(function () {
    angular.module('blogApp.controllers').
      controller('AddBlogController', function ($scope, $location, $routeParams, BlogService, PersistedData) {
          $scope.AddBlog = function () {
              $scope.IsProcessing = true;
              BlogService.AddBlog($routeParams.userName, $scope.blogtext).then(function (d) {
                  if (d.success) {
                      PersistedData.AddEntry($routeParams.userName, d.value);
                      $location.path("blog/" + $routeParams.userName);
                  } else {
                      $scope.ErrorMessage = d.errorMessage;
                  }
                  $scope.IsProcessing = false;
              });
          };
      });
}());