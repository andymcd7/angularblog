(function () {
    angular.module('blogApp.services').
      factory('PersistedData', function () {

          var blogData = {};
          blogData.Entries = [];
          blogData.SetEntries = function(entries) {
              blogData.Entries = entries;
          };
          blogData.AddEntry = function(entry) {
              blogData.Entries.push(entry);
          };
          
          return blogData;
      });
}());
