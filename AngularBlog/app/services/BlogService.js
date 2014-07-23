(function () {
    angular.module('blogApp.services').
      service('BlogService', function ($http) {

          var returnObject = function() {
              this.success = false;
              this.errorMessage = '';
              this.value = null;
          };
          
          var onComplete = function (response) {
              var r = new returnObject();
              r.success = true;
              r.value = response.data;
              return r;
          };

          var onError = function (response) {
              var r = new returnObject();
              r.success = false;
              r.errorMessage = "something went wrong";
              return r;
          };

          var getEntriesByUser = function (userName) {
              var promise = $http.get('/blogservice/api/Blog/GetBlogEntriesByUser?userName=' + userName)
                  .then(onComplete, onError);
              return promise;
          };
          
          var addBlog = function (userName, blogText) {
              var data = {                  
                  "BlogText":blogText,
                  "BlogUser":userName
              };
              var promise = $http.post('/blogservice/api/Blog/AddBlogEntry', JSON.stringify(data))
                  .then(onComplete, onError);
              return promise;
          };

          var deleteEntry = function (blogId) {
              var promise = $http.delete('/blogservice/api/Blog/DeleteBlogEntry?blogId=' + blogId)
                  .then(onComplete, onError);
              return promise;
          };

          return {              
              GetEntriesByUser: getEntriesByUser,
              AddBlog: addBlog,
              DeleteEntry: deleteEntry
          };
      });
}());
