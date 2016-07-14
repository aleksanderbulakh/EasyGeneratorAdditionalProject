ko.bindingHandlers.htmlLazy = {
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        if (!element.isContentEditable) {
            element.innerHTML = value;
        }
    }
};