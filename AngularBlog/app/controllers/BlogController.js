(function () {
    angular.module('blogApp.controllers', []).
      controller('BlogController', function ($scope, BlogService) {
          $scope.ShowBlog = function () {
              $scope.IsProcessing = true;
              BlogService.GetEntries().then(function (d) {
                  $scope.Success = d.success;
                  $scope.ErrorMessage = d.errorMessage;
                  $scope.Entries = d.value;
                  $scope.IsProcessing = false;
              });
          };
      });
}());