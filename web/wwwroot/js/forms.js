// form helper module
App.module("Form", function(moduleScope) {

    // function to build date picker from selector
    moduleScope.setForm = function(sel) {
        $(sel).closest("input#date-picker").datepicker({
            autoclose: true,
            todayHighlight: true
        });
    } 

    // init
    moduleScope.init = function() {
        // build datepicker on initial page load
		$("input#date-picker").datepicker({
		    autoclose: true,
		    todayHighlight: true
		});
    }
});