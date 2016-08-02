define(['plugins/http'], function (http) {
    var defaultErrorMessage = 'Request failed :(';

    function post(url, data) {
        var defer = Q.defer();

        http.post(url, data)
            .then(function (result) {
                if (typeof result === 'object' && result !== null) {
                    if (result.Success && result.RequestData !== null) {
                        var dataType = typeof result.RequestData;
                        if ((dataType === 'boolean' && result.RequestData) || dataType === 'object' || dataType === 'number')
                            return defer.resolve(result.RequestData);
                    }
                }

                defer.reject(onError(result.RequestData));
            })
        .fail(function (data) {
            defer.reject(onError(data));
        });
        return defer.promise;
    }

    function get(url, data) {
        var defer = Q.defer();

        http.get(url, data)
            .then(function (result) {
                if (typeof result === 'object' && result !== null) {
                    if (result.Success && result.RequestData !== null) {
                        var dataType = typeof result.RequestData;
                        if (dataType === 'boolean' || dataType === 'object' || dataType === 'number')
                            return defer.resolve(result.RequestData);
                    }
                }
                defer.reject(onError(result.RequestData));
            })
        .fail(function (data) {
            defer.reject(onError(data));
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