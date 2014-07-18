(function () {
    angular.module('blogApp.controllers').
      controller('BlogController', function ($scope, BlogService, PersistedData) {
          $scope.ShowBlog = function (refreshData) {
              if (!refreshData) {
                  if (PersistedData.Entries && PersistedData.Entries.length > 0) {
                      $scope.Success = true;
                      $scope.ErrorMessage = '';
                      $scope.Entries = PersistedData.Entries;
                  } else {
                      refreshData = true;
                  }
              }
              
              if (refreshData) {
                  $scope.IsProcessing = true;
                  BlogService.GetEntries().then(function (d) {
                      $scope.Success = d.success;
                      $scope.ErrorMessage = d.errorMessage;
                      $scope.Entries = d.value;
                      PersistedData.SetEntries(d.value);
                      $scope.IsProcessing = false;
                  });
              }
          };
      });
}());