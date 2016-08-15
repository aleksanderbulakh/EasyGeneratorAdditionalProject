define(['errorHandler/notImplementedError'], function (NotImplementedError) {
    return {
        throwIfObjectIsUndefined: function (object, name) {
            if (_.isUndefined(object)) {
                throw new NotImplementedError(name + ' is not found');
            }
        }
    };
});