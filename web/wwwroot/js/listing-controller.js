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
        var model = $scope.model;

        // remove current listing
        //$("#employee-listing").bootgrid("destroy");

        // filters
        var filters = function (){
            return {
                empId: model.empId,
                state: model.state,
                payPostingFrom: model.payPostingFrom,
                payPostingTo: model.payPostingTo
            }
        };

        // make grid
        $("#employee-listing").bootgrid({
            ajax: true,
            url: "/listing/getListFiltered",
            requestHandler: function(req) {
                req.empId = model.empId;
                req.state = model.state,
                req.payPostingFrom = model.payPostingFrom,
                req.payPostingTo = model.payPostingTo
                return req;
            },
            formatters: {},
            rowCount: [25, 50, -1]
        }).on("loaded.rs.jquery.bootgrid", function (e) {
            // move any header appends
            $("[data-bootgrid-append]")
            .prependTo(".bootgrid-header .actionBar")
            .removeAttr("data-bootgrid-append");
	    });

    }

    // watch filter changes
    $scope.$watchGroup(["model.empId", "model.state", "model.payPostingFrom", "model.payPostingTo"], function(){
        // reload listing
        //$("#employee-listing").bootgrid("reload")
    })


}])