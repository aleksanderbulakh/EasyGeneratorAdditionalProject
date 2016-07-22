define(['plugins/http'], function (http) {
    var defaultErrorMessage = 'Request failed :(';

    function post(url, data) {
        var defer = Q.defer();

        http.post(url, data)
            .then(function (result) {
                if (result && !result.ErrorMessage) {
                    return defer.resolve(result);
                }
                defer.reject(onError(result.ErrorMessage));
            })
        .fail(function (data) {
            defer.reject(onError());
        });
        return defer.promise;
    }

    function get(url, data) {
        var defer = Q.defer();

        http.get(url, data).then(function (result) {
            if (result && !result.ErrorMessage) {
                return defer.resolve(result);
            }
            defer.reject(onError(result.ErrorMessage));
        })
        .fail(function (data) {
            defer.reject(onError());
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