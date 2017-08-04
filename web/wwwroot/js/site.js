// difine app module object
var App = App || {};

// function that builds a module
App.module = function (moduleName, moduleBody) {

    // define module scope
    var moduleScope = {};

    // execute body
    moduleBody(moduleScope);

    // check if an init
    if (moduleScope.init !== undefined) {
        moduleScope.init();
    }

    // assign module
    App[moduleName] = moduleScope;
};

// build angular module
App.module("Angular", function(moduleScope) {

    // ng-app
    var ngApp = "app";

    // get angular module    
    moduleScope.getModule = function() {
        return angular.module(ngApp);
    };

    // get controller
    moduleScope.getController = function(name) {
        return angular.element("[ng-controller='" + name + "']");
    };

    // compile new controller
    moduleScope.compileNew = function() {

        // get controller not in scope of this app
        var $div = $("[ng-controller]").not(".ng-scope");

        if ($div.length > 0) {
            // inject the html
            angular.element($("body")[0]).injector().invoke([
                '$compile', function($compile) {
                    // get the scope, then compile
                    var scope = angular.element($div).scope();
                    $compile($div)(scope);
                }
            ]);
        }
    };

    // funciton to bootstrap angular app
    var bootstrapAngular = function() {
        angular.module(ngApp, []);
    }

    // init
    moduleScope.init = function() {
        // bootstrap angular app
        bootstrapAngular();
    }
});

$("#grid-basic").bootgrid({
    ajax: true,
    post: {},
    url: $("#grid-basic").data("url"),
    formatters: {},
    rowCount: [25, 50, -1]
});
$("#date-picker").datepicker({
    autoclose: true,
    todayHighlight: true
});
function bootboxtest() {
    bootbox.alert("working");
}
