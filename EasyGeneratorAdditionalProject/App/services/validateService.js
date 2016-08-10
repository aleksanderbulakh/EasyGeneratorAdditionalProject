define(function () {
    return {
        throwIfObjectIsUndefined: function (object, name) {
            if (object === undefined)
                throw name + ' is not found';
        }
    };
});