App.Angular.getModule()
.controller("RecordPayController", ["$scope", "$http", "$rootScope", function($scope, $http, $rootScope){
  
  $scope.status = {
    validationErrors : ""
  };
  // define model
  $scope.model = {};

  // function to save payment
  $scope.pay = function(){
    var model = $scope.model;
    $scope.status.validationErrors = "";
    if($scope.frm.$valid){
      // create employee
      $http.post("/api/payments", model)
      .success(function(res) {
          if(res.code !== undefined && res.code !== 0){
              bootbox.alert(res.text);
          }else {
            // emit event to refresh payment list
              $rootScope.$broadcast("pay-added");
          }
      })
    }else {
      $scope.status.validationErrors = "Please fill in all the required fields."
    }
  };

  // cancel
  $scope.cancel = function(){
    bootbox.hideAll()
  }

}]);