(function () {
    angular.module('blogApp.controllers').
      controller('BlogController', function ($scope, BlogService, UserService, PersistedData) {

          // get list of users on page load
          UserService.GetUsers().then(function (d) {
              $scope.Users = d.value;
              $scope.SelectedUser = d.value[0];
              PersistedData.SetUsers(d.value);
          });

          $scope.ShowBlogByUser = function () {
              $scope.IsProcessing = true;
              BlogService.GetEntriesByUser($scope.SelectedUser.UserId).then(function (d) {
                  $scope.Success = d.success;
                  $scope.ErrorMessage = d.errorMessage;
                  $scope.Entries = d.value;
                  $scope.IsProcessing = false;
              });
          };

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