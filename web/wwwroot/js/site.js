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