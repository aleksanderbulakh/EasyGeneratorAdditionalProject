ko.bindingHandlers.dateToLocalFormat = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        element.innerHTML = value.toLocaleDateString();
    }
};