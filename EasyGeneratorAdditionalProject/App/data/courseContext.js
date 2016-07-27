define(['mapper/mapper', 'http/httpWrapper'], function (mapper, http) {
    function initialize() {
        var self = this;
        return http.get('user/get')
            .then(function (user) {
                self.user = mapper.mapUser(user);
            });
    }

    return {
        user: undefined,
        initialize: initialize,
        courseList: undefined
    };
});