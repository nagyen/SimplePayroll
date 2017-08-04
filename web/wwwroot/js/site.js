angular.module("app", []);
$("#grid-basic").bootgrid();
$("#date-picker").datepicker({
    autoclose: true,
    todayHighlight: true
});

function bootboxtest() {
    bootbox.alert("working");
}