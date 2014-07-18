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

          var getEntries = function () {
              var promise = $http.get('/blogservice/api/Blog/GetAllBlogEntries')
                  .then(onComplete, onError);
              return promise;
          };
          
          var addBlog = function (blogUser, blogText) {
              var data = {                  
                  "BlogText":blogText,
                  "BlogUser":1
              };
              var promise = $http.post('/blogservice/api/Blog/AddBlogEntry', JSON.stringify(data))
                  .then(onComplete, onError);
              return promise;
          };

          return {              
              GetEntries: getEntries,
              AddBlog: addBlog
          };
      });
}());
