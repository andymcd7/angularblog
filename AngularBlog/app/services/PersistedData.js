(function () {
    angular.module('blogApp.services').
      factory('PersistedData', function () {

          var blogData = {};
          blogData.Entries = [];
          blogData.Users = [];

          blogData.SetEntries = function(entries) {
              blogData.Entries = entries;
          };
          blogData.AddEntry = function(entry) {
              blogData.Entries.push(entry);
          };
          blogData.SetUsers = function (users) {
              blogData.Users = users;
          };

          return blogData;
      });
}());
