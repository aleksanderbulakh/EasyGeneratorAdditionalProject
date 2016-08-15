define(['plugins/http', 'errorHandler/errorHandler', 'constants/constants'],
    function (http, errorHandler, constants) {
        var defaultErrorMessage = 'Request failed :(';

        function post(url, data) {
            var defer = Q.defer();

            http.post(url, data)
                .then(function (result) {
                    if (typeof result === constants.DATA_TYPES.OBJECT && result !== null) {
                        if (result.Success && result.RequestData !== null) {
                            var dataType = typeof result.RequestData;
                            if ((dataType === constants.DATA_TYPES.BOOL && result.RequestData) ||
                                dataType === constants.DATA_TYPES.OBJECT || dataType === constants.DATA_TYPES.NUMBER)
                                return defer.resolve(result.RequestData);
                        }
                    }

                    defer.reject(onError(result.RequestData));
                })
                .fail(function () {
                    defer.reject();
                });

            return defer.promise;
        }

        function get(url, data) {
            var defer = Q.defer();

            http.get(url, data)
                .then(function (result) {
                    if (typeof result === constants.DATA_TYPES.OBJECT && result !== null) {
                        if (result.Success && result.RequestData !== null) {
                            var dataType = typeof result.RequestData;
                            if ((dataType === constants.DATA_TYPES.BOOL && result.RequestData) ||
                                dataType === constants.DATA_TYPES.OBJECT || dataType === constants.DATA_TYPES.NUMBER)
                                return defer.resolve(result.RequestData);
                        }
                    }
                    defer.reject(onError(result.RequestData));
                })
                .fail(function () {
                    defer.reject();
                });

            return defer.promise;
        }

        function onError(errorMessage) {
            errorMessage = errorMessage || defaultErrorMessage;
            return errorMessage;
        }

        return {
            post: post,
            get: get
        };
    });