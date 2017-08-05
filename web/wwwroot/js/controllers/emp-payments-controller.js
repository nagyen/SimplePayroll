App.Angular.getModule()
.controller("EmpPaymentsController", ["$scope", "$http", function ($scope, $http) {
    
    // status
    $scope.status = {
        validationErrors : ""
    };
    
    // model
    $scope.model = {};
    
    // init
    $scope.init = function (empId) {
        // get employee details
        $http.get("/api/employee/" + empId)
            .success(function (res) {
               $scope.model.employee = res; 
            });
    }
}]);
