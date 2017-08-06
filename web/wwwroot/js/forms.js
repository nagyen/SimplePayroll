// form helper module
App.module("Forms", function(moduleScope) {

    // function to build date picker from selector
    moduleScope.setForm = function(sel) {
        $(sel).find("input[data-date-picker]").datepicker({
            autoclose: true,
            todayHighlight: true
        });
    };

    // init
    moduleScope.init = function() {
        // build datepicker on initial page load
		$("input[data-date-picker]").datepicker({
		    autoclose: true,
		    todayHighlight: true
		});
    }
});