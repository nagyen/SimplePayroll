App.Angular.getModule()
.controller("ListingController", ["$scope", "$http", function($scope, $http){

    // init status
    $scope.status = {
        register: false,
        validatationErrors: ""
    };

    // init model
    $scope.model = {};

    // init
    $scope.init = function(){
        // make listing 
        makeListing();
    }

    // function that makes listing
    var makeListing = function(){

        // get filters

        // make grid
        $("#employee-listing").bootgrid({
            ajax: true,
            post: {},
            url: $(this).data("url"),
            formatters: {},
            rowCount: [25, 50, -1]
        });
    }


}])