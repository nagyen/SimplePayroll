// format a string to ssn format with mask
App.Angular.getModule()
.filter("ssnFormat", function () {
    return function (value, mask) {

        if(!value) return "";

        var val = value.toString().replace(/\D/g, "");
        var len = val.length;
        if (len < 4)
            return val;
        else if (len > 3 && len < 6)
            if (mask)
                return "***-"+ val.substring(3);
            else
                return val.substring(0, 3) + "-" + val.substring(3);

        else if (len > 5)
            if (mask)
                return "***-**-" + val.substr(5, 4);
            else
                return val.substr(0, 3) + "-" + val.substr(3, 2) + "-" + val.substr(5, 4);
    }
});