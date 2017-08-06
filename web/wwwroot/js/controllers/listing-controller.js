App.Angular.getModule()
.controller("ListingController", ["$scope", "$http", function($scope, $http){

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
        
        // formatters
        var formatters = {
            "link": function (col, row) {
                var url = "/employee/" + row.empId + "/payments";
                return "<a href='"+ url +"'>" + row.fullName + "</a>"
            }
        };

        // make grid
        $("#employee-listing").bootgrid({
            ajax: true,
            url: "/employee/getListFiltered",
            requestHandler: filters,
            formatters: formatters,
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

    // add new employee btn click
    $scope.addNew = function() {
        // show dialog
        $http.get("/employee/addEmployeeDialog")
            .success(function(res){
                bootbox.dialog({
                    title: "Add New Employee",
                    message: res,
                    size: "large"
                })
                // compile injected controller
                App.Angular.compileNew();
            });
    }
}]);