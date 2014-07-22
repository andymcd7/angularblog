(function () {
    angular.module('blogApp.services').
      factory('PersistedData', function () {

          var blogData = {};
          blogData.Entries = {};
          blogData.Users = [];

          blogData.SetEntries = function(userName, entries) {
              blogData.Entries[userName] = entries;
          };
          blogData.AddEntry = function(userName, entry) {
              blogData.Entries[userName].push(entry);
          };
          blogData.SetUsers = function (users) {
              blogData.Users = users;
          };
          
          return blogData;
      });
}());
