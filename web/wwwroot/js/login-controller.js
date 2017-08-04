angular.module("app")
.controller("LoginController", ["$scope", "$http", function($scope, $http){
    //debug
    $scope.DEBUG = true;

    // init status
    $scope.status = {
        Register: false,
        ValidatationErrors: ""
    };

    // init model
    $scope.model = {};

    // function to login user
    $scope.login = function(){

        $scope.status.Instructions = ""
        var model = $scope.model;
        //check if form valid
        if($scope.frm.$valid) {

            // login
            $http.post("/auth/login", model)
            .success(function(res) {
                if(res.code != undefined && res.code != 0)
                {
                    $scope.status.ValidatationErrors = res.text;
                }else {
                    window.location = res.redirect;
                }
            })

        }
        else {
            $scope.status.ValidatationErrors = "Please fill all required fields."
        }
    }

    // function to register user
    $scope.register = function(){
        var model = $scope.model;
        //check if form valid
        if($scope.frm.$valid) {

            //check if both passwords match
            if(model.Password != model.ConfirmPassword)
            {
                $scope.status.ValidatationErrors = "Passwords not matched."
                return;
            }

            // login
            $http.post("/auth/register", model)
            .success(function(res) {
                if(res.code != undefined && res.code != 0)
                {
                    $scope.status.ValidatationErrors = res.text;
                }else {
                    $scope.status.Instructions = "Registered Successfully! Please Login to continue."
                    $scope.switchScreen(false);
                }
            })

        }
        else {
            $scope.status.ValidatationErrors = "Please fill all required fields."
        }
    }

    // switch screen
    $scope.switchScreen = function(registerScreen) {
        $scope.status.Register = registerScreen;
        $scope.status.ValidatationErrors = "";
        $scope.model.Username = "";
        $scope.model.Password = "";
        $scope.model.FirstName = "";
        $scope.model.LastName = "";
        $scope.model.ConfirmPassword = ""
    }
}]);