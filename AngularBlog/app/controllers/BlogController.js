(function () {
    angular.module('blogApp.controllers').
        controller('BlogController', function($scope, $routeParams, $location, BlogService, UserService, PersistedData) {

            $scope.Initialize = function () {
                var userName = $routeParams.userName;
                if (userName && userName.length > 0) {
                    if (PersistedData.Entries && PersistedData.Entries[userName] && PersistedData.Entries[userName].length > 0) {
                        $scope.Success = true;
                        $scope.ErrorMessage = '';
                        $scope.Entries = PersistedData.Entries[userName];
                    } else {
                        $scope.IsProcessing = true;
                        BlogService.GetEntriesByUser(userName).then(function (d) {
                            $scope.Success = d.success;
                            $scope.ErrorMessage = d.errorMessage;
                            $scope.Entries = d.value;
                            PersistedData.SetEntries(userName, d.value);
                            $scope.IsProcessing = false;
                        });
                    }
                }
            };
            
            $scope.DeleteEntry = function (blogId) {
                var userName = $scope.SelectedUser.UserName;
                var blogEntries = PersistedData.Entries[userName];
                BlogService.DeleteEntry(blogId);
                for (var i = 0; i < blogEntries.length; i++) {
                    if (blogEntries[i].BlogId == blogId) {
                        blogEntries.splice(i, 1);
                        break;
                    }
                }
                PersistedData.SetEntries(userName, blogEntries);
                $scope.Entries = blogEntries;
            };

            $scope.ViewUser = function () {
                var userName = '';
                if ($scope.SelectedUser && $scope.SelectedUser.UserName) {
                    userName = $scope.SelectedUser.UserName;
                }
                $location.path('blog/' + userName);
            };
            
            var setSelectedUser = function() {
                if ($routeParams.userName) {
                    for (var i = 0; i < $scope.Users.length; i++) {
                        if ($scope.Users[i].UserName == $routeParams.userName) {
                            $scope.SelectedUser = $scope.Users[i];
                        }
                    }
                }
            };

            // get list of users on page load
            if (PersistedData.Users && PersistedData.Users.length > 0) {
                $scope.Users = PersistedData.Users;
                setSelectedUser();
            } else {
                UserService.GetUsers().then(function (d) {
                    $scope.Users = d.value;
                    PersistedData.SetUsers(d.value);
                    setSelectedUser();
                });
            }

        });
}());