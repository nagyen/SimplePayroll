﻿App.Angular.getModule()
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
            console.log(res);
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

}]).directive('ssn', function () {
    function makeSsn (value) {
      var result = value;

      var ssn = value ? value.toString() : '';
      if (ssn.length > 3) {
        result = ssn.substr(0, 3) + '-';
        if (ssn.length > 5) {
          result += ssn.substr(3, 2) + '-';
          result += ssn.substr(5, 4);
        }
        else {
          result += ssn.substr(3);
        }
      }

      return result;
    }

    return {
      restrict: 'A',
      require: 'ngModel',
      link: function (scope, element, attrs, ngModel) {
        ngModel.$formatters.push(function (value) {
          return makeSsn(value);
        });

        // clean output as digits
        ngModel.$parsers.push(function (value) {
          var cursorPosition = element[0].selectionStart;
          var oldLength = value.toString().length;
          var nonDigits = /[^0-9]/g;
          var intValue = value.replace(nonDigits, '');
          if (intValue.length > 9) {
            intValue = intValue.substr(0, 9);
          }
          var newValue = makeSsn(intValue);
          ngModel.$setViewValue(newValue);
          ngModel.$render();
          element[0].setSelectionRange(cursorPosition + newValue.length - oldLength, cursorPosition + newValue.length - oldLength);
          return intValue;
        });
      }
    };
  });