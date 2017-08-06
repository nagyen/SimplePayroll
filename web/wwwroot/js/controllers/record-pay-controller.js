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
        
        // create payment
        $http.post("/api/payments", model)
            .success(function(res) {
                if(res.code !== undefined && res.code !== 0){
                    bootbox.alert(res.text);
                }else {
                    // broadcast event to refresh payment list
                    $rootScope.$broadcast("pay-added");
                    // close dialog
                    bootbox.hideAll();
                }
            })
    }else {
      $scope.status.validationErrors = "Please fill in all the required fields."
    }
  };

  // cancel
  $scope.cancel = function(){
    bootbox.hideAll();
  };
  
  // function to get all deductions for given salary
    $scope.getAllDeductions = function () {
      var model = $scope.model;
      var grossPay = model.grossPay;
      $http.get("/api/payments/getAllDeductions", {params: {grossPay: grossPay, empId: model.empId}})
          .success(function (res) {
              if (res.code !== undefined && res.code !== 0){
                  bootbox.alert(res.text);
              }
              else {
                  model.taxableIncome = res.taxableIncome;
                  model.retirement401K = res.retirement401K;
                  model.insurance = res.insurance;
                  model.fedTax = res.fedTax;
                  model.stateTax = res.stateTax;
                  model.socialSecurityTax = res.socialSecurityTax;
                  model.medicareTax = res.medicareTax;
                  model.netPay = res.netPay;
                  model.w4Allowances = res.w4Allowances;
              }
          })
      
    }
}]);