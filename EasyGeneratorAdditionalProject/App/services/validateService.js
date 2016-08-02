define(function () {
    return {
        throwIfObjectUndefined: function (object, name) {
            if (object === undefined)
                throw name + ' is not found';
        }
    };
});