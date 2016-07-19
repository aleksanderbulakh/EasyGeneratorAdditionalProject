define(['plugins/http'], function (http) {

    function post(url, data) {
        return http.post(url, data).fail(function (data) {
            alert('Request failed :(');
        });
    }

    function get(url, data) {
        return http.get(url, data).fail(function (data) {
            alert('Request failed :(');
        });
    }

    return {
        post: post,
        get: get
    };
});