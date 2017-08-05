App.Angular.getModule()
.controller("AddEmployeeController", ["$scope", "$http", function($scope, $http){
  
  $scope.status = {
    validationErrors : ""
  }
  // define model
  $scope.model = {};

  // function to add employee
  $scope.add = function(){
    var model = $scope.model;
    $scope.status.validationErrors = "";
    if($scope.frm.$valid){
      // create employee
      $http.post("/api/employee", model)
      .success(function(res) {
          if(res.code != undefined && res.code != 0){
              bootbox.alert(res.text);
          }else {
            // show payments screen on create
            window.location = "/employee/" + res.empId + "/payments";
          }
      })
    }else {
      $scope.status.validationErrors = "Please fill in all the fields."
    }
  }

  // cancel add
  $scope.cancel = function(){
    bootbox.hideAll()
  }

}]);