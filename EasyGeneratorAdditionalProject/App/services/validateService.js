define(['errorHandler/notImplementedError'], function (NotImplementedError) {
    return {
        throwIfObjectIsUndefined: function (object, name) {
            if (object === undefined) {
                throw new NotImplementedError(name + ' is not found');
            }
        }
    };
});