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
    };

    // function that makes listing
    var makeListing = function(){
        var model = $scope.model;

        // filters
        var filters = function (req){
            $("[data-listing-filter]").each(function () {
                var id =  $(this).data("listing-filter");
                req[id] = $(this).val();
            });
            return req;
        };

        // make grid
        $("#employee-listing").bootgrid({
            ajax: true,
            url: "/listing/getListFiltered",
            requestHandler: filters,
            formatters: {},
            rowCount: [10, 25, -1]
        }).on("loaded.rs.jquery.bootgrid", function (e) {
            // move any header appends
            $("[data-bootgrid-append]")
            .prependTo(".bootgrid-header .actionBar")
            .removeAttr("data-bootgrid-append");
	    });

    };

    // watch filter changes
    $scope.$watchGroup(["model.empId", "model.state", "model.payPostingFrom", "model.payPostingTo"], function(){
        // reload listing
        $("#employee-listing").bootgrid("reload")
    })


}]);