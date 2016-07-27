ko.extenders.validName = function (target, overrideMessage) {
    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    function validate(newValue) {
        var check;
        if (!newValue)
            check = false;
        else {
            newValue = newValue.trim();
            check = newValue.length <= 255 && newValue.length > 0;
        }
        target.hasError(!check);
        target.validationMessage(check ? "" : overrideMessage || "This field is required");
    }

    validate(target());
    target.subscribe(validate);
    return target;
};