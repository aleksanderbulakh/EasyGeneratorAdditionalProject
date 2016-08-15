define(['plugins/http', 'errorHandler/errorHandler', 'constants/constants'],
    function (http, errorHandler, constants) {
        var defaultErrorMessage = 'Request failed :(';

        function post(url, data) {
            var defer = Q.defer();

            http.post(url, data)
                .then(function (result) {
                    if (_.isObject(result)) {
                        if (result.Success && !_.isNull(result.RequestData)) {
                            if ((_.isBoolean(result.RequestData) && result.RequestData) ||
                                _.isObject(result.RequestData) || _.isNumber(result.RequestData))
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
                    if (_.isObject(result)) {
                        if (result.Success && !_.isNull(result.RequestData)) {
                            if ((_.isBoolean(result.RequestData) && result.RequestData) ||
                                _.isObject(result.RequestData) || _.isNumber(result.RequestData))
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