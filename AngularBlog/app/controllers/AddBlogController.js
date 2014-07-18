(function () {
    angular.module('blogApp.controllers').
      controller('AddBlogController', function ($scope, $location, BlogService, PersistedData) {
          $scope.AddBlog = function () {
              $scope.IsProcessing = true;
              BlogService.AddBlog($scope.bloguser, $scope.blogtext).then(function (d) {
                  if (d.success) {
                      PersistedData.AddEntry(d.value);
                      $location.path("blog");
                  } else {
                      $scope.ErrorMessage = d.errorMessage;
                  }
                  $scope.IsProcessing = false;
              });
          };
      });
}());