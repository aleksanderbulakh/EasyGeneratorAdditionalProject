ko.bindingHandlers.fileInput = {
    init: function (element, valueAccessor) {
        element.onchange = function () {
            var fileData = ko.utils.unwrapObservable(valueAccessor()) || {};
            if (fileData.dataUrl) {
                fileData.dataURL = fileData.dataUrl;
            }
            if (fileData.objectUrl) {
                fileData.objectURL = fileData.objectUrl;
            }
            fileData.file = fileData.file || ko.observable();

            var file = this.files[0];
            if (file) {
                fileData.file(file);
            }

            if (!fileData.clear) {
                fileData.clear = function () {
                    $.each(['file', 'objectURL', 'base64String', 'binaryString', 'text', 'dataURL', 'arrayBuffer'], function (i, property) {
                        if (fileData[property] && ko.isObservable(fileData[property])) {
                            if (property == 'objectURL') {
                                windowURL.revokeObjectURL(fileData.objectURL());
                            }
                            fileData[property](null);
                        }
                    });
                    element.value = '';
                }
            }
            if (ko.isObservable(valueAccessor())) {
                valueAccessor()(fileData);
            }
        };
        element.onchange();

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var fileData = ko.utils.unwrapObservable(valueAccessor()) || {};
            fileData.clear = undefined;
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor) {

        var fileData = ko.utils.unwrapObservable(valueAccessor());

        var file = ko.isObservable(fileData.file) && fileData.file();

        if (fileData.objectURL && ko.isObservable(fileData.objectURL)) {
            var newUrl = file && windowURL.createObjectURL(file);
            if (newUrl) {
                var oldUrl = fileData.objectURL();
                if (oldUrl) {
                    windowURL.revokeObjectURL(oldUrl);
                }
                fileData.objectURL(newUrl);
            }
        }


        if (fileData.base64String && ko.isObservable(fileData.base64String)) {
            if (fileData.dataURL && ko.isObservable(fileData.dataURL)) {
                // will be handled
            }
            else {
                fileData.dataURL = ko.observable(); // hack
            }
        }

        // var properties = ['binaryString', 'text', 'dataURL', 'arrayBuffer'], property;
        // for(var i = 0; i < properties.length; i++){
        //     property = properties[i];
        ['binaryString', 'text', 'dataURL', 'arrayBuffer'].forEach(function (property) {
            var method = 'readAs' + (property.substr(0, 1).toUpperCase() + property.substr(1));
            if (property != 'dataURL' && !(fileData[property] && ko.isObservable(fileData[property]))) {
                return true;
            }
            if (!file) {
                return true;
            }
            var reader = new FileReader();
            reader.onload = function (e) {
                if (fileData[property]) {
                    fileData[property](e.target.result);
                }
                if (method == 'readAsDataURL' && fileData.base64String && ko.isObservable(fileData.base64String)) {
                    var resultParts = e.target.result.split(",");
                    if (resultParts.length === 2) {
                        fileData.base64String(resultParts[1]);
                    }
                }
            };

            reader[method](file);
        });
    }
};