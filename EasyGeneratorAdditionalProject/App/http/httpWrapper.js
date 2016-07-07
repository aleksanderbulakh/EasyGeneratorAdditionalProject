define(['plugins/http'], function (http) {

    function post(url, data) {
        return http.post(url, data).fail(function () {
            alert('Request failed :(');
        });
    }

    return {
        post: post
    };
});