ko.bindingHandlers.contentEditable = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        var htmlLazy = allBindingsAccessor().htmlLazy;

        $(element).on("input", function () {
            if (this.isContentEditable && ko.isWriteableObservable(htmlLazy)) {
                htmlLazy(this.innerHTML);
            }
        });
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());

        element.contentEditable = value;

        if (!element.isContentEditable) {
            $(element).trigger("input");
        }
    }
};