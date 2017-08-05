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
                if (res.code != undefined && res.code != 0)
                {
                    bootbox.alert(res.text,  function () {
                        window.location = "/";
                    })
                }else {
                    // set employee details
                    $scope.model.employee = res;

                    // get payments
                    $scope.refreshPayements(1);
                }
            });
        
    };
    
    // get payments for employee
    $scope.refreshPayements = function (page) {
        // default to 1st page
        if(!page) page = 1;
        // set current page
        $scope.model.currentPage = page;
        
        var empId = $scope.model.employee.id;
        //  get the payments paged
        $http.get("/api/payments/"+empId+"?page="+page)
            .success(function(res){
                if(res.code !== undefined && res.code !== 0)
                {
                    bootbox.alert(res.text);
                }
                else
                {
                    $scope.model.payments = res;
                }

            });
        
    };

    // record a transaction
    $scope.recordPay = function(){
        var empId = $scope.model.employee.id;
        // show dialog
        $http.get("/employee/recordPayDialog", empId)
            .success(function(res){
                var modal = bootbox.dialog({
                    title: "Record Pay",
                    message: res,
                    size: "large"
                });
                
                // compile new conroller
                App.Angular.compileNew();
            })

    }
    
}]);
